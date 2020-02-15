using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

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
    public static List<Vector3> array = new List<Vector3>();

    public static IEnumerator DecayingForce(Vector3 vector, float decayTime, float decayAccel, 
                                            Action<bool> callback)
    {
        var timer = decayTime;
        var mult = 1f;
        var force = vector;
        var accel = decayAccel;

        while (timer > 0)
        {
            array.Add(force * mult);
            timer -= accel * Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, decayTime);
            accel += accel * Time.deltaTime;
            mult = timer / decayTime;
            callback(true);
            yield return null;
        }
        callback(false);
    }
}