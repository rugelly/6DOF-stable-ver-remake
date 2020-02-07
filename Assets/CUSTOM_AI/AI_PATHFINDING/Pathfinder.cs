using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GridStorageSO _grid;
    public Target _target;
    public PathfindsThrough pathfindsThrough;
    PositionTracker _positionTracker;

    private void Awake()
    {
        _positionTracker = GetComponent<PositionTracker>();
    }

    void FindPath(Vector3Int startPos, Vector3Int targetPos)
    {
        Point startPoint = _grid.PointFromCoords(startPos);
        Point targetPoint = _grid.PointFromCoords(targetPos);

        List<Point> openSet = new List<Point>();
        HashSet<Point> closedSet = new HashSet<Point>();
        openSet.Add(startPoint);

        while (openSet.Count > 0)
        {
            Point currentPoint = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentPoint.fCost || openSet[i].fCost == currentPoint.fCost && openSet[i].hCost < currentPoint.hCost)
                    currentPoint = openSet[i];
            }

            openSet.Remove(currentPoint);
            closedSet.Add(currentPoint);

            if (currentPoint == targetPoint)
                return;

            foreach (Point surroundingPoint in _grid.GetSurrounding(currentPoint))
            {
                if (closedSet.Contains(surroundingPoint))
                    continue;

                switch (pathfindsThrough)
                {
                    case PathfindsThrough.ground:
                        if (surroundingPoint.status != Status.GROUND)
                            continue;
                        break;

                    case PathfindsThrough.surface:
                        if (surroundingPoint.status != Status.SURFACE)
                            continue;
                        break;

                    case PathfindsThrough.both:
                        if (surroundingPoint.status != Status.GROUND 
                        ||  surroundingPoint.status != Status.SURFACE)
                            continue;
                        break;
                }
            }
        }
    }

    int GetPathDistance(Point a, Point b)
    {
        // int dstX = Mathf.Abs((int)a.coords.x - (int)b.coords.x);
        // int dstY = Mathf.Abs((int)a.coords.y - (int)b.coords.y);
        // int dstZ = Mathf.Abs((int)a.coords.z - (int)b.coords.z);

        return   Mathf.Abs((int)a.coords.x - (int)b.coords.x)
               + Mathf.Abs((int)a.coords.y + (int)b.coords.y)
               + Mathf.Abs((int)a.coords.z + (int)b.coords.z);
    }
}
