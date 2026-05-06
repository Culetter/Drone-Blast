using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] SelectionUI selectionUI;

    [SerializeField] DronePanelUI dronePanel;
    [SerializeField] SectorPanelUI sectorPanel;
    [SerializeField] BasePanelUI basePanel;

    private ISelectable selected;
    private SectorController currentSector;

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

        currentSector = selected.GetViewData() as SectorController;

        if (currentSector != null)
            currentSector.OnStateChanged += HandleSectorStateChanged;

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
        basePanel.Hide();

        switch (selectable.GetSelectionType())
        {
            case SelectionType.Sector:
                sectorPanel.Show((SectorController)selectable.GetViewData());
                break;
            case SelectionType.Drone:
                dronePanel.Show((DroneController)selectable.GetViewData());
                break;
            case SelectionType.BaseSector:
                basePanel.Show((BaseSectorController)selectable.GetViewData());
                break;
        }
    }
    public void ClearSelection()
    {
        if (selected != null)
            selected.OnDeselect();

        if (currentSector != null)
            currentSector.OnStateChanged -= HandleSectorStateChanged;


        currentSector = null;
        selected = null;

        basePanel.Hide();
        dronePanel.Hide();
        sectorPanel.Hide();
        selectionUI.Hide();
    }

    public void HandleAction(SelectionAction action)
    {
        if (selected == null)
            return;

        bool success = selected.PerformAction(action);

        if (success)
            UpdateButtons();
    }
    private void HandleSectorStateChanged(SectorController sector)
    {
        UpdateButtons();
    }
    private void UpdateButtons()
    {
        selectionUI.Hide();

        selectionUI.Show(
            selected.GetTransform(),
            selected.GetActions()
        );
    }
}
