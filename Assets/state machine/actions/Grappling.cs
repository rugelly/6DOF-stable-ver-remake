using PluggableFSM;
using UnityEngine;

[CreateAssetMenu(menuName = Paths.actions + "propelled along grapple")]
public class Grappling : Action
{
    private float accel, min, max;

    public override void Tick(Controller controller)
    {
        var current = controller.transform.position;
        var target = controller.grappleHitPoint;
        var heading = Vector3.ClampMagnitude(target - current, 1);
        heading *= accel;
        controller.cc.Move(heading * Time.deltaTime);

        accel += accel * Time.deltaTime;
        accel = Mathf.Clamp(accel, min, max);
    }

    public override void Enter(Controller controller)
    {
        accel = min = controller.stats.grappleAccelMinMax.x;
        max = controller.stats.grappleAccelMinMax.y;
    }

    public override void Exit(Controller controller)
    {
        accel = 0;
    }
}