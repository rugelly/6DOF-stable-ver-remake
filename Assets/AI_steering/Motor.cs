using UnityEngine;

public class Motor : MonoBehaviour
{
    public float speed;
    private CheckDirections _checkDirection;
    private Targeting _targeting;
    private CharacterController _controller;

    private void Awake()
    {
        _checkDirection = GetComponent<CheckDirections>();
        _targeting = GetComponent<Targeting>();
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void Update()
    {
        var index = _checkDirection.GetClosestValidDirectionIndex(_targeting._targetLastPosition);
        var direction = _checkDirection.directionReference[index];
        Debug.DrawRay(transform.position, direction, Color.cyan);
        _controller.Move(direction * speed * Time.deltaTime);
    }
}
