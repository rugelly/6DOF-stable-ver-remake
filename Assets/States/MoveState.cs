using UnityEngine;
using StateMachineSimple;

public class MoveState : State
{
    private Context _context;
    private Motor _motor;

    public MoveState(StateMachine stateMachine) : base(stateMachine)
    {}

    public override void OnStateEnter()
    {
        _context = stateMachine.GetComponent<Context>();
        _motor = stateMachine.GetComponent<Motor>();
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {
        
    }
}