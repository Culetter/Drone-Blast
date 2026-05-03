using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] GameObject scoutPrefab;
    [SerializeField] GameObject workerPrefab;

    [SerializeField] SpawnPoint[] workerSpawnPoints;
    [SerializeField] SpawnPoint[] scoutSpawnPoints;
    private SpawnPoint selectedPoint;
    private GameObject drone;
    private void Start()
    {
        SpawnWorker();
        SpawnScout();
    }

    [ContextMenu("Spawn Worker")]
    private void SpawnWorker()
    {
        selectedPoint = GetFreeSpawnPoint(workerSpawnPoints);

        if (selectedPoint == null)
        {
            Debug.Log("No free worker spawn points!");
            return;
        }

        drone = Instantiate(workerPrefab, selectedPoint.transform.position, Quaternion.identity);
        drone.GetComponent<DroneController>().Init(selectedPoint, DroneRole.Worker);

        selectedPoint.SetOccupied(true);
    }

    [ContextMenu("Spawn Scout")]
    private void SpawnScout()
    {
        selectedPoint = GetFreeSpawnPoint(scoutSpawnPoints);

        if (selectedPoint == null)
        {
            Debug.Log("No free scout spawn points!");
            return;
        }

        drone = Instantiate(scoutPrefab, selectedPoint.transform.position, Quaternion.identity);
        drone.GetComponent<DroneController>().Init(selectedPoint, DroneRole.Scout);

        selectedPoint.SetOccupied(true);
    }

    private SpawnPoint GetFreeSpawnPoint(SpawnPoint[] points)
    {
        foreach (SpawnPoint point in points)
        {
            if (!point.IsOccupied)
                return point;
        }

        return null;
    }
}
