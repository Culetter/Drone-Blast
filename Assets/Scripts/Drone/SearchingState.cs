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
        sector = sectorRegister.GetClosestToBase(drone.BaseSector);

        if (sector == null)
            return;

        sector.PerformAction(SelectionAction.Gather);
    }
    public void Exit()
    {

    }
}
