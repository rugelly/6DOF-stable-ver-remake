using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/decisions/target in range")]
public class TargetInRangeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        float range = SightOrDetect ? controller.stats.sightRange : controller.stats.detectTargetRange;
        return DistanceCheck(controller, range);
    }

    [Header("true: SIGHT range, false: DETECT range")]
    public bool SightOrDetect;

    private bool DistanceCheck(StateController controller, float range)
    {
        var distance = Vector3.Distance(controller.target.obj.transform.position, controller.transform.position);
        return distance <= range * range;
    }
}