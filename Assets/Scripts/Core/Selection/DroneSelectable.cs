using System.Collections.Generic;
using UnityEngine;

public class DroneSelectable : MonoBehaviour, ISelectable
{
    private DroneController drone;
    private void Awake()
    {
        drone = GetComponent<DroneController>();
    }
    public Transform GetTransform() => transform;
    public List<SelectionAction> GetActions() => null;

    public SelectionType GetSelectionType() => SelectionType.Drone;

    public object GetViewData() => drone;

    public void OnDeselect() { }

    public void OnSelect() { }

    public bool PerformAction(SelectionAction action) => true;
}
