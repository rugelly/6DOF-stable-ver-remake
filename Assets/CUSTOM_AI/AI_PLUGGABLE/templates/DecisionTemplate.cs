using System;
using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/decisions/zztemplate")]
public class DecisionTemplate : Decision
{
    public override bool Decide(StateController controller)
    {
        return SomeMethod(controller);
    }

    private bool SomeMethod(StateController controller)
    {
        throw new NotImplementedException();
    }
}