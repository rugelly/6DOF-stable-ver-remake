using UnityEngine;
using PluggableFSM;

[CreateAssetMenu(menuName = Paths.connections + "reached grapple point")]
public class ReachedGrapplePoint : Connection
{
    public override bool Eval(Controller controller)
    {
        var distance = Vector3.Distance(controller.grappleShootPoint.position, controller.grappleHitPoint);
        return distance < 0.8f;
    }
}
