using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    VolumeCheck _volume;
    public TwentySixDirectionsScriptableObject _directionRef;
    public WalksOn walksOn;

    public Vector3 currentGridPos;
    public bool[] moveableDirections;

    private void OnEnable()
    {
        _volume = FindObjectOfType<VolumeCheck>();
    }

    private void Update()
    {
        var pos = transform.position;
        currentGridPos = new Vector3(
            Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));

        // check every position around current against the status grid
        for (int i = 0; i < _directionRef.all.Length; i++)
        {
            var checkIndex = currentGridPos + _directionRef.all[i];
            checkIndex = new Vector3(
                Mathf.Clamp(checkIndex.x, 0, _volume.length),
                Mathf.Clamp(checkIndex.y, 0, _volume.height),
                Mathf.Clamp(checkIndex.z, 0, _volume.width));
                
            var status = _volume.statusGrid[(int)checkIndex.x, (int)checkIndex.y, (int)checkIndex.z];

            moveableDirections[i] = CheckType(status);
        }
    }

    private bool CheckType(int status)
    {
        switch (walksOn)
        {
            case WalksOn.all:
                return status != _volume.SOLID ? true : false;
            case WalksOn.airOnly:
                return status == _volume.AIR ? true : false;
            case WalksOn.surfaceOnly:
                return status == _volume.SURFACE || status == _volume.GROUND ? true : false;
            case WalksOn.groundOnly:
                return status == _volume.GROUND ? true : false;
            default:
                return false;
        }
    }
}
