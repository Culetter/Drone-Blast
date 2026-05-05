using System.Collections.Generic;
using UnityEngine;

public class SectorController : MonoBehaviour
{
    [SerializeField] private Material discoveredMaterial;

    private ISectorState _currentState;
    private ResourcesController _resourcesController;
    DroneAvailability droneAvailability;
    public event System.Action<SectorController> OnStateChanged;

    private void Awake()
    {
        _resourcesController = GetComponent<ResourcesController>();
        droneAvailability = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<DroneAvailability>();
    }

    private void Start()
    {
        SetState(new SectorUndiscoveredState());
    }

    public SectorStateType GetState()
    {
        return _currentState.StateType;
    }

    public List<SectorActionType> GetAvailableActions()
    {
        return _currentState.GetAvailableActions();
    }
    public bool PerformAction(SectorActionType action)
    {
        DroneRole role = _currentState.GetRequiredDroneRole(action);
        DroneController drone = droneAvailability.GetAvailableDrone(role);

        if (!_currentState.CanPerformAction(action, drone))
            return false;

        drone.Action(gameObject, action);

        switch (action)
        {
            case SectorActionType.Discover:
                SetState(new SectorDiscoveringState());
                break;
            case SectorActionType.Gather:
                SetState(new SectorGatheringState());
                break;
        }

        return true;
    }

    public void TakeResources(int amount)
    {
        _resourcesController.UpdateResources(amount);

        if (_resourcesController.GetResourcesAmount() <= 0)
        {
            SetState(new SectorEmptyState());
        }
    }

    [ContextMenu("Change State")]
    public void ChangeDiscoveredState()
    {
        SetState(new SectorDiscoveredState());
        GetComponent<Renderer>().material = discoveredMaterial;
        _resourcesController.Init();
    }

    private void SetState(ISectorState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
        OnStateChanged?.Invoke(this);
    }
}