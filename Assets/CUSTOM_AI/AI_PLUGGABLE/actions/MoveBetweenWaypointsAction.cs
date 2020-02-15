using UnityEngine;

[CreateAssetMenu(menuName = "pluggableAI/actions/waypoint move between")]
public class MoveBetweenWaypointsAction : AIAction
{
    public Waypoint[] waypoints;
    public bool reverseDirection;

    private bool trigger = false;
    private int currentIndex;

    public override void Act(AIStateController controller)
    {
        if (!trigger)
        {
            currentIndex = ClosestWaypointIndex(controller.transform.position);
            trigger = true;
        }

        var distance = Vector3.Distance(controller.transform.position, waypoints[currentIndex].point.position);

        if (distance <= waypoints[currentIndex].minDistanceToReach)
        {
            if (reverseDirection)
                currentIndex--;
            else
                currentIndex++;

            if (currentIndex >= waypoints.Length)
                currentIndex = 0;
            else if (currentIndex < 0)
                currentIndex = waypoints.Length - 1;

            if (waypoints[currentIndex].overrideNextPoint != -1)
                currentIndex = waypoints[currentIndex].overrideNextPoint;
        }

        controller._motor.Move(waypoints[currentIndex].point.position); 
    }

    private int ClosestWaypointIndex(Vector3 position)
    {
        var storedDistance = 9999f;
        var index = 0;
        for (int i = 0; i < waypoints.Length; i++)
        {
            var distance = Vector3.Distance(waypoints[i].point.position, position);
            if (distance < storedDistance)
            {
                index = i;
                storedDistance = distance;
            }
        }
        return index;
    }

    public override void OnExit()
    {
        trigger = false;
        Debug.Log("OnExit called for _moving between waypoints action_");
    }
}