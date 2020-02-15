using UnityEngine;

public class AIStateController : MonoBehaviour
{
    public AIState currentState;
    public EnemyStats stats;
    public Target target;
    public Vector3 eye;
    public AIState remainState;

    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Motor _motor;

    private void Awake()
    {
        _motor = GetComponent<Motor>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(AIState nextState)
    {
        if (nextState != remainState)
        {
            OnExitState();
            currentState = nextState;
        }
    }

    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
        foreach (var action in currentState.actions)
        {
            action.OnExit();
        }
        // StartCoroutine(ActionExitMethods());
    }

    // private IEnumerator ActionExitMethods()
    // {
    //     foreach (var action in currentState.actions)
    //     {
    //         action.OnExit();
    //     }
    //     yield return null;
    // }
}