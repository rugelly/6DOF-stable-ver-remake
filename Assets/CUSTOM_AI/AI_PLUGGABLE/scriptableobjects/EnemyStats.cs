using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
#pragma warning disable 0649 // hide "value never assigned to" warnings

    public float sightRange;

    [SerializeField]
    [Range(0, 360)]
    private int _FOV;
    public float FOV
    {
        get
        {
            // convert fov DEGREES to DOT PRODUCT ie: 45deg FOV? return 0.75f
            // with special rule for 180 degrees
            return Mathf.Clamp(_FOV == 180 ? 0 : -((_FOV * 2) / 360f) + 1, -1, 1);
        }
    }

    public float sightTargetCheckRate;

    public float detectTargetRange;

    public float maintainDistance;

    public Vector2 rotateAroundTargetPosition;

    [Range(0, 360)]
    public int rotateAroundDegrees;

    public float rotateAroundSpeed;

#pragma warning restore
}