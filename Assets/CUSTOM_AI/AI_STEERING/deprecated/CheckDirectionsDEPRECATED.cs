using UnityEngine;
using System.Linq;
using System;

public class CheckDirections : MonoBehaviour
{
    [HideInInspector] public bool[] surroundingSpotsObstructed = new bool[26];
    [HideInInspector] public bool[] surroundingSpotsValid = new bool[26];
    [HideInInspector] public Vector3[] directionReference;
    public WalksOn walksOn;
    public LayerMask mask;
    public float rayLength;

    private void Awake()
    {
        ConstructArray();
    }

    private void Update()
    {
        // loop all directions
        for (int i = 0; i < directionReference.Length; i++)
        {
            // array spot == true if a direction ray hits something
            surroundingSpotsObstructed[i] = Obstructed(transform.position, directionReference[i]);

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
        for (int i = 0; i < directionReference.Length; i++)
        {
            if (surroundingSpotsValid[i])
                closestDirection[i] = (correctedInputDirection - directionReference[i]).sqrMagnitude;
            else
                closestDirection[i] = 9999; // TODO: is infinity a performance hit?
        }
        return Array.IndexOf(closestDirection, closestDirection.Min());
    }

    private bool CheckSpaceAroundSpotInDirection(int i)
    {
        // spot to cast from is the direction we are checking, shifted in that direction
        var checkedPos = transform.position + directionReference[i];

        // if the direction is NOT obscured
        if (!surroundingSpotsObstructed[i])
        {
            // for each direction in the initial loop, loop all directions a second time
            for (int ii = 0; ii < directionReference.Length; ii++)
            {
                // there is something around the checked point in ANY direction
                if (Obstructed(checkedPos, directionReference[ii]))
                {
                    // create a new position that is in the direction the obstruction was found
                    var vertCheckPos = checkedPos + directionReference[ii];

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

        return false;
    }

    private bool Obstructed(Vector3 start, Vector3 direction)
    {
        var radius = 0.4f;
        var result = Physics.SphereCast(start, radius, direction, out RaycastHit hit, 1 - (radius /2), mask);
        // var result = Physics.Raycast(start, direction, rayLength, mask);
        return result;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < surroundingSpotsValid.Length; i++)
        {
            Gizmos.color = Color.black;
            if (surroundingSpotsValid[i])
                Gizmos.DrawWireCube(directionReference[i] + transform.position, Vector3.one / 5);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

    private void ConstructArray()
    {
        directionReference = new Vector3[26] 
        {
        Vector3.up,      Vector3.down,
        Vector3.forward, Vector3.back,
        Vector3.right,   Vector3.left,

        Vector3Diagonal.rghtUpForw, Vector3Diagonal.rghtDnForw,
        Vector3Diagonal.leftUpForw, Vector3Diagonal.leftDnForw,
        Vector3Diagonal.rghtUpBack, Vector3Diagonal.rghtDnBack,
        Vector3Diagonal.leftUpBack, Vector3Diagonal.leftDnBack,

        Vector3Diagonal.rghtForw, Vector3Diagonal.rghtBack,
        Vector3Diagonal.leftForw, Vector3Diagonal.leftBack,
        
        Vector3Diagonal.rghtUp, Vector3Diagonal.rghtDn,
        Vector3Diagonal.leftUp, Vector3Diagonal.leftDn,

        Vector3Diagonal.forwUp, Vector3Diagonal.forwDn,
        Vector3Diagonal.backUp, Vector3Diagonal.backDn
        };
    }
}
