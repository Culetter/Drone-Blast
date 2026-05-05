using UnityEngine;

public struct DroneData
{
    public DroneRole role;
    public DroneStateType state;

    public float? discoveringTime;

    public int? inventoryCapacity;
    public int? remainingInventory;
    public int? resourcesPerGather;
    public float? gatheringTime;
}
