using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/stats")]
public class PlayerStats : ScriptableObject
{
    public float moveSpeed;
    public float accelTime;
    public Vector2 lookSensitivity;
    public Vector2 lookClamp; // x = min, y = max
    public Vector3 jumpStrength;
    public Vector3 gravity;
}