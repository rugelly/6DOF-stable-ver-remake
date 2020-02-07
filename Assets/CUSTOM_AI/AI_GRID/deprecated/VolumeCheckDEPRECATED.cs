using System.Collections;
using UnityEngine;

public class VolumeCheck : MonoBehaviour
{
    public TwentySixDirectionsScriptableObject _dir;
    public Vector3 bounds;
    public LayerMask mask;
    
    private int[,,] coordGrid;
    public int[,,] statusGrid;
    public int length {get{return (int)bounds.x;}}
    public int height {get{return (int)bounds.y;}}
    public int width {get{return (int)bounds.z;}}

    public bool generate;

    private const int 
        air     = 0, 
        solid   = 9, 
        ground  = 2, 
        surface = 3;
    public int AIR {get{return air;}}
    public int SOLID {get{return solid;}}
    public int GROUND {get{return ground;}}
    public int SURFACE {get{return surface;}}

    private void OnEnable()
    {
        generate = true;
    }

    private void Update()
    {
        if (generate)
        {
            generate = false;
            InitSizes();
            CheckSpotsAndMarkStatusGrid();
        }
    }

    private void InitSizes()
    {
        coordGrid = new int[length, width, height];
        statusGrid = coordGrid;
    }

    private void CheckSpotsAndMarkStatusGrid()
    {
        Debug.Log("starting generation");

        for (int l = 0; l < coordGrid.GetLength(0); l++)
        {
            StartCoroutine(InitCoroutine(l));
        }
    }

    private IEnumerator InitCoroutine(int l)
    {
        for (int h = 0; h < coordGrid.GetLength(1); h++)
        {
            for (int w = 0; w < coordGrid.GetLength(2); w++)
            {
                // convert 3d coords to an index for a 1d array
                // var flattenedIndex = 
                //     l + h * coordGrid.GetLength(0) + w * coordGrid.GetLength(0) * coordGrid.GetLength(1);

                if (ThisSpotFull(l, h, w))
                    statusGrid[l, h, w] = SOLID;
                    // statusGrid[flattenedIndex] = _grid.SOLID;
                else
                    statusGrid[l, h, w] = CheckAroundSpot(new Vector3(l, h, w));
                    // statusGrid[flattenedIndex] = CheckAroundSpot(new Vector3(l, h, w));

                yield return null;
            }
        }
        Debug.Log("slice: " + l + " of: " + (coordGrid.GetLength(0) - 1));
    }

    private int CheckAroundSpot(Vector3 position)
    {
        for (int i = 0; i < _dir.all.Length; i++)
        {
            if (Check(position + transform.position, _dir.all[i]))
            {
                // something hit in any downward direction?
                if (i == 1  ||
                    i == 7  ||
                    i == 9  ||
                    i == 11 ||
                    i == 13 ||
                    i == 17 ||
                    i == 21 ||
                    i == 23 ||
                    i == 25)
                    return GROUND;
                // something hit in any direction?
                else
                    return SURFACE;
            }
        }
        // didnt hit a single thing
        return AIR;
    }

    private bool ThisSpotFull(int x, int y, int z)
    {
        var collidersInThisSpot = Physics.OverlapSphere(transform.position + new Vector3(x, y, z), 0.2f, mask);
        if (collidersInThisSpot.Length > 0 || collidersInThisSpot == null)
            return true;
        else
            return false;
    }

    private bool Check(Vector3 position, Vector3 direction)
    {
        return Physics.Raycast(position + transform.position, direction, 0.9f, mask);
    }
}
