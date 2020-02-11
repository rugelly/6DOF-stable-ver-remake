using UnityEditor;
using UnityEngine;

// public class StatUtil
// {
//     // public const float FLOAT = -0.001f;
//     // public const int INT = -69;

//     // public static T CheckAndSet<T>(PlayerStateController sc, T toCheck, T overrdCheck, T baseVal, T overrdVal)
//     // {
//     //     return toCheck.Equals(overrdCheck) ? baseVal : overrdVal;
//     //     // toCheck = baseVal;
//     // }
// }

// NOTE: because generics cannot be serialized in editor until 2020 version (in alpha currently)
// can currently extend the class for each type you want and write a editorPropertyDrawer for it
[System.Serializable]
public class Stat<T>
{
    public T value;
    public bool overriding;

    public Stat(T val, bool overriden)
    {
        value = val;
        overriding = overriden;
    }
}
[System.Serializable]
public class StatInt : Stat<int>
{
    public StatInt(int val, bool overriden) : base(val, overriden)
    {value = val; overriding = overriden;}
}
[System.Serializable]
public class StatFloat : Stat<float>
{
    public StatFloat(float val, bool overriden) : base(val, overriden)
    {value = val; overriding = overriden;}
}
[System.Serializable]
public class StatVector3 : Stat<Vector3>
{
    public StatVector3(Vector3 val, bool overriden) : base(val, overriden)
    {value = val; overriding = overriden;}
}
[System.Serializable]
public class StatVector2 : Stat<Vector2>
{
    public StatVector2(Vector2 val, bool overriden) : base(val, overriden)
    {value = val; overriding = overriden;}
}

public class MovementVector
{
    private static Vector3 empty = Vector3.zero;

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
        var currentSpot = _index;
        _index += 1;
        return currentSpot;
    }

    public static void CedeIndex()
    {
        array[_index] = empty;
        _index -= 1;
    }

    public static Vector3[] array = new Vector3[20];
}