using UnityEngine;

public class DrawLevelGrid : MonoBehaviour
{
    public GridStorageSO _grid;
    public bool drawGrid, drawBorder;
    [Range(0.1f, 1)]
    public float size;

    private void OnDrawGizmos()
    {
        if (drawGrid)
        {
            foreach (var point in _grid.points)
            {
                Gizmos.color = SetGizmoColour(point.status);
                Gizmos.DrawWireCube(point.coords, Vector3.one * size);
            }
        }

        if (drawBorder)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((_grid.size / 2) + (Vector3.one * -0.5f), _grid.size);
        }
    }

    Color SetGizmoColour(int status)
    {
        if      (status == _grid.AIR)
            return Color.clear;
        else if (status == _grid.GROUND)
            return Color.green;
        else if (status == _grid.SURFACE)
            return Color.blue;
        else if (status == _grid.SOLID)
            return Color.red;
        else if (status == _grid.BORDER)
            return Color.clear;
        else
            return Color.magenta;
    }
}
