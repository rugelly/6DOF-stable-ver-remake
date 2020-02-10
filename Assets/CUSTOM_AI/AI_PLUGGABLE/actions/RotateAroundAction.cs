using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/actions/rotate around point")]
public class RotateAroundAction : Action
{
    public override void Act(StateController controller)
    {
        controller._motor.Move(
            FollowRotatingPoint(
                controller.transform.position,
                controller.target.obj.transform.position,
                controller.stats.rotateAroundTargetPosition,
                controller.stats.rotateAroundSpeed,
                0.35f,
                controller.stats.rotateAroundDegrees));
    }

    public override void OnExit()
    {
        Debug.Log("onExit called for _rotate around action_");
    }

    float timer;
    private bool flipDirection;
    private Vector3 FollowRotatingPoint(Vector3 position, Vector3 target, Vector2 nodePosition, float rotateSpeed, float nodeTouchDistance, int degreesOfMovement)
    {
        if (Vector3.Distance(position, target) < nodeTouchDistance)
            if (flipDirection)
                timer -= rotateSpeed * Time.deltaTime;
            else
                timer += rotateSpeed * Time.deltaTime;

        timer = Mathf.Clamp01(timer);
        
        if (degreesOfMovement == 360)
            timer = timer == 1 ? 0 : timer;
        else
        {
            if (timer == 1) 
                flipDirection = true;
            else if (timer == 0) 
                flipDirection = false;
        }

        var rot = degreesOfMovement * timer;
        var targetRotatingPoint = RotatePointAroundPivot(target
            + (Vector3)nodePosition, target, new Vector3(0, rot, 0));

        return targetRotatingPoint;
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angle) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
}