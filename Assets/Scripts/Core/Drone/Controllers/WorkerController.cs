using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerController : DroneController, IWorkerDrone
{
    [SerializeField] float gatheringTime = 5.0f;
    [SerializeField] int inventoryCapacity = 10;
    [SerializeField] int remainingInventory;
    [SerializeField] int resourcesPerGather = 1;

    WorkerController()
    {
        remainingInventory = inventoryCapacity;
    }

    public float GetGatheringTime() => gatheringTime;
    public int GetRemainingInventory() => remainingInventory;
    public int GetResourcesPerGather() => resourcesPerGather;
    public bool BaseHasAutoGatheringUpdate() => BaseSector.HasAutoMiningUpgrade();

    public void UpdateInventory(int value)
    {
        remainingInventory -= value;
        NotifyChange();
    }

    public override void OnReachTarget(GameObject target)
    {
        SetState(new GatheringState(target));
    }

    public void UnloadResources()
    {
        SpawnPoint.Sector.LoadStorage(inventoryCapacity - remainingInventory);
        remainingInventory = inventoryCapacity;
        NotifyChange();
    }

    private static readonly HashSet<DroneStateType> availableStates = new()
    {
        DroneStateType.Idle, DroneStateType.Searching
    };

    public override bool IsAvailable() => availableStates.Contains(_currentState.StateType);

    public override void Action(GameObject target, SelectionAction action)
    {
        switch (action)
        {
            case SelectionAction.Gather:
                SetState(new MovingState(target));
                break;
        }
        
    }
    public override DroneData GetData()
    {
        return new DroneData
        {
            role = Role,
            state = _currentState.StateType,
            gatheringTime = gatheringTime,
            inventoryCapacity = inventoryCapacity,
            remainingInventory = remainingInventory,
            resourcesPerGather = resourcesPerGather
        };
    }
}
