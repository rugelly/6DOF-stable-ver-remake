using UnityEngine;

public class FollowType : MonoBehaviour
{
    Targeting _targeting;
    Motor _motor;
    PositionTracker _position;

    public float distanceToKeep;
    public Vector2 rotateAround;

    private Vector3 target;

    private void OnEnable()
    {
        _targeting = GetComponent<Targeting>();
        _motor = GetComponent<Motor>();
        _position = GetComponent<PositionTracker>();
    }

    private float CheckDistance(Vector3 target)
    {
        return (transform.position - target).sqrMagnitude;
    }

    private void StayAtDistance()
    {
        var distance = CheckDistance(_targeting.targetLastPosition);
        var oppositeTarget = transform.position + (transform.position - _targeting.targetLastPosition);

        if (_targeting.inLOS) // TODO: handle LOS allowing/disallowing things in more parent logic controller
            target = distance > (distanceToKeep * distanceToKeep)
                ? _targeting.targetLastPosition : oppositeTarget;
        else
            target = _targeting.targetLastPosition;

        _motor.Move(target);
    }

    float timer;
    private void FollowRotatingPoint()
    {
        var rot = 360 * timer;
        var targetRotatingPoint = RotatePointAroundPivot(_targeting.targetLastPosition
            + (Vector3)rotateAround, _targeting.targetLastPosition, new Vector3(0, rot, 0));

        var distance = CheckDistance(targetRotatingPoint);

        if (distance < 0.5f)
            timer += 1 * Time.deltaTime;
        
        timer = Mathf.Clamp01(timer);
        timer = timer == 1 ? 0 : timer;

        _motor.Move(targetRotatingPoint);
        followRotatePoint = targetRotatingPoint;
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    Vector3 followRotatePoint;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(followRotatePoint, Vector3.one * 0.4f);
    }
}