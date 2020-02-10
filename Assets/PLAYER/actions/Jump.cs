using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/jump")]
public class Jump : PlayerAction
{
    public Vector3 strength = new Vector3(StatUtil.FLOAT, StatUtil.FLOAT, StatUtil.FLOAT);

    [SerializeField]
    private int movementIndex;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        StatUtil.CheckAndSet(sc, ref strength, new Vector3(StatUtil.FLOAT, StatUtil.FLOAT, StatUtil.FLOAT), sc.stats.jumpStrength, strength);

        movementIndex = MovementVector.ClaimIndex();
    }

    public override void Act(PlayerStateController sc)
    {
        var actual = sc.transform.TransformDirection(strength);
        MovementVector.array[movementIndex] = sc._input.jump ? actual : Vector3.zero;
    }

    

    public override void OnExit()
    {
        MovementVector.CedeIndex();
        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}