using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void DrawLine(List<Vector3> positions)
    {
        lr.positionCount = positions.Count;
        lr.SetPositions(positions.ToArray());
    }
}
