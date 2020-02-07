using System;
using UnityEngine;

[CreateAssetMenu(menuName="pluggableAI/actions/zztemplate")]
public class ActionTemplate : Action
{
    public override void Act(StateController controller)
    {
        WhateverThisDoes(controller);
    }

    public override void OnExit()
    {
        throw new NotImplementedException();
    }

    private void WhateverThisDoes(StateController controller)
    {
        throw new NotImplementedException();
    }
}
