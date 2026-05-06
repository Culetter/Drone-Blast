using NUnit.Framework;
using System.Collections.Generic;

public interface ISectorState
{
    SectorStateType StateType { get; }
    DroneController ResponsibleDrone { get; }
    void Enter(SectorController sector, DroneController drone);
    void Exit();
    List<SelectionAction> GetAvailableActions();
    DroneRole GetRequiredDroneRole(SelectionAction action);
    bool CanPerformAction(SelectionAction action, DroneController drone);
}
