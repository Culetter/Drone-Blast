using System.Collections.Generic;
using UnityEngine;

public interface IDroneState
{
    DroneStateType StateType { get; }
    void Enter(DroneController drone);
    void Update();
    void Exit();
}
