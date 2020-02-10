using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerStateController : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerState remainState;

    public PlayerStats stats;
    public Transform cam;
    [HideInInspector] public PlayerInput _input;
    [HideInInspector] public CharacterController _charCont;

    [HideInInspector] public float stateTimeElapsed;

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

    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
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
        stateTimeElapsed = 0;
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