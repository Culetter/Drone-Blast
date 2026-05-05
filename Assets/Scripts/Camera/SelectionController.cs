using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] SelectionUI selectionUI;

    [SerializeField] DronePanelUI dronePanel;
    [SerializeField] SectorPanelUI sectorPanel;

    private ISelectable selected;

    private void Awake()
    {
        if (selectionUI == null)
            selectionUI = FindAnyObjectByType<SelectionUI>();

        selectionUI.OnActionClicked += HandleAction;
    }

    public void Select(GameObject obj)
    {
        ClearSelection();

        selected = obj.GetComponent<ISelectable>();

        if (selected == null)
        {
            selectionUI.Hide();
            return;
        }

        selected.OnSelect();

        ShowPanel(selected);

        selectionUI.Show(
            selected.GetTransform(),
            selected.GetActions()
        );
    }
    private void ShowPanel(ISelectable selectable)
    {
        sectorPanel.Hide();
        dronePanel.Hide();

        switch (selectable.GetSelectionType())
        {
            case SelectionType.Sector:
                sectorPanel.Show((SectorController)selectable.GetViewData());
                break;
            case SelectionType.Drone:
                dronePanel.Show((DroneController)selectable.GetViewData());
                break;
        }
    }
    public void ClearSelection()
    {
        if (selected != null)
            selected.OnDeselect();

        selected = null;

        sectorPanel.Hide();
        selectionUI.Hide();
    }

    public void HandleAction(SelectionAction action)
    {
        if (selected == null)
            return;

        bool success = selected.PerformAction(action);

        if (success)
            selectionUI.Hide();
    }
}
