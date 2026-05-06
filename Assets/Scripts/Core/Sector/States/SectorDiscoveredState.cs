using System.Collections.Generic;
using UnityEngine;

public class SectorDiscoveredState : ISectorState
{
    public SectorStateType StateType => SectorStateType.Discovered;
    private SectorController sector;
    public DroneController ResponsibleDrone { get; private set; }
    public void Enter(SectorController sector, DroneController drone)
    {
        this.sector = sector;
        ResponsibleDrone = drone;
    }

    public void Exit()
    {

    }
    public List<SelectionAction> GetAvailableActions()
    {
        if (sector.BaseHasAutoMiningUpgrade())
            return null;

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
