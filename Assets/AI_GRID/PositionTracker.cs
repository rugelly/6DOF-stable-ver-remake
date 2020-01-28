using System;
using System.Linq;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public GridStorageSO _grid;
    public TwentySixDirectionsScriptableObject _directionRef;
    public WalksOn walksOn;
    public Vector3 currentGridCoords;
    public bool[] moveableDirections;
    public bool drawGizmo;

    private void OnEnable()
    {
        moveableDirections = new bool[_directionRef.all.Length];
    }

    Vector3 prevGridCoords;

    private void Update()
    {
        var pos = transform.position;
        currentGridCoords = new Vector3(
            Mathf.FloorToInt(pos.x), 
            Mathf.FloorToInt(pos.y), 
            Mathf.FloorToInt(pos.z));

        // only check surrounding when grid pos changes
        // if (currentGridCoords != prevGridCoords)
        // check every position around current against the status grid
        for (int i = 0; i < _directionRef.all.Length; i++)
        {
            var checkIndex = currentGridCoords + _directionRef.all[i];
            checkIndex = new Vector3(
                Mathf.Clamp(checkIndex.x, 0, _grid.size.x),
                Mathf.Clamp(checkIndex.y, 0, _grid.size.y),
                Mathf.Clamp(checkIndex.z, 0, _grid.size.z));

            var status = 
                _grid.points[_grid.FlattenedIndex((int)checkIndex.x, (int)checkIndex.y, (int)checkIndex.z)]
                .status;

            moveableDirections[i] = CheckType(status);
        }

        prevGridCoords = currentGridCoords;
    }

    private bool CheckType(int status)
    {
        if (status == _grid.SOLID)
            return false;
            
        switch (walksOn)
        {
            case WalksOn.all:
                return status != _grid.SOLID ? true : false;
            case WalksOn.airOnly:
                return status == _grid.AIR ? true : false;
            case WalksOn.surfaceOnly:
                return status == _grid.SURFACE || status == _grid.GROUND ? true : false;
            case WalksOn.groundOnly:
                return status == _grid.GROUND ? true : false;
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
