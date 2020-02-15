using UnityEngine;
using PluggableFSM;

[CreateAssetMenu(menuName=Paths.connections + "stuck while grappling")]
public class StuckGrappling : Connection
{
    public override bool Eval(Controller controller)
    {
        if (Vector3.Distance(controller.grappleShootPoint.position, controller.line.GetPosition(0)) > 1)
            if (controller.cc.velocity.magnitude < controller.stats.grappleStuckSpeedCutoff)
                return true;
        return false;
    }
}
