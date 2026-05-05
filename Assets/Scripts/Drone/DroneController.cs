using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroneController : MonoBehaviour
{
    protected IDroneState _currentState;
    public SpawnPoint SpawnPoint { get; private set; }
    public DroneMovement Movement { get; private set; }
    public DroneRole Role { get; private set; }
    public event Action<DroneController> OnStateChanged;
    public event Action OnDataChanged;

    private void Awake()
    {
        Movement = GetComponent<DroneMovement>();
    }

    private void Update()
    {
        _currentState?.Update();
    }
    private void OnDestroy()
    {
        if (SpawnPoint != null)
            SpawnPoint.SetOccupied(false);
    }

    public void Init(SpawnPoint point, DroneRole role)
    {
        SpawnPoint = point;
        Role = role;
        SetState(new IdleState());
    }
    public void SetState(IDroneState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
        OnStateChanged?.Invoke(this);
        NotifyChange();
    }
    protected void NotifyChange()
    {
        OnDataChanged?.Invoke();
    }
    public abstract void OnReachTarget(GameObject target);
    public abstract void Action(GameObject target, SelectionAction action);
    public abstract bool IsAvailable();
    public abstract DroneData GetData();
}