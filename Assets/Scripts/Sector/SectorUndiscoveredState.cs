using System.Collections.Generic;
using UnityEngine;

public class SectorUndiscoveredState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Undiscovered;
    public DroneRole GetRequiredDroneRole() => DroneRole.Scout;
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
            SectorActionType.Discover
        };
    }
    public DroneRole GetRequiredDroneRole(SectorActionType action)
    {
        switch (action)
        {
            case SectorActionType.Discover:
                return DroneRole.Scout;
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SectorActionType action, DroneController drone)
    {
        switch (action)
        {
            case SectorActionType.Discover:
                return drone != null;

            default:
                return false;
        }
    }
}
