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
    public List<SelectionAction> GetAvailableActions()
    {
        return new List<SelectionAction>()
        {
            SelectionAction.Gather
        };
    }
    public DroneRole GetRequiredDroneRole(SelectionAction action)
    {
        switch (action)
        {
            case SelectionAction.Gather:
                return DroneRole.Worker;
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SelectionAction action, DroneController drone)
    {
        switch (action)
        {
            case SelectionAction.Gather:
                return drone != null;

            default:
                return false;
        }
    }
}
