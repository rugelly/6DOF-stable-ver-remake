using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    public abstract void Act(AIStateController controller);

    public abstract void OnExit();
}