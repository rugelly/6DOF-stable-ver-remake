using System.Collections;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private Context _context;

    private void Awake()
    {
        _context = GetComponent<Context>();
    }

    public void Move(Vector3 direction, float speed)
    {
        
    }
}
