using UnityEngine;

public class Waypoint
{
    public Transform point;
    public float pauseTime;
    public int overrideNextPoint = -1;
    public float minDistanceToReach = 0.5f;
}