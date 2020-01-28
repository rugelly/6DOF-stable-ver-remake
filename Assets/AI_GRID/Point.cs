using UnityEngine;

[System.Serializable]
public class Point
{
    public int status;
    public Vector3 coords;

    public Point(int _status, Vector3 _coords)
    {
        status = _status;
        coords = _coords;
    }
}
