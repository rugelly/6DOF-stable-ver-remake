using UnityEngine;

[System.Serializable]
public class Point
{
    public int status;
    public Vector3 coords;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Point(int _status, Vector3 _coords)
    {
        status = _status;
        coords = _coords;
    }
}
