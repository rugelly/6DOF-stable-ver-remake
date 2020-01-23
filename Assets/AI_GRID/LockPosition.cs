using UnityEngine;

public class LockPosition : MonoBehaviour
{
    public Vector3 position;

    private void OnEnable()
    {
        transform.position = position;
    }

    private void OnDrawGizmosSelected()
    {
        transform.position = position;
    }
}
