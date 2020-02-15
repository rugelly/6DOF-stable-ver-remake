using PluggableFSM;
using UnityEngine;

[CreateAssetMenu(menuName = Paths.actions + "jump input")]
public class Jump : Action
{
    public StatVector3 jumpStrength;
    private Vector3 actualStrength;
    public StatFloat jumpDeceleration;
    private float actualDecel;

    public bool running;

    public override void Enter(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on enter called");

        actualStrength = jumpStrength.overriding ? jumpStrength.value : controller.stats.jumpStrength;
        actualStrength -= controller.stats.gravity;
        actualDecel = jumpDeceleration.overriding ? jumpDeceleration.value : controller.stats.jumpDecel;
    }

    public override void Tick(Controller controller)
    {
        if (controller.input.jump && !running)
            controller.StartCoroutine(MovementVector.DecayingForce(actualStrength, 1, actualDecel, 
                (test) => {running = test;}));
                // NOTE: use Awake(); inside the codeblock and then after can even write logic, 
                //       call other functions. pretty cool!
    }

    public override void Exit(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}