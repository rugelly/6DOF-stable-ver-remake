using UnityEngine;

[CreateAssetMenu(menuName="pluggableAI/state")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColour = Color.grey;

    public void UpdateState(StateController controller)
    {
        CheckTransitions(controller);
        DoActions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
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