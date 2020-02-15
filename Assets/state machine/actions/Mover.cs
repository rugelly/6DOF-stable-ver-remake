using System.Collections.Generic;
using PluggableFSM;
using UnityEngine;

[CreateAssetMenu(menuName = Paths.actions + "sum movement vectors")]
public class Mover : Action
{
    private Vector3 sum;
    public Vector3 functionalTotal; // for debug purps
    public List<Vector3> test = new List<Vector3>();

    public override void Tick(Controller controller)
    {
        foreach (var vector in MovementVector.array)
        {
            sum += vector;
        }
        controller.cc.Move(sum * Time.deltaTime);
        functionalTotal = sum;
        sum = Vector3.zero;
        test = MovementVector.array;
        MovementVector.array.Clear();
    }

    public override void Enter(Controller controller)
    {
        // Debug.Log(this.GetType().ToString() + " : on enter called");
    }

    public override void Exit(Controller controller)
    {
        sum = Vector3.zero;
        // Debug.Log(this.GetType().ToString() + " : on exit called");
    }
}