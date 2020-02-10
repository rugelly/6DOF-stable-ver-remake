using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/gravity")]
public class Gravity : PlayerAction
{
    public Vector3 field = new Vector3(StatUtil.FLOAT, StatUtil.FLOAT, StatUtil.FLOAT);
    public float timeToEvalCurve = 0;
    public AnimationCurve curve;

    [SerializeField]
    private int movementIndex;
    private float timer = 0;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        StatUtil.CheckAndSet(sc,
                             ref field,
                             new Vector3(StatUtil.FLOAT, StatUtil.FLOAT, StatUtil.FLOAT),
                             sc.stats.gravity,
                             field);

        movementIndex = MovementVector.ClaimIndex();
    }

    public override void Act(PlayerStateController sc)
    {
        timer += timeToEvalCurve * Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, timeToEvalCurve);
        var curvePosition = timer / timeToEvalCurve;
        var eval = curve.Evaluate(curvePosition);
        MovementVector.array[movementIndex] = field * eval * Time.deltaTime;
    }

    public override void OnExit()
    {
        timer = 0;
        MovementVector.CedeIndex();
        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}