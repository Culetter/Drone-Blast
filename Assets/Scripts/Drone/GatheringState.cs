using System.Collections.Generic;
using UnityEngine;

public class GatheringState : IDroneState
{
    private DroneController drone;
    private GameObject target;
    private SectorController sector;
    private IWorkerDrone worker;

    private float timer = 0;

    public DroneStateType StateType => DroneStateType.Gathering;

    public GatheringState(GameObject target)
    {
        Debug.Log("Gathering State");
        this.target = target;
    }

    public void Enter(DroneController drone)
    {
        if (drone is not IWorkerDrone workerDrone)
        {
            drone.SetState(new MovingState());
            return;
        }

        worker = workerDrone;
        this.drone = drone;
        sector = target.GetComponent<SectorController>();
    }

    public void Update()
    {
        float gatheringTime = worker.GetGatheringTime();
        int inventoryCapacity = worker.GetRemainingInventory();
        int resourcesPerGather = worker.GetResourcesPerGather();
        int resourcesAmount = sector.GetAvailableResources();

        int toGether = Mathf.Min(inventoryCapacity, resourcesPerGather, resourcesAmount);

        if (toGether <= 0)
        {
            drone.SetState(new MovingState());
            return;
        }

        timer += Time.deltaTime;

        if (timer > gatheringTime)
        {
            timer = 0;

            sector.TakeResources(toGether);
            worker.UpdateInventory(toGether);
        }
    }

    public void Exit() { }
}
