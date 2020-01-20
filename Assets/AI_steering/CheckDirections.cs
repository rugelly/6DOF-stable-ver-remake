using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(Context))]
public class CheckDirections : MonoBehaviour
{
    Context _context;
    [HideInInspector] public bool[] surroundingSpotsObstructed = new bool[26];
    [HideInInspector] public bool[] surroundingSpotsValid = new bool[26];
    public WalksOn walksOn;

    private void Awake()
    {
        _context = GetComponent<Context>();
    }

    private void Update()
    {
        // loop all directions
        for (int i = 0; i < _context.directionReference.Length; i++)
        {
            // array spot == true if a direction ray hits something
            surroundingSpotsObstructed[i] = Obstructed(transform.position, _context.directionReference[i]);

            // depending on movement mode:
            // match inverse of obstructed array
            // OR
            // in an unobstructed spot, check all spots around that one. return depending on mvmt type
            if (walksOn == WalksOn.all)
                // valid to move any direction that is not obstructed
                surroundingSpotsValid[i] = !surroundingSpotsObstructed[i];
            else
                surroundingSpotsValid[i] = CheckSpaceAroundSpotInDirection(i);
        }
    }

    public int GetClosestValidDirectionIndex(Vector3 direction)
    {
        var correctedInputDirection = direction - transform.position;
        float[] closestDirection = new float[26];
        for (int i = 0; i < _context.directionReference.Length; i++)
        {
            if (surroundingSpotsValid[i])
                closestDirection[i] = (correctedInputDirection - _context.directionReference[i]).sqrMagnitude;
            else
                closestDirection[i] = 99999;
        }
        return Array.IndexOf(closestDirection, closestDirection.Min());
    }

    private bool CheckSpaceAroundSpotInDirection(int i)
    {
        // spot to cast from is the direction we are checking, shifted in that direction
        var checkedPos = transform.position + _context.directionReference[i];

        // if the direction is NOT obscured
        if (!surroundingSpotsObstructed[i])
        {
            // for each direction in the initial loop, loop all directions a second time
            for (int ii = 0; ii < _context.directionReference.Length; ii++)
            {
                // there is something around the checked point in ANY direction
                if (Obstructed(checkedPos, _context.directionReference[ii]))
                {
                    // create a new position that is in the direction the obstruction was found
                    var vertCheckPos = checkedPos + _context.directionReference[ii];

                    // only checked spots below the new position are valid
                    if (vertCheckPos.y < checkedPos.y && walksOn == WalksOn.groundOnly)
                        return true;
                    // as long as there is an obstruction around this point it is a valid direction
                    else if (walksOn == WalksOn.surfaceOnly)
                        return true;
                    else if (walksOn == WalksOn.airOnly)
                        return false;
                }
            }
            // went through the whole loop without hitting an obstruction
            if (walksOn == WalksOn.airOnly)
                return true;
        }
        else
            return false;

        return false;
    }

    private bool Obstructed(Vector3 start, Vector3 direction)
    {
        // var radius = 0.5f;
        // var result = !Physics.SphereCast(start, radius, direction, out RaycastHit hit, 1 - radius);
        var result = Physics.Raycast(start, direction, 1f);
        return result;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < surroundingSpotsValid.Length; i++)
        {
            Gizmos.color = Color.black;
            if (surroundingSpotsValid[i])
                Gizmos.DrawWireCube(_context.directionReference[i] + transform.position, Vector3.one / 5);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
