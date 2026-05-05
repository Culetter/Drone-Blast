using NUnit.Framework;
using System.Collections.Generic;

public interface ISectorState
{
    SectorStateType StateType { get; }
    void Enter(SectorController sector);
    void Exit();
    List<SelectionAction> GetAvailableActions();
    DroneRole GetRequiredDroneRole(SelectionAction action);
    bool CanPerformAction(SelectionAction action, DroneController drone);
}
