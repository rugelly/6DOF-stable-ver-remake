using UnityEngine;

namespace PluggableFSM
{
[CreateAssetMenu(menuName=Paths.state)]
public class State : ScriptableObject
{
    public Action[] actions;
    public Connector[] connectors;

    public bool exiting;

    public void Enter(Controller controller) 
    {
        exiting = false;

        // Debug.Log(name + " - ENTERING ----------------------------------------------------");

        foreach (var connector in connectors)
        {
            // Debug.Log(name + " - " + connector.connection.name + " - ENTER");
            connector.connection.Enter();
        }

        foreach (var action in actions)
        {
            // Debug.Log(name + " - " + action.name + " - ENTER");
            action.Enter(controller);
        }
    }

    public void Tick(Controller controller) 
    {
        if (!exiting)
        {
            // Debug.Log(name + " evaluating connections...");
            foreach (var connector in connectors)
            {
                // Debug.Log(name + " - " + connector.connection.name + " - EVAL");
                var result = connector.connection.Eval(controller);

                if (connector.wantedResult == Connector.WantedResult.isTrue)
                    if (result)
                        Transition(controller, connector, result);

                if (connector.wantedResult == Connector.WantedResult.isFalse)
                    if (!result)
                        Transition(controller, connector, result);
            }
        }

        if (!exiting)
        {
            // Debug.Log(name + " ticking actions...");
            foreach (var action in actions)
            {
                // Debug.Log(name + " - " + action.name + " - TICK");
                action.Tick(controller);
            }
        }
    }

    private void Transition(Controller c, Connector connector, bool result)
    {
        // Debug.Log(name + " - " + connector.connection.name + " eval'd wanted result of: " + result + " !");
        exiting = true;
        Exit(c);
        c.Add(connector.next);
        c.callEnterNextFrame = true;
    }

    public void Exit(Controller controller) 
    {
        foreach (var connector in connectors)
        {
            // Debug.Log(name + " - " + connector.connection.name + " - EXIT");
            connector.connection.Exit();
        }

        foreach (var action in actions)
        {
            // Debug.Log(name + " - " + action.name + " - EXIT");
            action.Exit(controller);
        }

        // Debug.Log(name + " - EXITED ----------------------------------------------------");
    }
}
}