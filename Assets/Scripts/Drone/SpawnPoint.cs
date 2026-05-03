using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public BaseSectorController Sector {  get; private set; }
    public bool IsOccupied { get; private set; }

    private void Awake()
    {
        Sector = GetComponentInParent<BaseSectorController>();
    }

    public void SetOccupied(bool value)
    {
        IsOccupied = value;
    }
}
