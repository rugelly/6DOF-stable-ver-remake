using UnityEngine;

[CreateAssetMenu(menuName="CUSTOM/grid storage")]
public class GridStorageSO : ScriptableObject
{
    [SerializeField]
    public Point[] points;
    public Vector3 size;
    // public float scale;

    private const int 
        air     = 0, 
        solid   = 9, 
        ground  = 2, 
        surface = 3,
        border  = 8;
    public int AIR {get{return air;}}
    public int SOLID {get{return solid;}}
    public int GROUND {get{return ground;}}
    public int SURFACE {get{return surface;}}
    public int BORDER {get{return border;}}

    public int FlattenedIndex(int x, int y, int z)
    {
        return  x + y * (int)size.x + z * (int)size.x * (int)size.y;
    }

    // public CheckPoint GetValueFrom1DIndexAtGridCoords(int x, int y, int z)
    // {
    //     var flattenedIndex =  x + y * (int)size.x + z * (int)size.x * (int)size.y;
    //     return point[flattenedIndex];
    // }
}