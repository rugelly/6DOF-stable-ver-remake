using UnityEngine;

public class Motor : MonoBehaviour
{
    public TwentySixDirectionsScriptableObject _directionRef;
    public float speed;
    public float directionUnblockMoveMin;
    public float directionBlockTimerMax;

    PositionTracker _position;
    CharacterController _controller;

    private void Awake()
    {
        _position = GetComponent<PositionTracker>();
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 target)
    {
        BlockDirectionTimerRough();

        index = _position.GetClosestValidDirectionIndex(target);
        var direction = _directionRef.all[index];

        Debug.DrawRay(transform.position, direction, Color.cyan);
        _controller.Move(direction * speed * Time.deltaTime);
    }

    // TODO: does this even work??
    float timerRandomDir;
    // int index; // already exists
    public float chooseNewDirectionTime;
    public void MoveRandom()
    {
        BlockDirectionTimerRough();

        if (timerRandomDir == chooseNewDirectionTime)
            index = Random.Range(0, 26);
        while (_position.moveableDirections[index] == false)
        {
            index = Random.Range(0, 26);
        }
        var direction = _directionRef.all[index];
        Debug.DrawRay(transform.position, direction, Color.cyan);
        _controller.Move(direction * speed * Time.deltaTime);        

        timerRandomDir += chooseNewDirectionTime * Time.deltaTime;
        chooseNewDirectionTime = Mathf.Clamp(timerRandomDir, 0, chooseNewDirectionTime);
    }

    // TODO: clean this up
    // TODO: more configurable stuff eg: max num of sequential directions to block before resetting
    Vector3 storedPos;
    float timerBlockDir;
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
            timerBlockDir = 0;
            toggle = true;
        }
        else
        {
            if (Vector3.Distance(pos, storedPos) > directionUnblockMoveMin)
            {
                block = false;
                toggle = false;
            }

            if (timerBlockDir > directionBlockTimerMax)
            {
                storedIndex = index;
                block = true;
                toggle = false;
            }

            timerBlockDir += 1 * Time.deltaTime;
        }

        if (block)
            _position.moveableDirections[storedIndex] = false;
    }
}
