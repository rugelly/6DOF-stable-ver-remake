using UnityEngine;
using PluggableFSM;

[CreateAssetMenu(menuName=Paths.connections + "cancel grapple with input")]
public class CancelGrapple : Connection
{
    public override bool Eval(Controller controller)
    {
        if (controller.input.grapple || controller.input.jump)
            return true;

        return false;
    }
}