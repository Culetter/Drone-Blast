using System.Collections.Generic;
using UnityEngine;

public class SearchingState : IDroneState
{
    private SectorRegister sectorRegister;
    private DroneController drone;
    private SectorController sector;
    public SearchingState()
    {
        sectorRegister = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<SectorRegister>();
    }
    public DroneStateType StateType => DroneStateType.Searching;
    public void Enter(DroneController drone)
    {
        this.drone = drone;
    }
    public void Update()
    {
        sector = sectorRegister.GetDiscoveredSector();

        if (sector == null)
            return;

        drone.SetState(new MovingState(sector.gameObject));
    }
    public void Exit()
    {

    }
}
