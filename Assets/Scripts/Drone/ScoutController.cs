using System.Collections.Generic;
using UnityEngine;

public class ScoutController : DroneController, IScoutDrone
{
    [SerializeField] float discoverTime = 5f;

    public float GetDiscoverTime() => discoverTime;

    private static readonly HashSet<DroneStateType> availableStates = new()
    {
        DroneStateType.Idle, DroneStateType.MovingToBaseSector
    };

    public override bool IsAvailable() => availableStates.Contains(_currentState.StateType);

    public override void OnReachTarget(GameObject target)
    {
        SetState(new DiscoveringState(target));
    }

    public override void Action(GameObject target, SectorActionType action)
    {
        switch (action)
        {
            case SectorActionType.Discover:
                SetState(new MovingState(target));
                break;
        }
        
    }
}
