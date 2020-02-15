using UnityEngine;

[CreateAssetMenu(menuName="pluggableAI/state")]
public class AIState : ScriptableObject
{
    public AIAction[] actions;
    public AITransition[] transitions;
    public Color sceneGizmoColour = Color.grey;

    public void UpdateState(AIStateController controller)
    {
        CheckTransitions(controller);
        DoActions(controller);
    }

    private void DoActions(AIStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(AIStateController controller)
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