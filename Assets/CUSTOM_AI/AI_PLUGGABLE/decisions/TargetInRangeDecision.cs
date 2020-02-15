using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/decisions/target in range")]
public class TargetInRangeDecision : AIDecision
{
    public RangeType rangeType;
    public float customRange;
    private float range;

    public override bool Decide(AIStateController controller)
    {
        switch (rangeType)
        {
            case RangeType.sight:
                range = controller.stats.sightRange;
                break;
            case RangeType.detect:
                range = controller.stats.detectTargetRange;
                break;
            case RangeType.custom:
                range = customRange;
                break;
        }
        return DistanceCheck(controller, range);
    }

    private bool DistanceCheck(AIStateController controller, float r)
    {
        var distance = Vector3.Distance(controller.target.obj.transform.position, controller.transform.position);
        return distance <= r * r;
    }
}