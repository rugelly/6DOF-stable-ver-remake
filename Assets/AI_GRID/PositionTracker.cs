using System;
using System.Linq;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    VolumeCheck _volume;
    public TwentySixDirectionsScriptableObject _directionRef;
    public WalksOn walksOn;

    public Vector3 currentGridPos;
    [HideInInspector] public bool[] moveableDirections;

    private void OnEnable()
    {
        _volume = FindObjectOfType<VolumeCheck>();
        moveableDirections = new bool[_directionRef.all.Length];
    }

    private void Update()
    {
        var pos = transform.position;
        currentGridPos = new Vector3(
            Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));

        // check every position around current against the status grid
        for (int i = 0; i < _directionRef.all.Length; i++)
        {
            var checkIndex = currentGridPos + _directionRef.all[i];
            checkIndex = new Vector3(
                Mathf.Clamp(checkIndex.x, 0, _volume.length),
                Mathf.Clamp(checkIndex.y, 0, _volume.height),
                Mathf.Clamp(checkIndex.z, 0, _volume.width));

            var status = _volume.statusGrid[(int)checkIndex.x, (int)checkIndex.y, (int)checkIndex.z];

            moveableDirections[i] = CheckType(status);
        }
    }

    private bool CheckType(int status)
    {
        if (status == _volume.SOLID)
            return false;
            
        switch (walksOn)
        {
            case WalksOn.all:
                return status != _volume.SOLID ? true : false;
            case WalksOn.airOnly:
                return status == _volume.AIR ? true : false;
            case WalksOn.surfaceOnly:
                return status == _volume.SURFACE || status == _volume.GROUND ? true : false;
            case WalksOn.groundOnly:
                return status == _volume.GROUND ? true : false;
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
                closestDirection[i] = 9999; // TODO: is infinity instead of this better?
        }
        return Array.IndexOf(closestDirection, closestDirection.Min());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < moveableDirections.Length; i++)
        {
            if (moveableDirections[i])
                Gizmos.DrawWireCube(transform.position + _directionRef.all[i], Vector3.one * 0.4f);
        }
    }
}
