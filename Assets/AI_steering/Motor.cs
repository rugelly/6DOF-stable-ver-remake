using UnityEngine;

public class Motor : MonoBehaviour
{
    public TwentySixDirectionsScriptableObject _directionRef;
    public float speed;
    PositionTracker _position;
    private CheckDirections _checkDirection;
    private Targeting _targeting;
    private CharacterController _controller;

    private void Awake()
    {
        _checkDirection = GetComponent<CheckDirections>();
        _position = GetComponent<PositionTracker>();
        _targeting = GetComponent<Targeting>();
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void Update()
    {
        // var index = _checkDirection.GetClosestValidDirectionIndex(_targeting._targetLastPosition);
        // var direction = _checkDirection.directionReference[index];

        var index = _position.GetClosestValidDirectionIndex(_targeting._targetLastPosition);
        var direction = _directionRef.all[index];

        Debug.DrawRay(transform.position, direction, Color.cyan);
        _controller.Move(direction * speed * Time.deltaTime);
    }
}
