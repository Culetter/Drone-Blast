using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneAvailability : MonoBehaviour
{
    private Dictionary<DroneRole, List<DroneController>> availableDrones = new Dictionary<DroneRole, List<DroneController>>();
    private Dictionary<DroneRole, List<DroneController>> busyDrones = new Dictionary<DroneRole, List<DroneController>>();
    void Awake()
    {
        foreach (DroneRole role in System.Enum.GetValues(typeof(DroneRole)))
        {
            availableDrones[role] = new List<DroneController>();
            busyDrones[role] = new List<DroneController>();
        }
    }
    public DroneController GetAvailableDrone(DroneRole role)
    {
        var list = availableDrones[role];

        if (list.Count == 0)
            return null;

        return list[0];
    }
    public void Register(DroneController drone)
    {
        drone.OnStateChanged += HandleStateChanged;
        HandleStateChanged(drone);
    }
    private void HandleStateChanged(DroneController drone)
    {
        RemoveFromAll(drone);

        if (drone.IsAvailable())
            availableDrones[drone.Role].Add(drone);
        else busyDrones[drone.Role].Add(drone);
    }
    private void RemoveFromAll(DroneController drone)
    {
        availableDrones[drone.Role].Remove(drone);
        busyDrones[drone.Role].Remove(drone);
    }
}
