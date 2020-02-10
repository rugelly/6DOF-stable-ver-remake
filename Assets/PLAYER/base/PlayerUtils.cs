using UnityEngine;

public class StatUtil
{
    public const float FLOAT = -0.001f;
    public const int INT = -69;

    public static void CheckAndSet<T>(PlayerStateController sc, ref T toChange, T overrdCheck, T baseVal, T overrdVal)
    {
        toChange = toChange.Equals(overrdCheck) ? baseVal : overrdVal; 
        // toChange = baseVal;
    }
}

public class MovementVector
{
    public static int index
    { 
        get { return _index; } 
        set 
        {
            value = Mathf.Clamp(value, 0, 20);
            _index = value;
        } 
    }
    private static int _index;

    public static int ClaimIndex()
    {
        _index += 1;
        return _index;
    }

    public static void CedeIndex()
    {
        array[_index] = Vector3.zero;
        _index -= 1;
    }

    public static Vector3[] array = new Vector3[20];
}