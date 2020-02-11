using UnityEngine;

[CreateAssetMenu(menuName="pluggablePLAYER/decisions/grounded")]
public class GroundedDecision : PlayerDecision
{
    public override bool Decide(PlayerStateController sc)
    {
        return sc._charCont.isGrounded;
    }
}
