using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class VolumeCheck : MonoBehaviour
{
    public Vector3 bounds;
    // public LayerMask mask;
    // public float testRadius;
    private int[,,] statusGrid;
    public GameObject prefab;
    public bool delete, generate, generated;
    private const int 
        AIR     = 0, 
        SOLID   = 9, 
        GROUND  = 2, 
        SURFACE = 3;

    private void Update()
    {
    if (!Application.isPlaying)
    {
        if (delete)
        {
        // for (int i = 0; i < 32; i++)
        while (transform.childCount != 0)
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        generated = false;
        delete = false;
        }

        if (generate && !generated)
        {
        statusGrid = new int[Mathf.FloorToInt(bounds.x),Mathf.FloorToInt(bounds.y),Mathf.FloorToInt(bounds.z)];

        var prefabCount = 0;

        for (int l = 0; l < statusGrid.GetLength(0); l++)
        {
            for (int h = 0; h < statusGrid.GetLength(1); h++)
            {
                for (int w = 0; w < statusGrid.GetLength(2); w++)
                {
                    UnityEditor.PrefabUtility.InstantiatePrefab(prefab, transform);
                    
                    var pf = transform.GetChild(prefabCount);
                    pf.position = new Vector3(l, h, w) + transform.position;
                    prefabCount++;
                }
            }
        }
        generated = true;
        generate = false;
        }
    }
    }

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
