using PluggableFSM;
using UnityEngine;

[CreateAssetMenu(menuName = Paths.actions + "mouse look")]
public class Rotater : Action
{
    public StatVector2 sens;
    private Vector2 actualSens;
    public StatVector2 clamp;
    private Vector2 actualClamp;

    private Vector2 rotation;

    public override void Enter(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on enter called");

        rotation = controller.savedRotation;

        actualSens = sens.overriding ? sens.value : controller.stats.lookSensitivity;
        actualClamp = clamp.overriding ? clamp.value : controller.stats.lookClamp;
    }

    public override void Tick(Controller controller)
    {
        rotation.x += controller.input.look.x * actualSens.x;
        rotation.y += controller.input.look.y * actualSens.y;
        rotation.x = Mathf.Clamp(rotation.x, actualClamp.x, actualClamp.y);

        controller.transform.eulerAngles = new Vector2(0, rotation.y);
        controller.cam.transform.localEulerAngles = new Vector2(rotation.x, 0);
    }

    public override void Exit(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on exit called");

        controller.savedRotation = rotation;
    }
}