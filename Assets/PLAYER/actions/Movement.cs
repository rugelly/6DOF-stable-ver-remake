using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/movement input")]
public class Movement : PlayerAction
{
    public float moveSpeed = StatUtil.FLOAT;
    public float accelTime = StatUtil.FLOAT;

    [SerializeField] // debug
    private float _xTimer, _yTimer;
    [SerializeField] // debug
    private int movementIndex;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");
        movementIndex = MovementVector.ClaimIndex();

        // TEMPLATE: StatUtil.CheckAndSet(sc, ref VARIABLE, StatUtil.TYPECONST, sc.stats.VARIABLE, VARIABLE);
        StatUtil.CheckAndSet(sc, ref moveSpeed, StatUtil.FLOAT, sc.stats.moveSpeed, moveSpeed);
        StatUtil.CheckAndSet(sc, ref accelTime, StatUtil.FLOAT, sc.stats.accelTime, accelTime);
    }

    public override void Act(PlayerStateController sc)
    {
        var input = sc._input.move;
        input = Vector2.ClampMagnitude(input, 1);
        input *= moveSpeed;
        
        var xMult = TimerIncOrReset(input.x, ref _xTimer, sc._input.oppositeHorizontalInput);
        var yMult = TimerIncOrReset(input.y, ref _yTimer, sc._input.oppositeVerticalInput);

        var converted = new Vector3(input.x, 0, input.y);
        var actual = sc.transform.TransformDirection(converted);

        MovementVector.array[movementIndex] = actual * Time.deltaTime;
    }

    private float TimerIncOrReset(float input, ref float timer, bool condition)
    {
        if (condition)
        {
            timer = 0;
            Debug.Log("opposite movement triggered -> timer = 0");
        }

        if (input != 0)
        {
            timer += accelTime * Time.deltaTime;
            Debug.Log("input -> incrementing movement timer");
        }
        else
        {
            timer = 0;
            Debug.Log("no input -> timer = 0");
        }

        timer = Mathf.Clamp01(timer);

        return timer;
    }

    public override void OnExit()
    {
        MovementVector.CedeIndex();
        _xTimer = _yTimer = 0;
        Debug.Log(this.GetType().ToString() + " : on exit called");
    }

    
}
