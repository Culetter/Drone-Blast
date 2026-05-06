using System.Collections.Generic;
using UnityEngine;

public class SectorUndiscoveredState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Undiscovered;
    public DroneController ResponsibleDrone { get; private set; }
    public DroneRole GetRequiredDroneRole() => DroneRole.Scout;
    public void Enter(SectorController sector, DroneController drone)
    {
        ResponsibleDrone = drone;
    }

    public void Exit()
    {

    }
    public List<SelectionAction> GetAvailableActions()
    {
        return new List<SelectionAction>()
        {
            SelectionAction.Discover
        };
    }
    public DroneRole GetRequiredDroneRole(SelectionAction action)
    {
        switch (action)
        {
            case SelectionAction.Discover:
                return DroneRole.Scout;
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SelectionAction action, DroneController drone)
    {
        switch (action)
        {
            case SelectionAction.Discover:
                return drone != null;

            default:
                return false;
        }
    }
}
