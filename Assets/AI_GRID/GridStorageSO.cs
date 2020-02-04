using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="CUSTOM/grid storage")]
public class GridStorageSO : ScriptableObject
{
    [SerializeField]
    public Point[] points;
    public List<Point> ground;
    public List<Point> surface;
    public Vector3 size;
    public LayerMask mask;
    // public float scale;

    public int FlattenedIndex(int x, int y, int z)
    {
        return  x + y * (int)size.x + z * (int)size.x * (int)size.y;
    }

    public int FlattenedIndex(Vector3 worldCoords)
    {
        var x = (int)worldCoords.x;
        var y = (int)worldCoords.y;
        var z = (int)worldCoords.z;
        return  x + y * (int)size.x + z * (int)size.x * (int)size.y;
    }

    public int GridIndex(Vector3 worldCoords)
    {
        return FlattenedIndex(worldCoords);
    }

    public Point PointFromCoords(Vector3 coords)
    {
        return points[GridIndex(coords)];
    }

    public List<Point> GetSurrounding(Point point)
    {
        List<Point> surrounding = new List<Point>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == 0 && y == 0 && z == 0)
                        continue;

                    var checkX = (int)point.coords.x + x;
                    var checkY = (int)point.coords.y + y;
                    var checkZ = (int)point.coords.z + z;

                    if ((checkX >= 0 && checkX < size.x)
                    &&  (checkY >= 0 && checkY < size.y)
                    &&  (checkZ >= 0 && checkZ < size.z))
                        surrounding.Add(points[FlattenedIndex(checkX, checkY, checkZ)]);
                }
            }
        }

        return surrounding;
    }

    // public CheckPoint GetValueFrom1DIndexAtGridCoords(int x, int y, int z)
    // {
    //     var flattenedIndex =  x + y * (int)size.x + z * (int)size.x * (int)size.y;
    //     return point[flattenedIndex];
    // }
}