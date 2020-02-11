using UnityEngine;

public class Grapple : PlayerAction
{
    private Vector3 point;

    public override void Act(PlayerStateController sc)
    {
        if (sc._input.grapple)
        {
            if (Physics.Raycast(sc.grappleShootPoint.position, sc.cam.forward, out RaycastHit hit))
                point = hit.point;
        }
    }

    public override void OnEnter(PlayerStateController sc)
    {
        //
    }

    public override void OnExit()
    {
        //
    }
}