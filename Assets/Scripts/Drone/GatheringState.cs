using UnityEngine;

public class GatheringState : IDroneState
{
    private DroneController drone;
    private GameObject target;
    private ResourcesController resources;
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
        resources = target.GetComponent<ResourcesController>();
    }

    public void Update()
    {
        float gatheringTime = worker.GetGatheringTime();
        int inventoryCapacity = worker.GetRemainingInventory();
        int resourcesPerGather = worker.GetResourcesPerGather();
        int resourcesAmount = resources.GetResourcesAmoun();

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

            resources.UpdateResources(toGether);
            worker.UpdateInventory(toGether);
        }
    }

    public void Exit() { }
}
