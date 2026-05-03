using UnityEngine;

public class ScoutController : DroneController, IScoutDrone
{
    [SerializeField] float discoverTime = 5f;

    public float GetDiscoverTime() => discoverTime;

    public override void OnReachTarget(GameObject target)
    {
        SetState(new DiscoveringState(target));
    }

    [ContextMenu("Discover")]
    public void Discover(GameObject targetSector)
    {
        SetState(new MovingState(targetSector));
    }
}
