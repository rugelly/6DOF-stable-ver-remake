using UnityEngine;

[CreateAssetMenu(menuName="CUSTOM/DIRECTION REF")]
public class TwentySixDirections : ScriptableObject
{
    public Vector3[] all = 
    {
        Vector3.up,      Vector3.down,
        Vector3.forward, Vector3.back,
        Vector3.right,   Vector3.left,

        Vector3Diagonal.rghtUpForw, Vector3Diagonal.rghtDnForw,
        Vector3Diagonal.leftUpForw, Vector3Diagonal.leftDnForw,
        Vector3Diagonal.rghtUpBack, Vector3Diagonal.rghtDnBack,
        Vector3Diagonal.leftUpBack, Vector3Diagonal.leftDnBack,

        Vector3Diagonal.rghtForw, Vector3Diagonal.rghtBack,
        Vector3Diagonal.leftForw, Vector3Diagonal.leftBack,
        
        Vector3Diagonal.rghtUp, Vector3Diagonal.rghtDn,
        Vector3Diagonal.leftUp, Vector3Diagonal.leftDn,

        Vector3Diagonal.forwUp, Vector3Diagonal.forwDn,
        Vector3Diagonal.backUp, Vector3Diagonal.backDn
    };
}
