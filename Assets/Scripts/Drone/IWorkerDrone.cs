using UnityEngine;

public interface IWorkerDrone
{
    public void Gather(GameObject targetSector);
    float GetGatheringTime();
    int GetRemainingInventory();
    int GetResourcesPerGather();
    void UpdateInventory(int value);
    void UnloadResources();
}
