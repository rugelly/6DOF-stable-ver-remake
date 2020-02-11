using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/jump")]
public class Jump : PlayerAction
{
    public StatVector3 jumpStrength; private Vector3 actualJumpStrength;
    public StatFloat jumpDeceleration; private float actualJumpDeceleration;

    public float timer;
    private float timerMax;
    public float mult;

    public Vector3 final;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        actualJumpStrength = jumpStrength.overriding ? jumpStrength.value : sc.stats.jumpStrength;
        actualJumpStrength -= sc.stats.gravity;
        actualJumpDeceleration = jumpDeceleration.overriding ? jumpDeceleration.value : sc.stats.jumpDecel;

        timerMax = actualJumpStrength.magnitude;

        SetIndex(MovementVector.ClaimIndex());
    }

    public override void Act(PlayerStateController sc)
    {
        if (sc._input.jump)
        {
            final = sc.transform.TransformDirection(actualJumpStrength);
            timer = timerMax;
        }

        timer -= (actualJumpDeceleration * Time.deltaTime) * actualJumpDeceleration;
        timer = Mathf.Clamp(timer, 0, timerMax);
        mult = timer / timerMax;

        MovementVector.array[GetIndex()] = final * mult;
    }

    public override void OnExit()
    {
        MovementVector.CedeIndex();

        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}