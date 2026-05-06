using System.Collections.Generic;
using UnityEngine;

public class IdleState : IDroneState
{
    public DroneStateType StateType => DroneStateType.Idle;
    DroneController drone;
    public void Enter(DroneController drone)
    {
        this.drone = drone;
    }

    public void Update()
    {
        if (drone is IWorkerDrone worker && worker.BaseHasAutoGatheringUpdate())
            drone.SetState(new SearchingState());
    }

    public void Exit()
    {

    }
}
