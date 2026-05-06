using System.Collections.Generic;
using UnityEngine;

public class BaseSelectable : MonoBehaviour, ISelectable
{
    private BaseSectorController sector;
    private void Awake()
    {
        sector = GetComponent<BaseSectorController>();
    }
    public Transform GetTransform() => transform;

    public void OnSelect() { }
    public void OnDeselect() { }

    public List<SelectionAction> GetActions()
    {
        return null;
    }

    public bool PerformAction(SelectionAction action)
    {
        return true;
    }

    public SelectionType GetSelectionType() => SelectionType.BaseSector;

    public object GetViewData() => sector;
}
