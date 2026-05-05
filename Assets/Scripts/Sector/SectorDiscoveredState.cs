using System.Collections.Generic;
using UnityEngine;

public class SectorDiscoveredState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Discovered;
    public void Enter(SectorController sector)
    {

    }

    public void Exit()
    {

    }
    public List<SectorActionType> GetAvailableActions()
    {
        return new List<SectorActionType>()
        {
            SectorActionType.Gather
        };
    }
    public DroneRole GetRequiredDroneRole(SectorActionType action)
    {
        switch (action)
        {
            case SectorActionType.Gather:
                return DroneRole.Worker;
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SectorActionType action, DroneController drone)
    {
        switch (action)
        {
            case SectorActionType.Gather:
                return drone != null;

            default:
                return false;
        }
    }
}
