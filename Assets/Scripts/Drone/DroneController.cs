using UnityEngine;

public abstract class DroneController : MonoBehaviour
{
    [SerializeField] protected GameObject targetSector;
    private IDroneState _currentState;
    public SpawnPoint SpawnPoint { get; private set; }
    public DroneMovement Movement { get; private set; }


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
        SetState(new IdleState());
    }


    public void SetState(IDroneState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
    }

    public abstract void OnReachTarget(GameObject target);
}