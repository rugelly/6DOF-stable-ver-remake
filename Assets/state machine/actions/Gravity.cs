using PluggableFSM;
using UnityEngine;

[CreateAssetMenu(menuName = Paths.actions + "gravity")]
public class Gravity : Action
{
    public StatVector3 gravity;
    private Vector3 actualGravity;
    public float timeToEvalCurve = 0;
    public AnimationCurve curve = AnimationCurve.Constant(0, 1, 1);

    private float timer = 0;
    private float curvePosition, eval;
    private Vector3 operatingGravity;
    private float groundedGravClamp;

    public override void Enter(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on enter called");

        actualGravity = gravity.overriding ? gravity.value : controller.stats.gravity;
        groundedGravClamp = actualGravity.magnitude;
    }

    public override void Tick(Controller controller)
    {
        if (timeToEvalCurve != 0 && !controller.grounded)
        {
            timer += timeToEvalCurve * Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, timeToEvalCurve);
            curvePosition = timer / timeToEvalCurve;
            eval = curve.Evaluate(curvePosition);
        }
        else
        {
            eval = 1;
        }
        operatingGravity += actualGravity * Time.deltaTime;
        MovementVector.array.Add(operatingGravity * eval);

        if (!controller.grounded)
            operatingGravity = Vector3.ClampMagnitude(operatingGravity, 200);
        else
            operatingGravity = Vector3.ClampMagnitude(operatingGravity, groundedGravClamp);
    }

    public override void Exit(Controller controller)
    {
        timer = eval = curvePosition = 0;
        operatingGravity = Vector3.zero;

        // Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}