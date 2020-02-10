using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 move 
    {get{return new Vector2(
    Input.GetAxisRaw
        ("Horizontal"),
    Input.GetAxisRaw
        ("Vertical"));}}

     // NOTE: the input axis are OPPOSITE the vector axis
    public Vector2 look
    {get{return new Vector2(
    Input.GetAxisRaw
        ("Mouse Y"),
    Input.GetAxisRaw
        ("Mouse X"));}}

    public bool jump 
    {get{return 
    Input.GetButtonDown
        ("Jump");}}

    public bool jumpHeld 
    {get{return 
    Input.GetButton
        ("Jump");}}

    public bool grapple 
    {get{return 
    Input.GetButtonDown
        ("Grapple");}}

    public bool shoot
    {get{return
    Input.GetButtonDown
        ("Shoot");}}

    public bool shootHeld
    {get{return
    Input.GetButton
        ("Shoot");}}

    public bool oppositeHorizontalInput {get{return _oppositeHorizontalInput;}}
    private bool _oppositeHorizontalInput;
    public bool oppositeVerticalInput {get{return _oppositeVerticalInput;}}
    private bool _oppositeVerticalInput;

    private float h, v, sh, sv;
    private void Update()
    {
        h = move.x;
        v = move.y;

        _oppositeHorizontalInput = DifferentInputDirections(h, sh) ? true : false;
        _oppositeVerticalInput = DifferentInputDirections(v, sv) ? true : false;

        sh = h;
        sv = v;
    }

    private bool DifferentInputDirections(float one, float two)
    {
        if ((one > 0 && two > 0) || (one < 0 && two < 0))
            return false;
        else if ((one > 0 && two < 0) || (one < 0 && two > 0))
            return true;
        else
            return false;
    }
}
