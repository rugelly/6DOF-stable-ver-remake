using UnityEngine;

[RequireComponent(typeof(Targeting))]
public class TargetBasedPosition : MonoBehaviour
{
    public float distanceFromTarget;
    public float preference;

    Context _context;
    CheckDirections _checkDirections;
    Targeting _targeting;

    private void OnEnable()
    {
        _context = GetComponent<Context>();
        _checkDirections = GetComponent<CheckDirections>();
        _targeting = GetComponent<Targeting>();
    }

    private void Update()
    {
        var currentDistance = (transform.position - _targeting.targetLastPosition).sqrMagnitude;

        var variableDistanceFromTarget = _targeting.inLOS ? distanceFromTarget : 0;

        if (currentDistance != variableDistanceFromTarget)
        {
            _context.SetPreference(
                _context.like, 
                _checkDirections.GetClosestValidDirectionIndex(_targeting.targetLastPosition),
                preference);
        }
    }
}
