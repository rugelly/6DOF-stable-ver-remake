using UnityEngine;
using System;
using System.Linq;

public class Context : MonoBehaviour
{
    [HideInInspector] public float[] like;
    [HideInInspector] public float[] hate;
    // [HideInInspector] public Vector3[] directionReference;

    private void Awake()
    {
        // ConstructArrays();
    }

    // starting from the highest value like->lowest
    // the first direction where like > hate is the direction we go
    // if none are good, return -1
    public int IdealDirection()
    {
        var sortedLike = like;
        var sortedHate = hate;
        Array.Sort(sortedLike);
        Array.Sort(sortedHate);

        for (int i = 0; i < like.Length; i++)
        {
            if (sortedLike[i] > sortedHate[i])
                return Array.IndexOf(like, sortedLike[i]);
        }
        return -1;
    }

    public void SetPreference(float[] array, int i, float amount)
    {
        // array[i] = amount > array[i] ? amount : array[i];
        array[i] = amount;
    }

    private void Clear(float[] chosenArray)
    {
        for (int i = 0; i < chosenArray.Length; i++)
        {
            chosenArray[i] = 0;
        }
    }

    // private void ConstructArrays()
    // {
    //     directionReference = new Vector3[26] 
    //     {
    //     Vector3.up,      Vector3.down,
    //     Vector3.forward, Vector3.back,
    //     Vector3.right,   Vector3.left,

    //     Vector3Diagonal.rghtUpForw, Vector3Diagonal.rghtDnForw,
    //     Vector3Diagonal.leftUpForw, Vector3Diagonal.leftDnForw,
    //     Vector3Diagonal.rghtUpBack, Vector3Diagonal.rghtDnBack,
    //     Vector3Diagonal.leftUpBack, Vector3Diagonal.leftDnBack,

    //     Vector3Diagonal.rghtForw, Vector3Diagonal.rghtBack,
    //     Vector3Diagonal.leftForw, Vector3Diagonal.leftBack,
        
    //     Vector3Diagonal.rghtUp, Vector3Diagonal.rghtDn,
    //     Vector3Diagonal.leftUp, Vector3Diagonal.leftDn,

    //     Vector3Diagonal.forwUp, Vector3Diagonal.forwDn,
    //     Vector3Diagonal.backUp, Vector3Diagonal.backDn
    //     };

    //     like = new float[directionReference.Length];
    //     hate = new float[directionReference.Length];
    // }

    // private float split = 0.03f;

    // private void DebugDrawContext()
    // {
    //     for (int i = 0; i < directionReference.Length; i++)
    //     {
    //         Debug.DrawRay(transform.position + new Vector3( split,  split,  split), directionReference[i] * like[i], Color.cyan);
    //         Debug.DrawRay(transform.position + new Vector3(-split, -split, -split), directionReference[i] * hate[i], Color.magenta);
    //     }
    // }

    // private void Update()
    // {
    //     DebugDrawContext();
    // }

    private void LateUpdate()
    {
        Clear(like);
        Clear(hate);
    }

    
}
