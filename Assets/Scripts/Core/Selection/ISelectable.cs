using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    Transform GetTransform();

    void OnSelect();
    void OnDeselect();

    List<SelectionAction> GetActions();

    bool PerformAction(SelectionAction action);

    SelectionType GetSelectionType();

    object GetViewData();
}
