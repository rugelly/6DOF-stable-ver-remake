using UnityEngine;

public class Context : MonoBehaviour
{
    public float[] like = new float[6];
    public float[] hate = new float[6];
    private Vector3[] directionReference = 
    new Vector3[6] {
        Vector3.up, Vector3.down, 
        Vector3.forward, Vector3.back, 
        Vector3.right, Vector3.left};

    // dot given vector against each direction
    // if new value bigger than relative value in context array, overwrite it
    public void Assign(Vector3 givenDirection, float[] chosenArray)
    {
        for (int i = 0; i < chosenArray.Length; i++)
        {
            var newValue = Vector3.Dot(givenDirection, directionReference[i]);
            chosenArray[i] = newValue > chosenArray[i] ? newValue : chosenArray[i];
        }
    }

    public void Clear(float[] chosenArray)
    {
        for (int i = 0; i < chosenArray.Length; i++)
        {
            chosenArray[i] = 0;
        }
    }

    private float split = 0.03f;
    private void DebugDrawContext()
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.DrawRay(transform.position + new Vector3(split, split, split), directionReference[i] * like[i], Color.green);
            Debug.DrawRay(transform.position + new Vector3(-0.02f, -split, -split), directionReference[i] * hate[i], Color.red);
        }
    }

    private void Update()
    {
        DebugDrawContext();
    }
}
