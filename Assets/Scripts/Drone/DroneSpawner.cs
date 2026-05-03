using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] GameObject scoutPrefab;
    [SerializeField] GameObject workerPrefab;

    [SerializeField] SpawnPoint[] workerSpawnPoints;
    [SerializeField] SpawnPoint[] scoutSpawnPoints;
    private DroneAvailability droneAvailability;
    private SpawnPoint selectedPoint;
    private GameObject drone;
    private void Awake()
    {
        droneAvailability = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<DroneAvailability>();
    }
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
        DroneController controller = drone.GetComponent<DroneController>();
        controller.Init(selectedPoint, DroneRole.Worker);

        selectedPoint.SetOccupied(true);
        droneAvailability.Register(controller);
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
        DroneController controller = drone.GetComponent<DroneController>();
        controller.Init(selectedPoint, DroneRole.Scout);

        selectedPoint.SetOccupied(true);
        droneAvailability.Register(controller);
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
