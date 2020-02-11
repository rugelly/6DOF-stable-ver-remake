using UnityEngine;

[CreateAssetMenu(menuName="pluggablePLAYER/state")]
public class PlayerState : ScriptableObject
{
    public PlayerAction[] actions;
    public PlayerTransition[] transitions;

    public void UpdateState(PlayerStateController controller)
    {
        CheckTransitions(controller);
        DoActions(controller);
    }

    private void DoActions(PlayerStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(PlayerStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
                controller.TransitionToState(transitions[i].trueState);
            else
                controller.TransitionToState(transitions[i].falseState);
        }
    }
}