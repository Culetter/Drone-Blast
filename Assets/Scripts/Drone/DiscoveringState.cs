using System.Collections.Generic;
using UnityEngine;

public class DiscoveringState : IDroneState
{
    private DroneController drone;
    private GameObject target;
    private SectorController sector;

    private float discoverTime;
    private float timer = 0;

    public DroneStateType StateType => DroneStateType.Discovering;

    public DiscoveringState(GameObject target)
    {
        Debug.Log("Discover State");
        this.target = target;
    }

    public void Enter(DroneController drone)
    {
        this.drone = drone;

        sector = target.GetComponent<SectorController>();

        if (drone is IScoutDrone scout)
            discoverTime = scout.GetDiscoverTime();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > discoverTime)
        {
            sector?.ChangeDiscoveredState();
            drone.SetState(new MovingState());
        }
    }

    public void Exit() { }
}
