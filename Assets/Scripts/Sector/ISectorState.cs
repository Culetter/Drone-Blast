using NUnit.Framework;
using System.Collections.Generic;

public interface ISectorState
{
    SectorStateType StateType { get; }
    void Enter(SectorController sector);
    void Exit();
    List<SectorActionType> GetAvailableActions();
    DroneRole GetRequiredDroneRole(SectorActionType action);
    bool CanPerformAction(SectorActionType action, DroneController drone);
}
