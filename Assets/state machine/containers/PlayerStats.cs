using UnityEngine;
using NaughtyAttributes;
using PluggableFSM;

[CreateAssetMenu(menuName = Paths.root + "Stats")]
public class PlayerStats : ScriptableObject
{
    public float moveSpeed;
    public float accelTime;
    public Vector2 lookSensitivity;
    public Vector2 lookClamp; // x = min, y = max
    public Vector3 jumpStrength;
    public float jumpDecel;
    public Vector3 gravity;
    public float grappleCheckRadius;
    public float grappleMaxDistance;
    public LayerMask grappleMask;
    public Vector2 grappleAccelMinMax;
    public float grappleStuckSpeedCutoff;
    
}