using UnityEngine;

public class IdleState : IDroneState
{
    public DroneStateType StateType => DroneStateType.Idle;
    public void Enter(DroneController drone)
    {
        Debug.Log("Idle State");
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
