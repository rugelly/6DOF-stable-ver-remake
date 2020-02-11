using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerStateController : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerState remainState;

    public PlayerStats stats;
    public Transform cam;
    public Transform grappleShootPoint;
    [HideInInspector] public PlayerInput _input;
    [HideInInspector] public CharacterController _charCont;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _charCont = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        OnEnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(PlayerState nextState)
    {
        if (nextState != remainState)
        {
            OnExitState();
            currentState = nextState;
            OnEnterState(this);
        }
    }

    private void OnEnterState(PlayerStateController sc)
    {
        foreach (var action in currentState.actions)
        {
            action.OnEnter(sc);
        }
    }

    private void OnExitState()
    {
        StartCoroutine(ExitActionRoutine());
    }

    private IEnumerator ExitActionRoutine()
    {
        foreach (var action in currentState.actions)
        {
            action.OnExit();
        }
        yield return null;
    }
}