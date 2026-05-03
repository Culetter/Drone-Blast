using UnityEngine;

public class UnloadingState : IDroneState
{
    public DroneStateType StateType => DroneStateType.Unloading;

    public void Enter(DroneController drone)
    {
        if (drone is not IWorkerDrone workerDrone)
        {
            drone.SetState(new IdleState());
            return;
        }

        workerDrone.UnloadResources();
        drone.SetState(new IdleState());
    }

    public void Update() { }

    public void Exit() { }
}
