using System.Collections.Generic;
using UnityEngine;

public class SectorGatheringState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Gathering;
    public DroneController ResponsibleDrone { get; private set; }
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
            SelectionAction.Cancel
        };
    }
    public DroneRole GetRequiredDroneRole(SelectionAction action)
    {
        switch (action)
        {
            case SelectionAction.Cancel:
                return DroneRole.None;
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SelectionAction action, DroneController drone)
    {
        switch (action)
        {
            case SelectionAction.Cancel:
                return drone != null;
            default:
                return false;
        }
    }
}
