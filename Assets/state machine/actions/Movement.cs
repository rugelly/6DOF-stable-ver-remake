using PluggableFSM;
using UnityEngine;

[CreateAssetMenu(menuName = Paths.actions + "movement input")]
public class Movement : Action
{
    private float actualMoveSpeed;
    public float moveSpeedMult = 1;
    private float actualAccelTime;
    public float accelTimeMult = 1;

    [SerializeField] // debug
    private float _xTimer, _yTimer;

    public override void Enter(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on enter called");

        _xTimer = controller.momentum.x;
        _yTimer = controller.momentum.y;

        actualMoveSpeed = controller.stats.moveSpeed * moveSpeedMult;
        actualAccelTime = controller.stats.accelTime * accelTimeMult;
    }

    public override void Tick(Controller controller)
    {
        var input = controller.input.move;
        input = Vector2.ClampMagnitude(input, 1);
        input *= actualMoveSpeed;

        var xMult = TimerIncOrReset(input.x, ref _xTimer, controller.input.oppositeHorizontalInput);
        var yMult = TimerIncOrReset(input.y, ref _yTimer, controller.input.oppositeVerticalInput);

        var converted = new Vector3(input.x * xMult, 0, input.y * yMult);
        converted = Vector3.ClampMagnitude(converted, actualMoveSpeed);
        var final = controller.transform.TransformDirection(converted);

        MovementVector.array.Add(final);
    }

    private float TimerIncOrReset(float input, ref float timer, bool condition)
    {
        if (condition)
        {
            timer = 0;
            Debug.Log("opposite movement triggered -> timer = 0");
        }

        if (input != 0)
            timer += (actualAccelTime) * Time.deltaTime;
        else
            timer = 0;

        timer = Mathf.Clamp(timer, 0, actualAccelTime);

        return timer;
    }

    public override void Exit(Controller controller)
    {
        controller.momentum = new Vector2(_xTimer, _yTimer);
        // Debug.Log(this.GetType().ToString() + " : on exit called");
    }

}