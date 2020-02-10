using UnityEngine;

public abstract class PlayerAction : ScriptableObject
{
    public abstract void Act(PlayerStateController sc);

    public abstract void OnEnter(PlayerStateController sc);

    public abstract void OnExit();
}