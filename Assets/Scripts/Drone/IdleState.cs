using System.Collections.Generic;
using UnityEngine;

public class IdleState : IDroneState
{
    public DroneStateType StateType => DroneStateType.Idle;
    public void Enter(DroneController drone)
    {
        if (drone is IWorkerDrone worker && worker.HasAutoMiningUpgrade())
            drone.SetState(new SearchingState());
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
