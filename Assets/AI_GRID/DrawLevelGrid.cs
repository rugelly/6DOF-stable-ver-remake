using UnityEngine;

public class DrawLevelGrid : MonoBehaviour
{
    public bool draw;

    private void OnDrawGizmos()
    {
    if (Application.isPlaying)
    {
        VolumeCheck _vc = GetComponent<VolumeCheck>();

        if (draw)
        {
            Color colour;

            for (int l = 0; l < _vc.length; l++)
            {
                for (int h = 0; h < _vc.height; h++)
                {
                    for (int w = 0; w < _vc.width; w++)
                    {
                        var thisSpot = _vc.statusGrid[l,h,w];

                        if (thisSpot == _vc.AIR)
                            colour = Color.clear;
                        else if (thisSpot == _vc.GROUND)
                            colour = Color.green;
                        else if (thisSpot == _vc.SURFACE)
                            colour = Color.blue;
                        else if (thisSpot == _vc.SOLID)
                            colour = Color.red;
                        else
                            colour = Color.magenta;

                        Gizmos.color = colour;
                        Gizmos.DrawWireCube(new Vector3(l, h, w) + transform.position, Vector3.one / 2);
                    }
                }
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                transform.position + new Vector3(_vc.length / 2, _vc.height / 2, _vc.width / 2),
                new Vector3(_vc.length, _vc.height, _vc.width));
        }
    }
    }
}
