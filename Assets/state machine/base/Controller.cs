using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace PluggableFSM
{
public class Controller : MonoBehaviour
{
    [SerializeField]
    private List<State> stack = new List<State>();
    public int current {get { return stack.Count - 1; } }
    [ReadOnly] public int currentTest;
    public int maxSize;
    [Space]
    public PlayerStats stats;
    public Transform cam;
    public Transform grappleShootPoint;
    public LineRenderer line;
    
    [HideInInspector] public bool callEnterNextFrame;
    [ReadOnly] public bool grounded;
    private bool storedGrounded;
    public State currentState {get { return stack[current]; } }

    [HideInInspector] public CharacterController cc;
    [HideInInspector] public PlayerInput input;

    [ReadOnly] public Vector3 grappleHitPoint;
    [ReadOnly] public Vector2 savedRotation;
    [ReadOnly] public Vector2 momentum;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();

        callEnterNextFrame = true;

        // zero out some variables
        savedRotation = new Vector2(cam.transform.localEulerAngles.y, transform.eulerAngles.x);
    }

    private void Update()
    {
        if (callEnterNextFrame)
        {
            currentState.Enter(this);
            callEnterNextFrame = false;
        }

        currentState.Tick(this);

        currentTest = current;

        if (storedGrounded != cc.isGrounded)
            StartCoroutine(ChangeGroundedState());

        storedGrounded = cc.isGrounded;
    }

    IEnumerator ChangeGroundedState()
    {
        yield return new WaitForSeconds(0.07f);
        grounded = cc.isGrounded;
    }

    public void Pop()
    {
        if (stack.Count > 1)
            stack.RemoveAt(current);
    }

    // NOTE: 
    // make sure you call [controller.Add] instead of
    // calling [controller.stack.Add]
    public void Add(State s)
    {
        stack.Add(s);
        Trim();
    }

    private void Trim()
    {
        if (stack.Count > maxSize)
            stack.RemoveAt(0);
    }
}
}