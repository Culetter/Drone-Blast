using System.Collections.Generic;
using UnityEngine;

public class SectorRegister : MonoBehaviour
{
    private List<SectorController> discoveredSectors = new List<SectorController>();

    public SectorController GetClosestToBase(BaseSectorController baseSector)
    {
        if (discoveredSectors.Count == 0)
            return null;

        Transform baseTransform = baseSector.transform;

        SectorController closest = null;
        float minDistance = float.MaxValue;

        foreach (var sector in discoveredSectors)
        {
            float distance = Vector3.Distance(baseTransform.position, sector.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = sector;
            }
        }

        return closest;
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
