using UnityEngine;

public class MovingState : IDroneState
{
    private DroneController drone;
    private GameObject targetObject;

    public MovingState() { }
    public MovingState(GameObject targetObject)
    {
        Debug.Log("Move State");
        this.targetObject = targetObject;
    }

    public DroneStateType StateType
    {
        get
        {
            if (targetObject != null)
                return DroneStateType.MovingToSector;
            return DroneStateType.MovingToBaseSector;
        }
    }

    public void Enter(DroneController drone)
    {
        this.drone = drone;

        if (targetObject != null)
        {
            drone.Movement.SetTarget(targetObject);
            return;
        }
        drone.Movement.SetTarget(drone.SpawnPoint);
    }

    public void Update()
    {
        if (!drone.Movement.HasReachedTarget()) return;

        if (targetObject != null)
        {
            drone.OnReachTarget(targetObject);
            return;
        }

        if (drone is IWorkerDrone)
        {
            drone.SetState(new UnloadingState());
            return;
        }

        drone.SetState(new IdleState());
    }

    public void Exit() { }
}
