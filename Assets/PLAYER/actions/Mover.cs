using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/mover total movement vectors")]
public class Mover : PlayerAction
{
    private Vector3 sum;
    public Vector3 functionalTotal; // for debug purps

    public override void Act(PlayerStateController sc)
    {
        foreach (var vector in MovementVector.array)
        {
            sum += vector;
        }
        sc._charCont.Move(sum);
        functionalTotal = sum;
        sum = Vector3.zero;
    }

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");
        sum = Vector3.zero;
    }

    public override void OnExit()
    {
        sum = Vector3.zero;
        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}
