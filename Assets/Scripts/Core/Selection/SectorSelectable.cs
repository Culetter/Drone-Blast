using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SectorSelectable : MonoBehaviour, ISelectable
{
    private SectorController sector;
    private void Awake()
    {
        sector = GetComponent<SectorController>();
    }
    public Transform GetTransform() => transform;

    public void OnSelect() { }
    public void OnDeselect() { }

    public List<SelectionAction> GetActions()
    {
        return sector.GetAvailableActions();
    }

    public bool PerformAction(SelectionAction action)
    {
        return sector.PerformAction(action);
    }

    public SelectionType GetSelectionType() => SelectionType.Sector;

    public object GetViewData() => sector;
}
