using UnityEngine;

public class Motor : MonoBehaviour
{
    public TwentySixDirectionsScriptableObject _directionRef;
    public float speed;
    public float directionUnblockMoveMin;
    public float directionBlockTimerMax;

    PositionTracker _position;
    Targeting _targeting;
    CharacterController _controller;

    private void Awake()
    {
        _position = GetComponent<PositionTracker>();
        _targeting = GetComponent<Targeting>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // BlockDirectionTimerRough();

        // index = _position.GetClosestValidDirectionIndex(_targeting.targetLastPosition);
        // var direction = _directionRef.all[index];

        // Debug.DrawRay(transform.position, direction, Color.cyan);
        // _controller.Move(direction * speed * Time.deltaTime);
    }

    public void Move(Vector3 target)
    {
        BlockDirectionTimerRough();

        index = _position.GetClosestValidDirectionIndex(target);
        var direction = _directionRef.all[index];

        Debug.DrawRay(transform.position, direction, Color.cyan);
        _controller.Move(direction * speed * Time.deltaTime);
    }

    // TODO: make this nice actually pls
    Vector3 storedPos;
    float timer;
    int storedIndex;
    int index;
    bool toggle;
    bool block;
    void BlockDirectionTimerRough()
    {
        var pos = transform.position;
        if (!toggle)
        {
            storedPos = pos;
            timer = 0;
            toggle = true;
        }
        else
        {
            if (Vector3.Distance(pos, storedPos) > directionUnblockMoveMin)
            {
                block = false;
                toggle = false;
            }

            if (timer > directionBlockTimerMax)
            {
                storedIndex = index;
                block = true;
                toggle = false;
            }

            timer += 1 * Time.deltaTime;
        }

        if (block)
            _position.moveableDirections[storedIndex] = false;
    }
}
