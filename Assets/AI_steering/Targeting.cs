using UnityEngine;
using System.Linq;
using System;

public class Targeting : MonoBehaviour
{
    public float aggroRange;
    public Vector3 eye;
    public GameObject target;

    private Collider _targetCollider;
    public Vector3 _targetLastPosition;
    private string _wantedTag = "Player";

    private void Awake()
    {
        Component c;
        if (    target.TryGetComponent(typeof(BoxCollider), out c)      
            ||  target.TryGetComponent(typeof(SphereCollider), out c)   
            ||  target.TryGetComponent(typeof(CapsuleCollider), out c))

            _targetCollider = (Collider)c;
        else
            target = null;
    }

    private void Update()
    {
        if (target != null)
        {
            if (InRange())
            {
                if (LineOfSight())
                    _targetLastPosition = target.transform.position;
            }
        }
    }

    private bool InRange()
    {
        return (target.transform.position - transform.position).sqrMagnitude < aggroRange * aggroRange;
    }

    private bool LineOfSight()
    {
        var _castPoint = transform.TransformDirection(eye) + transform.position;
        var _targetHeight = _targetCollider.bounds.max.y - _targetCollider.bounds.min.y;
        var _targetTop = _targetCollider.bounds.center + (Vector3.up * (_targetHeight * 0.3f));
        var _targetMid = _targetCollider.bounds.center;
        var _targetBot = _targetCollider.bounds.center - (Vector3.up * (_targetHeight * 0.3f));

        bool[] _casts = {
            TagLinecast(_castPoint, _targetTop, _wantedTag),
            TagLinecast(_castPoint, _targetMid, _wantedTag),
            TagLinecast(_castPoint, _targetBot, _wantedTag)};

        return _casts.Contains(true);
    }

    private bool TagLinecast(Vector3 start, Vector3 end, string targetTag)
    {
        RaycastHit hit;
        Physics.Linecast(start, end, out hit);
        if (hit.collider.tag == targetTag)
            Debug.DrawLine(start, hit.point, Color.green);
        else         
            Debug.DrawLine(start, hit.point, Color.red);
        return hit.collider.tag == targetTag;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformDirection(eye) + transform.position, 0.02f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_targetLastPosition, Vector3.one * 0.9f);
    }
}
