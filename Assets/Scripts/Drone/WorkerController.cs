using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerController : DroneController, IWorkerDrone
{
    [SerializeField] float gatheringTime = 5.0f;
    [SerializeField] int inventoryCapacity = 10;
    [SerializeField] int remainingInventory;
    [SerializeField] int resourcesPerGather = 1;
    [SerializeField] bool autoMiningUpgrade = false;

    WorkerController()
    {
        remainingInventory = inventoryCapacity;
    }

    public float GetGatheringTime() => gatheringTime;
    public int GetRemainingInventory() => remainingInventory;
    public int GetResourcesPerGather() => resourcesPerGather;
    public bool HasAutoMiningUpgrade() => autoMiningUpgrade;
    public void UpdateInventory(int value)
    {
        remainingInventory -= value;
    }

    public override void OnReachTarget(GameObject target)
    {
        SetState(new GatheringState(target));
    }

    public void UnloadResources()
    {
        SpawnPoint.Sector.LoadStorage(inventoryCapacity - remainingInventory);
        remainingInventory = inventoryCapacity;
    }

    private static readonly HashSet<DroneStateType> availableStates = new()
    {
        DroneStateType.Idle
    };

    public override bool IsAvailable() => availableStates.Contains(_currentState.StateType);

    public override void Action(GameObject target, SectorActionType action)
    {
        switch (action)
        {
            case SectorActionType.Gather:
                SetState(new MovingState(target));
                break;
        }
        
    }
}
