using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/movement input")]
public class Movement : PlayerAction
{
    public StatFloat moveSpeed; private float actualMoveSpeed;
    public StatFloat accelTime; private float actualAccelTime;

    [SerializeField] // debug
    private float _xTimer, _yTimer;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        SetIndex(MovementVector.ClaimIndex());

        actualMoveSpeed = moveSpeed.overriding ? moveSpeed.value : sc.stats.moveSpeed;
        actualAccelTime = accelTime.overriding ? accelTime.value : sc.stats.accelTime;
    }

    public override void Act(PlayerStateController sc)
    {
        var input = sc._input.move;
        input = Vector2.ClampMagnitude(input, 1);
        input *= actualMoveSpeed;
        
        var xMult = TimerIncOrReset(input.x, ref _xTimer, sc._input.oppositeHorizontalInput);
        var yMult = TimerIncOrReset(input.y, ref _yTimer, sc._input.oppositeVerticalInput);

        var converted = new Vector3(input.x, 0, input.y);
        var final = sc.transform.TransformDirection(converted);

        MovementVector.array[GetIndex()] = final;
    }

    private float TimerIncOrReset(float input, ref float timer, bool condition)
    {
        if (condition)
        {
            timer = 0;
            Debug.Log("opposite movement triggered -> timer = 0");
        }

        if (input != 0)
            timer += actualAccelTime * Time.deltaTime;
        else
            timer = 0;

        timer = Mathf.Clamp(timer, 0, actualAccelTime);

        return timer;
    }

    public override void OnExit()
    {
        MovementVector.CedeIndex();

        _xTimer = _yTimer = 0;

        Debug.Log(this.GetType().ToString() + " : on exit called");
    }

    
}
