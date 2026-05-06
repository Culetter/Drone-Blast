using System.Collections.Generic;
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

        Vector3 start = drone.transform.position;

        Vector3 targetPos;

        if (targetObject != null)
        {
            targetPos = targetObject.transform.position;
            targetPos.y = drone.SpawnPoint.transform.position.y;

            drone.Movement.SetTarget(targetObject);
        }
        else
        {
            drone.Movement.SetTarget(drone.SpawnPoint);
            targetPos = drone.SpawnPoint.transform.position;
        }

        drone.SetPath(new Vector3[]
        {
            start,
            targetPos
        });
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
