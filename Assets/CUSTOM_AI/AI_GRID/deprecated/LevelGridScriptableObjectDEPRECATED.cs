using UnityEngine;

[CreateAssetMenu(menuName="CUSTOM/LEVEL GRID REF")]
public class LevelGridScriptableObject : ScriptableObject
{
    public byte[] hash;
    public int[] stored;
    public Vector3 dimensions;

    private const int 
        air     = 0, 
        solid   = 9, 
        ground  = 2, 
        surface = 3;
    public int AIR {get{return air;}}
    public int SOLID {get{return solid;}}
    public int GROUND {get{return ground;}}
    public int SURFACE {get{return surface;}}

    public int[,,] UnflattenedGrid
    {get{
        var length = Mathf.FloorToInt(dimensions.x);
        var height = Mathf.FloorToInt(dimensions.y);
        var width  = Mathf.FloorToInt(dimensions.z);
        var threedee = new int[length, height, width];
        
        for (int i = 0; i < stored.Length; i++)
        {
            var x = i % length;
            var y = (i / length) % height;
            var z = i / (length * height);
            threedee[x,y,z] = stored[x + y * length + z * length * height];
        }
        return threedee;
    }}

    public int UnflattenedIndex(int x, int y, int z)
    {
        var length = Mathf.FloorToInt(dimensions.x);
        var height = Mathf.FloorToInt(dimensions.y);
        var width  = Mathf.FloorToInt(dimensions.z);

        return stored[x + y * length + z * length * height];
    }
}
