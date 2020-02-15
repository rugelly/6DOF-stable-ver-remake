using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/decisions/target in LOS")]
public class TargetInLOSDecision : AIDecision
{
    float timer;

    public override bool Decide(AIStateController controller)
    {
        if (InFOV(
                controller.target,
                controller.transform.TransformDirection(controller.eye),
                controller.stats.FOV))
        {
            if (timer > controller.stats.sightTargetCheckRate)
            {
                timer = 0;
                return InLOS(controller.target);
            }
            timer += 1 * Time.deltaTime;
        }
        return false;
    }

    private bool InFOV(Target t, Vector3 e, float fov)
    {
        Vector3 toTarget = (t.obj.transform.position - e).normalized;
        return Vector3.Dot(toTarget, e) > fov;
    }

    private bool InLOS(Target t)
    {
        var _castPoint = t.obj.transform.position;
        var _height = t.col.bounds.max.y - t.col.bounds.min.y;
        var _top = t.col.bounds.center + (Vector3.up * (_height * 0.3f));
        var _mid = t.col.bounds.center;
        var _bot = t.col.bounds.center - (Vector3.up * (_height * 0.3f));

        bool[] _casts = {
            ObjLinecast(_castPoint, _top, t.col),
            ObjLinecast(_castPoint, _mid, t.col),
            ObjLinecast(_castPoint, _bot, t.col)
        };

        return _casts.Contains(true);
    }

    private bool ObjLinecast(Vector3 s, Vector3 e, Collider t)
    {
        Physics.Linecast(s, e, out RaycastHit hit);
        var result = hit.collider == t;
        var colour = result ? Color.green : Color.red;
        Debug.DrawLine(s, hit.point, colour);
        return result;
    }
}