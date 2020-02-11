using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/gravity")]
public class Gravity : PlayerAction
{
    public StatVector3 gravity; private Vector3 actualGravity;
    public float timeToEvalCurve = 0;
    public AnimationCurve curve = AnimationCurve.Constant(0, 1, 1);

    private float timer = 0;
    private float curvePosition, eval;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        actualGravity = gravity.overriding ? gravity.value : sc.stats.gravity;

        SetIndex(MovementVector.ClaimIndex());
    }

    public override void Act(PlayerStateController sc)
    {
        if (timeToEvalCurve != 0)
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
        actualGravity += actualGravity * Time.deltaTime;
        MovementVector.array[GetIndex()] = actualGravity * eval;
    }

    public override void OnExit()
    {
        timer = 0;

        MovementVector.CedeIndex();

        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}