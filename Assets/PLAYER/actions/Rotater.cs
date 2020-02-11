using UnityEngine;

[CreateAssetMenu(menuName = "pluggablePLAYER/actions/mouselook")]
public class Rotater : PlayerAction
{
    public StatVector2 sens; private Vector2 actualSens;
    public StatVector2 clamp; private Vector2 actualClamp;

    private Vector2 rotation;

    public override void OnEnter(PlayerStateController sc)
    {
        Debug.Log(this.GetType().ToString() + " : on enter called");

        actualSens = sens.overriding ? sens.value : sc.stats.lookSensitivity;
        actualClamp = clamp.overriding ? clamp.value : sc.stats.lookClamp;

        rotation = (Vector2)sc.transform.rotation.eulerAngles;
    }

    public override void Act(PlayerStateController sc)
    {
        rotation.x += sc._input.look.x * actualSens.x;
        rotation.y += sc._input.look.y * actualSens.y;
        rotation.x = Mathf.Clamp(rotation.x, actualClamp.x, actualClamp.y);

        sc.transform.eulerAngles = new Vector2(0, rotation.y);
        sc.cam.transform.localEulerAngles = new Vector2(rotation.x, 0);
    }

    public override void OnExit()
    {
        Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}
