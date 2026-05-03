using UnityEngine;

public interface IScoutDrone
{
    public void Discover(GameObject targetSector);
    float GetDiscoverTime();
}
