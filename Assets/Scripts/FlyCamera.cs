using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour
{
    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.
    Converted to C# 27-02-13 - no credit wanted.
    Reformatted and cleaned by Ryan Breaker 23-6-18
    Further reformatting by Tristan - February - 2020
    Original comment:
    Simple flycam I made, since I couldn't find any others made public.
    Made simple to use (drag and drop, done) for regular keyboard layout.
    Controls:
    WASD  : Directional movement
    Shift : Increase speed
    Space : Moves camera directly up per its local Y-axis
    */

    public float mainSpeed = 10.0f;   // Regular speed
    public float shiftAdd  = 25.0f;   // Amount to accelerate when shift is pressed
    public float maxShift  = 100.0f;  // Maximum speed when holding shift
    public float camSens   = 0.15f;   // Mouse sensitivity

    Vector2 rotation;
    private float totalRun = 1.0f;

    void Update()
    {
        rotation += new Vector2(-Input.GetAxis("Mouse Y") * camSens, Input.GetAxis("Mouse X") * camSens);
        transform.eulerAngles = rotation;

        // Keyboard commands
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p *= totalRun * shiftAdd;
            p = Vector3.ClampMagnitude(p, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p *= mainSpeed;
        }

        p *= Time.deltaTime;
        transform.Translate(p);
    }

    // Returns the basic values, if it's 0 than it's not active.
    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();

        // Forwards
        if (Input.GetKey(KeyCode.W))
            p_Velocity += Vector3.forward;

        // Backwards
        if (Input.GetKey(KeyCode.S))
            p_Velocity += Vector3.back;

        // Left
        if (Input.GetKey(KeyCode.A))
            p_Velocity += Vector3.left;

        // Right
        if (Input.GetKey(KeyCode.D))
            p_Velocity += Vector3.right;

        // Up
        if (Input.GetKey(KeyCode.E))
            p_Velocity += Vector3.up;

        // Down
        if (Input.GetKey(KeyCode.Q))
            p_Velocity += Vector3.down;

        return p_Velocity;
    }
}