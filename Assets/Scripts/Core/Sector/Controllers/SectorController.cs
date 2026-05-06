using System;
using System.Collections.Generic;
using UnityEngine;

public class SectorController : MonoBehaviour
{
    [SerializeField] private Material discoveredMaterial;

    private ISectorState _currentState;
    private ResourcesController _resourcesController;
    private SectorStateType _previousStateType;
    private DroneAvailability droneAvailability;
    private BaseSectorController baseSector;

    public event Action<SectorController> OnStateChanged;
    public event Action OnDataChanged;

    private void Awake()
    {
        _resourcesController = GetComponent<ResourcesController>();
        droneAvailability = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<DroneAvailability>();
    }

    private void Start()
    {
        SetState(new SectorUndiscoveredState(), null);
    }

    public void Init(BaseSectorController baseSector)
    {
        this.baseSector = baseSector;
    }

    public SectorStateType GetState()
    {
        return _currentState.StateType;
    }

    public bool BaseHasAutoMiningUpgrade() => baseSector.HasAutoMiningUpgrade();

    public List<SelectionAction> GetAvailableActions()
    {
        return _currentState.GetAvailableActions();
    }

    public bool PerformAction(SelectionAction action)
    {
        DroneRole role = _currentState.GetRequiredDroneRole(action);
        DroneController drone;


        if (role == DroneRole.None)
            drone = _currentState.ResponsibleDrone;
        else
            drone = droneAvailability.GetAvailableDrone(role);

        if (!_currentState.CanPerformAction(action, drone))
            return false;

        drone.Action(gameObject, action);

        switch (action)
        {
            case SelectionAction.Discover:
                SetState(new SectorDiscoveringState(), drone);
                break;

            case SelectionAction.Gather:
                SetState(new SectorGatheringState(), drone);
                break;

            case SelectionAction.Cancel:
                SetPreviousState();
                drone.SetState(new MovingState());
                break;
        }

        return true;
    }

    public void SetPreviousState()
    {
        switch (_previousStateType)
        {
            case SectorStateType.Undiscovered:
                SetState(new SectorUndiscoveredState(), null);
                break;

            case SectorStateType.Discovered:
                SetState(new SectorDiscoveredState(), null);
                break;
        }
    }

    public void TakeResources(int amount)
    {
        _resourcesController.UpdateResources(amount);

        if (_resourcesController.GetResourcesAmount() <= 0)
        {
            SetState(new SectorEmptyState(), null);
        }

        OnDataChanged?.Invoke();
    }

    public int GetAvailableResources()
    {
        if (_currentState.StateType == SectorStateType.Undiscovered)
            return 0;

        return _resourcesController.GetResourcesAmount();
    }

    [ContextMenu("Change State")]
    public void ChangeDiscoveredState(DroneController drone)
    {
        GetComponent<Renderer>().material = discoveredMaterial;
        _resourcesController.Init();
        SetState(new SectorDiscoveredState(), drone);
    }

    public SectorData GetData()
    {
        return new SectorData
        {
            sectorState = _currentState.StateType,
            remainingResources = _resourcesController.GetResourcesAmount()
        };
    }

    private void SetState(ISectorState newState, DroneController drone)
    {
        _currentState?.Exit();
        _previousStateType = _currentState?.StateType ?? SectorStateType.Undiscovered;
        _currentState = newState;

        _currentState.Enter(this, drone);

        OnStateChanged?.Invoke(this);
        OnDataChanged?.Invoke();
    }
}