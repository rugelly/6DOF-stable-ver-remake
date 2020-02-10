using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/mouselook")]
public class Rotater : PlayerAction
{
    public Vector2 sens = new Vector2(StatUtil.FLOAT, StatUtil.FLOAT);
    public Vector2 clamp = new Vector2(StatUtil.FLOAT, StatUtil.FLOAT);

    private Vector2 rotation;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        StatUtil.CheckAndSet(sc, ref sens, new Vector2(StatUtil.FLOAT, StatUtil.FLOAT), sc.stats.lookSensitivity, sens);
        StatUtil.CheckAndSet(sc, ref clamp, new Vector2(StatUtil.FLOAT, StatUtil.FLOAT), sc.stats.lookClamp, clamp);

        rotation = (Vector2)sc.transform.rotation.eulerAngles;
    }

    public override void Act(PlayerStateController sc)
    {
        rotation.x += sc._input.look.x * sens.x;
        rotation.y += sc._input.look.y * sens.y;
        rotation.x = Mathf.Clamp(rotation.x, clamp.x, clamp.y);

        sc.transform.eulerAngles = new Vector2(0, rotation.y);
        sc.cam.transform.localEulerAngles = new Vector2(rotation.x, 0);
    }

    public override void OnExit()
    {
        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}
