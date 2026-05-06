using System.Collections.Generic;
using UnityEngine;

public class SectorEmptyState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Empty;
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
        return null;
    }
    public DroneRole GetRequiredDroneRole(SelectionAction action)
    {
        switch (action)
        {
            default:
                throw new System.Exception($"Action {action} not supported in {StateType}");
        }
    }
    public bool CanPerformAction(SelectionAction action, DroneController drone)
    {
        switch (action)
        {
            default:
                return false;
        }
    }
}
