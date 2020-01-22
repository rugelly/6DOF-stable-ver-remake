using UnityEngine;

[ExecuteInEditMode]
public class PrefabUnitCheck : MonoBehaviour
{
    SphereCollider _sc;
    public TwentySixDirections _dir;
    public LayerMask mask;

    private void OnEnable()
    {
        _sc = GetComponent<SphereCollider>();

        //is there something inside this prefab? delete urself my man
        if (SpotCheck())
            DestroyImmediate(gameObject);

        // loop all directions
        for (int i = 0; i < _dir.all.Length; i++)
        {
            // something hit in any downward direction?
            if (Check(_dir.all[i]))
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
                {
                    tag = "ground";
                    break;
                }
            }
            // something hit in any direction?
            else if (Check(_dir.all[i]))
            {
                tag = "surface";
                break;
            }
            // ray hit nothing throughout entire loop
            else
            {
                tag = "air";
            }
        }
    }

    private bool SpotCheck()
    {
        var collidersInThisSpot = Physics.OverlapSphere(transform.position, _sc.radius, mask);
        if (collidersInThisSpot.Length > 0 || collidersInThisSpot == null)
            return true;
        else
            return false;
    }

    private bool Check(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, mask);
    }

    private void OnDrawGizmos()
    {
        Color colour;
        if (tag == "ground")
            colour = Color.blue;
        else if (tag == "surface")
            colour = Color.cyan;
        else if (tag == "air")
            colour = Color.clear;
        else
            colour = Color.red;

        Gizmos.color = colour;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
