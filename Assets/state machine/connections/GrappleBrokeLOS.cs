using UnityEngine;
using PluggableFSM;

[CreateAssetMenu(menuName=Paths.connections + "grapple broke LOS")]
public class GrappleBrokeLOS : Connection
{
    private RaycastHit hit;

    public override bool Eval(Controller controller)
    {
        var current = controller.transform.position;
        var target = controller.grappleHitPoint;
        Physics.Linecast(current, target, out hit, controller.stats.grappleMask);
        return hit.point != target;
    }
}
