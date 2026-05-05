using System.Collections.Generic;
using UnityEngine;

public class SectorGatheringState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Gathering;
    public void Enter(SectorController sector)
    {

    }

    public void Exit()
    {

    }
    public List<SectorActionType> GetAvailableActions()
    {
        return null;
    }
    public DroneRole GetRequiredDroneRole(SectorActionType action)
    {
        switch (action)
        {
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SectorActionType action, DroneController drone)
    {
        switch (action)
        {
            default:
                return false;
        }
    }
}
