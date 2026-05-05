using System.Collections.Generic;
using UnityEngine;
using System;

public class SectorController : MonoBehaviour
{
    [SerializeField] private Material discoveredMaterial;

    private ISectorState _currentState;
    private ResourcesController _resourcesController;
    DroneAvailability droneAvailability;
    public event Action<SectorController> OnStateChanged;
    public event Action OnDataChanged;

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

    public List<SelectionAction> GetAvailableActions()
    {
        return _currentState.GetAvailableActions();
    }
    public bool PerformAction(SelectionAction action)
    {
        DroneRole role = _currentState.GetRequiredDroneRole(action);
        DroneController drone = droneAvailability.GetAvailableDrone(role);

        if (!_currentState.CanPerformAction(action, drone))
            return false;

        drone.Action(gameObject, action);

        switch (action)
        {
            case SelectionAction.Discover:
                SetState(new SectorDiscoveringState());
                break;
            case SelectionAction.Gather:
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
        OnDataChanged?.Invoke();
    }

    public int GetAvailableResources()
    {
        if (_currentState.StateType == SectorStateType.Undiscovered)
            return 0;

        return _resourcesController.GetResourcesAmount();
    }

    [ContextMenu("Change State")]
    public void ChangeDiscoveredState()
    {
        GetComponent<Renderer>().material = discoveredMaterial;
        _resourcesController.Init();
        SetState(new SectorDiscoveredState());
    }

    private void SetState(ISectorState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
        OnStateChanged?.Invoke(this);
        OnDataChanged?.Invoke();
    }
    public SectorData GetData()
    {
        return new SectorData
        {
            sectorState = _currentState.StateType,
            remainingResources = _resourcesController.GetResourcesAmount()
        };
    }
}