using UnityEngine;

public class DronePathVisualizer : MonoBehaviour
{
    private Vector3[] points;
    private LineRenderer line;
    private DroneController drone;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        drone = GetComponent<DroneController>();

        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.startColor = Color.white;
        line.endColor = Color.white;
    }

    public void SetPoints(Vector3[] newPoints)
    {
        points = newPoints;
        UpdateLine();
    }

    private void LateUpdate()
    {
        if (points == null || points.Length == 0)
            return;

        if (drone != null && !drone.Movement.IsMoving)
            return;

        UpdateLine();
    }

    private void UpdateLine()
    {
        line.positionCount = points.Length;

        line.SetPosition(0, drone.transform.position);

        for (int i = 1; i < points.Length; i++)
            line.SetPosition(i, points[i]);
    }
}
