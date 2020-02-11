using UnityEngine;

public abstract class PlayerAction : ScriptableObject
{
    [SerializeField]
    protected int MovementIndex;

    public void SetIndex(int claim)
    {
        MovementIndex = MovementVector.ClaimIndex();
    }

    public int GetIndex()
    {
        return MovementIndex;
    }

    public abstract void OnEnter(PlayerStateController sc);

    public abstract void Act(PlayerStateController sc);

    public abstract void OnExit();
}