using UnityEngine;

namespace PluggableFSM
{
public abstract class Action : ScriptableObject
{
    public abstract void Enter(Controller controller);

    public abstract void Tick(Controller controller);
    
    public abstract void Exit(Controller controller);
}
}