using UnityEngine;
using PluggableFSM;

[CreateAssetMenu(menuName=Paths.connections + "fire grapple input")]
public class FireGrapple : Connection
{
    private RaycastHit hit;
    private Vector3 zero = Vector3.zero;

    private bool CheckLine(Controller controller)
    {
        var hitSomething = Physics.SphereCast
        (
            controller.grappleShootPoint.position,
            controller.stats.grappleCheckRadius,
            controller.cam.forward,
            out hit,
            controller.stats.grappleMaxDistance,
            controller.stats.grappleMask
        );

        controller.line.SetPosition(0, controller.grappleShootPoint.position);
        if (hitSomething)
            controller.line.SetPosition(1, hit.point);
        else
            controller.line.SetPosition(1, controller.cam.forward * controller.stats.grappleMaxDistance);

        controller.grappleHitPoint = hit.point;

        return hitSomething;
    }

    public override bool Eval(Controller controller)
    {
        if (controller.input.grapple)
            if (CheckLine(controller))
                return true;
        
        return false;
    }
}