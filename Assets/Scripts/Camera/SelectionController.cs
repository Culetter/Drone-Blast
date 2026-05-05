using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] private SelectionUI selectionUI;

    private GameObject selectedObject;

    private void Awake()
    {
        if (selectionUI == null)
            selectionUI = FindAnyObjectByType<SelectionUI>();

        selectionUI.OnActionClicked += HandleAction;
    }

    public void Select(GameObject obj)
    {
        selectedObject = obj;

        if (selectedObject == null)
        {
            selectionUI.Hide();
            return;
        }

        var sector = selectedObject.GetComponent<SectorController>();

        if (sector == null)
            return;

        selectionUI.Show(
            selectedObject.transform,
            sector.GetAvailableActions()
        );
    }

    public void ClaerSelection()
    {
        selectedObject = null;
        selectionUI.Hide();
    }

    public void HandleAction(SectorActionType action)
    {
        if (selectedObject == null)
            return;

        var sector = selectedObject.GetComponent<SectorController>();

        if (sector == null)
            return;

        bool success = sector.PerformAction(action);

        if (success)
            selectionUI.Hide();
    }
}
