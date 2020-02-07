using System;
using System.Linq;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public GridStorageSO _grid;
    public TwentySixDirectionsScriptableObject _directionRef;
    public WalksOn walksOn;
    public Vector3 currentGridCoords;
    public int currentGridIndex {get{return _grid.FlattenedIndex(currentGridCoords);}}
    public bool[] moveableDirections;
    public bool drawGizmo;
    public Vector3 upVector;

    private void OnEnable()
    {
        moveableDirections = new bool[_directionRef.all.Length];
    }

    Vector3 prevGridCoords;

    private void Update()
    {
        var pos = transform.position;
        currentGridCoords = new Vector3((int)pos.x, (int)pos.y, (int)pos.z);

        // this activates once when grid position changes
        if (currentGridCoords != prevGridCoords)
            upVector = WhatsMyUpVector();

        // check every position around current against the status grid
        for (int i = 0; i < _directionRef.all.Length; i++)
        {
            var checkIndex = currentGridCoords + _directionRef.all[i];
            // dont check outside grid boundaries
            checkIndex = new Vector3(
                Mathf.Clamp(checkIndex.x, 0, _grid.size.x),
                Mathf.Clamp(checkIndex.y, 0, _grid.size.y),
                Mathf.Clamp(checkIndex.z, 0, _grid.size.z));

            var status = _grid.points[_grid.FlattenedIndex(checkIndex)].status;
            moveableDirections[i] = CheckType(status);
        }

        prevGridCoords = currentGridCoords;
    }

    private bool CheckType(int status)
    {
        if (status == Status.SOLID)
            return false;
            
        switch (walksOn)
        {
            case WalksOn.all:
                return status != Status.SOLID ? true : false;
            case WalksOn.airOnly:
                return status == Status.AIR ? true : false;
            case WalksOn.surfaceOnly:
                return status == Status.SURFACE || status == Status.GROUND ? true : false;
            case WalksOn.groundOnly:
                return status == Status.GROUND ? true : false;
            default:
                return false;
        }
    }

    public int GetClosestValidDirectionIndex(Vector3 direction)
    {
        var correctedInputDirection = direction - transform.position;
        float[] closestDirection = new float[26];
        for (int i = 0; i < _directionRef.all.Length; i++)
        {
            if (moveableDirections[i])
                closestDirection[i] = (correctedInputDirection - _directionRef.all[i]).sqrMagnitude;
            else
                closestDirection[i] = Mathf.Infinity; // TODO: just use a big number instead?
        }
        return Array.IndexOf(closestDirection, closestDirection.Min());
    }

    public Vector3 WhatsMyUpVector()
    {
        int rand = UnityEngine.Random.Range(0, 2);

        if (rand == 0)
        {
            // do up/down + cardinals first
            for (int i = 0; i < 6; i++)
            {
                if (CondensedCast(i))
                    return -_directionRef.all[i];
            }
            // no hits? do the rest
            for (int i = 6; i < _directionRef.all.Length; i++)
            {
                if (CondensedCast(i))
                    return -_directionRef.all[i];
            }
        }
        else
        {
            // same as above but reverse order
            for (int i = 5; i >= 0; i--)
            {
                if (CondensedCast(i))
                    return -_directionRef.all[i];
            }
            for (int i = _directionRef.all.Length - 1; i >= 0; i--)
            {
                if (CondensedCast(i))
                    return -_directionRef.all[i];
            }
        }
        // nothing at all? then up vector is UP
        return Vector3.up;
    }

    private bool CondensedCast(int index)
    {
        return Physics.Raycast(transform.position, _directionRef.all[index] * 0.7f, _grid.mask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (drawGizmo)
        for (int i = 0; i < moveableDirections.Length; i++)
        {
            if (moveableDirections[i])
                Gizmos.DrawWireCube(transform.position + _directionRef.all[i], Vector3.one * 0.4f);
        }
    }
}
