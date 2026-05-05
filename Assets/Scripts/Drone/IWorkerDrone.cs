using UnityEngine;

public interface IWorkerDrone
{
    float GetGatheringTime();
    int GetRemainingInventory();
    int GetResourcesPerGather();
    void UpdateInventory(int value);
    void UnloadResources();
    bool HasAutoMiningUpgrade();
}
