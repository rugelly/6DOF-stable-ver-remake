using UnityEngine;

[ExecuteInEditMode]
public class VolumeGenerator : MonoBehaviour
{
    public TwentySixDirectionsScriptableObject _dir;
    public GridStorageSO _grid;
    public LayerMask mask;
    public bool generate;

    private void OnGUI()
    {
        if (Application.isEditor)
        if (generate)
        {
            generate = false;
            Generate();
        }
    }

    private void Generate()
    {
        // init SO grid size
        _grid.points = new Point[(int)_grid.size.x * (int)_grid.size.y * (int)_grid.size.z];

        for (int x = 0; x < _grid.size.x; x++)
        {
            for (int y = 0; y < _grid.size.y; y++)
            {
                for (int z = 0; z < _grid.size.z; z++)
                {
                    _grid.points[_grid.FlattenedIndex(x, y, z)] = 
                        new Point(CheckSpot(x, y, z), new Vector3(x, y, z));
                }
            }
        }
    }

    private int CheckSpot(float x, float y, float z)
    {
        if ((x == 0 || x == (_grid.size.x - 1))
        ||  (y == 0 || y == (_grid.size.y - 1))
        ||  (z == 0 || z == (_grid.size.z - 1)))
            return _grid.BORDER;

        var collidersInThisSpot = Physics.OverlapSphere(new Vector3(x, y, z), 0.35f, mask);
        if (collidersInThisSpot.Length > 0 || collidersInThisSpot == null)
            return _grid.SOLID;
        else
            return CheckAroundSpot(new Vector3(x, y, z));
    }

    private int CheckAroundSpot(Vector3 position)
    {
        for (int i = 0; i < _dir.all.Length; i++)
        {
            if (Check(position, _dir.all[i]))
            {
                // something hit in any downward direction?
                if(i == 1  
                || i == 7  
                || i == 9  
                || i == 11 
                || i == 13 
                || i == 17 
                || i == 21 
                || i == 23 
                || i == 25)
                    return _grid.GROUND;
                // something hit in any direction?
                else
                    return _grid.SURFACE;
            }
        }
        // didnt hit a single thing
        return _grid.AIR;
    }

    private bool Check(Vector3 position, Vector3 direction)
    {
        return Physics.Raycast(position, direction, 0.9f, mask);
    }
}
