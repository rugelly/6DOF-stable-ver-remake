using UnityEngine;
using PluggableFSM;

[CreateAssetMenu(menuName=Paths.connections + "grounded check")]
public class GroundedCheck : Connection
{
    public override bool Eval(Controller controller)
    {
        return controller.grounded;
    }
}
