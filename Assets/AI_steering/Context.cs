using UnityEngine;
using System;

public class Context : MonoBehaviour
{
    [HideInInspector] public float[] like;
    [HideInInspector] public float[] hate;
    [HideInInspector] public Vector3[] directionReference;

    private void Awake()
    {
        ConstructArrays();
    }

    // starting from the highest value like->lowest
    // the first direction where like > hate is the direction we go
    // if none are good, return -1
    public int IdealDirection()
    {
        for (int i = 0; i < like.Length; i++)
        {
            var iterateInt = GetSortedIndex(like, i, true);
            if (like[iterateInt] > hate[iterateInt])
                return iterateInt;
        }
        return -1;
    }

    // sort array from high -> low
    // look for the same value in the unsorted input array
    // valueWanted? 0? we want the highest. 1? second highest. etc...
    // return the index of the wanted number (eg: highest = sorted[0]) from the unsorted array
    // DEMO:
        // float[] tmparr = new float[]{-5, -35, 1, 35, 99, 3};
        // Debug.Log(tmparr[0] + " " + tmparr[1]  + " " + tmparr[2]
        // + " " + tmparr[3] + " " + tmparr[4] + " " + tmparr[5]);
        // Debug.Log(tmparr[GetSortedIndex(tmparr, 0)]);
    // DEMO END
    private int GetSortedIndex(float[] inputArray, int valueWantedIndex, bool highToLow)
    {
        var sorted = inputArray;
        Array.Sort(sorted);
        if (highToLow)
            Array.Reverse(sorted);
        return Array.IndexOf(inputArray, sorted[valueWantedIndex]);
    }

    public void SetPreference(float[] array, int i, float amount)
    {
        array[i] = amount > array[i] ? amount : array[i];
    }

    private void Clear(float[] chosenArray)
    {
        for (int i = 0; i < chosenArray.Length; i++)
        {
            chosenArray[i] = 0;
        }
    }

    private void ConstructArrays()
    {
        directionReference = new Vector3[26] 
        {
        Vector3.up,      Vector3.down,
        Vector3.forward, Vector3.back,
        Vector3.right,   Vector3.left,

        Vector3Diagonal.rghtUpForw, Vector3Diagonal.rghtDnForw,
        Vector3Diagonal.leftUpForw, Vector3Diagonal.leftDnForw,
        Vector3Diagonal.rghtUpBack, Vector3Diagonal.rghtDnBack,
        Vector3Diagonal.leftUpBack, Vector3Diagonal.leftDnBack,

        Vector3Diagonal.rghtForw, Vector3Diagonal.rghtBack,
        Vector3Diagonal.leftForw, Vector3Diagonal.leftBack,
        
        Vector3Diagonal.rghtUp, Vector3Diagonal.rghtDn,
        Vector3Diagonal.leftUp, Vector3Diagonal.leftDn,

        Vector3Diagonal.forwUp, Vector3Diagonal.forwDn,
        Vector3Diagonal.backUp, Vector3Diagonal.backDn
        };

        like = new float[directionReference.Length];
        hate = new float[directionReference.Length];
    }

    private float split = 0.03f;

    private void DebugDrawContext()
    {
        for (int i = 0; i < directionReference.Length; i++)
        {
            Debug.DrawRay(transform.position + new Vector3( split,  split,  split), directionReference[i] * like[i], Color.cyan);
            Debug.DrawRay(transform.position + new Vector3(-split, -split, -split), directionReference[i] * hate[i], Color.red);
        }
    }

    private void Update()
    {
        DebugDrawContext();
    }

    private void LateUpdate()
    {
        Clear(like);
        Clear(hate);
    }

    
}
