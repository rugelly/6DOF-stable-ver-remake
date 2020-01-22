using System;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class VolumeCheck : MonoBehaviour
{
    public TwentySixDirections _dir;
    public Vector3 bounds;
    public LayerMask mask;
    [SerializeField]
    private int[,,] statusGrid;
    [SerializeField]
    private int[,,] coordGrid;

    /// PREFAB STUFF DEPRECATED
    // public GameObject prefab;

    public bool generate;
    
    private const int 
        AIR     = 0, 
        SOLID   = 9, 
        GROUND  = 2, 
        SURFACE = 3;

    private void Update()
    {
    if (!Application.isPlaying)
    {
        /// PREFAB STUFF DEPRECATED
        // if (delete)
        // {
        // // for (int i = 0; i < 32; i++)
        // while (transform.childCount != 0)
        // {
        //     foreach (Transform child in transform)
        //     {
        //         DestroyImmediate(child.gameObject);
        //     }
        // }
        // generated = false;
        // delete = false;
        // }

        if (generate)
        {
            coordGrid = new int[Mathf.FloorToInt(bounds.x),Mathf.FloorToInt(bounds.y),Mathf.FloorToInt(bounds.z)];
            statusGrid = coordGrid;

            /// PREFAB STUFF DEPRECATED
            // GeneratePrefabs();

            CheckSpotsAndMarkStatusGrid();
        
        // generated = true;
        generate = false;
        }
    }
    }

    private void CheckSpotsAndMarkStatusGrid()
    {
        for (int l = 0; l < coordGrid.GetLength(0); l++)
        {
            for (int h = 0; h < coordGrid.GetLength(1); h++)
            {
                for (int w = 0; w < coordGrid.GetLength(2); w++)
                {
                    if (ThisSpotFull(l, h, w))
                        statusGrid[l,h,w] = SOLID;
                    else
                        statusGrid[l,h,w] = CheckAroundSpot(new Vector3(l, h, w));
                }
            }
        }
    }

    private int CheckAroundSpot(Vector3 position)
    {
        for (int i = 0; i < _dir.all.Length; i++)
        {
            if (Check(position, _dir.all[i]))
            {
                if (i == 1  ||
                    i == 7  ||
                    i == 9  ||
                    i == 11 ||
                    i == 13 ||
                    i == 17 ||
                    i == 21 ||
                    i == 23 ||
                    i == 25)
                // something hit in any downward direction?
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

    private void OnDrawGizmosSelected()
    {
        Color colour;

        for (int l = 0; l < coordGrid.GetLength(0); l++)
        {
        for (int h = 0; h < coordGrid.GetLength(1); h++)
        {
        for (int w = 0; w < coordGrid.GetLength(2); w++)
        {
            var thisSpot = statusGrid[l,h,w];

            if (thisSpot == AIR)
                colour = Color.clear;
            else if (thisSpot == GROUND)
                colour = Color.green;
            else if (thisSpot == SURFACE)
                colour = Color.blue;
            else if (thisSpot == SOLID)
                colour = Color.red;
            else
                colour = Color.magenta;

            Gizmos.color = colour;
            Gizmos.DrawWireCube(new Vector3(l, h, w) + transform.position, Vector3.one / 2);
        }
        }
        }
    }

    /// PREFAB STUFF DEPRECATED
    // private void GeneratePrefabs()
    // {
    //     var prefabCount = 0;

    //     for (int l = 0; l < coordGrid.GetLength(0); l++)
    //     {
    //         for (int h = 0; h < coordGrid.GetLength(1); h++)
    //         {
    //             for (int w = 0; w < coordGrid.GetLength(2); w++)
    //             {
    //                 UnityEditor.PrefabUtility.InstantiatePrefab(prefab, transform);
                    
    //                 var pf = transform.GetChild(prefabCount);
    //                 pf.position = new Vector3(l, h, w) + transform.position;
    //                 prefabCount++;
    //             }
    //         }
    //     }
    // }

    /// OLD
    // private int SpotCheck(float x, float y, float z)
    // {
    //     var colliders = Physics.OverlapSphere(transform.position + new Vector3(x, y, z), testRadius, mask);
        
    //     if (colliders == null || colliders.Length == 0)
    //     {
    //         return AIR;
    //     }
    //     else
    //     {
    //         int[] spotTotal = new int[colliders.Length];

    //         for (int i = 0; i < colliders.Length; i++)
    //         {
    //             spotTotal[i] = ColliderPositionCheck(colliders[i], new Vector3(x, y, z));
    //         }

    //         if (spotTotal.Contains(SOLID))
    //             return SOLID;
    //         else if (spotTotal.Contains(GROUND))
    //             return GROUND;
    //         else
    //             return SURFACE;
    //     }
    // }

    /// OLD
    // private int ColliderPositionCheck(Collider collider, Vector3 castPosition)
    // {
    //     var colPos = collider.transform.position;
    //     if (colPos == castPosition)
    //         return SOLID;
    //     // else if (colPos.x == castPosition.x || colPos.z == castPosition.z)
    //     else
    //         if (colPos.y < castPosition.y)
    //             return GROUND;
    //         else
    //             return SURFACE;
        
    // }

    /// OLD
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(transform.position, bounds);

    //     for (int i = 0; i < statusGrid.GetLength(0); i++)
    //     {
    //         for (int ii = 0; ii < statusGrid.GetLength(1); ii++)
    //         {
    //             for (int iii = 0; iii < statusGrid.GetLength(2); iii++)
    //             {
    //                 Color colour;
    //                 var s = 0.5f;
    //                 Vector3 size = new Vector3(s,s,s);
    //                 if (statusGrid[i,ii,iii] == AIR)
    //                     colour = Color.clear;
    //                 else if (statusGrid[i,ii,iii] == SOLID)
    //                     colour = Color.green;
    //                 else if (statusGrid[i,ii,iii] == GROUND)
    //                     colour = Color.black;
    //                 else
    //                     colour = Color.magenta;

    //                 Gizmos.color = colour;
    //                 Gizmos.DrawWireCube(
    //                     new Vector3(i + transform.position.x, ii + transform.position.y, iii + transform.position.z),
    //                     size);
    //             }
    //         }
    //     }
    // }


}
