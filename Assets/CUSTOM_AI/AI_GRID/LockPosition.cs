using UnityEngine;

public class LockPosition : MonoBehaviour
{
    Vector3 position = Vector3.zero;

    private void OnEnable()
    {
        transform.position = position;
    }

    private void OnDrawGizmosSelected()
    {
        transform.position = position;
    }
}
