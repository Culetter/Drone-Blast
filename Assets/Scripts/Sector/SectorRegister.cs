using System.Collections.Generic;
using UnityEngine;

public class SectorRegister : MonoBehaviour
{
    private List<SectorController> discoveredSectors = new List<SectorController>();

    public SectorController GetDiscoveredSector()
    {
        if (discoveredSectors.Count == 0)
            return null;

        return discoveredSectors[0];
    }

    public void Register(SectorController sector)
    {
        sector.OnStateChanged += HandleStateChanged;
    }
    private void HandleStateChanged(SectorController sector)
    {
        RemoveFromAll(sector);

        if (sector.GetState() == SectorStateType.Discovered)
            discoveredSectors.Add(sector);
    }

    private void RemoveFromAll(SectorController sector)
    {
        discoveredSectors.Remove(sector);
    }
}
