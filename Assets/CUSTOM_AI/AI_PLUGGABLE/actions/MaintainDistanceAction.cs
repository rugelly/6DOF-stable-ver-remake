using UnityEngine;

[CreateAssetMenu(menuName="pluggableAI/actions/maintain distance")]
public class MaintainDistanceAction : Action
{
    public float overrideDistanceToKeep;

    public override void Act(StateController controller)
    {
        var distance = Vector3.Distance(controller.target.obj.transform.position, controller.transform.position);
        var target = controller.target.obj.transform.position;
        var oppositeTarget = controller.transform.position + (controller.transform.position - controller.target.obj.transform.position);

        var keepDistance = controller.stats.maintainDistance;
        if (overrideDistanceToKeep != 0)
            keepDistance = overrideDistanceToKeep;

        var finalTarget = distance > keepDistance ? target : oppositeTarget;
        controller._motor.Move(finalTarget);
    }

    public override void OnExit()
    {
        Debug.Log("OnExit called for: _maintain distance action_");
    }
}
