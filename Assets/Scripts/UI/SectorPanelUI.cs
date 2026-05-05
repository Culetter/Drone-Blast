using TMPro;
using UnityEngine;

public class SectorPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sectorStateText;
    [SerializeField] TextMeshProUGUI remainingResourcesText;

    private SectorController target;

    public void Show(SectorController newTarget)
    {
        if (target != null)
            target.OnDataChanged -= UpdateUI;

        target = newTarget;

        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        target.OnDataChanged += UpdateUI;

        gameObject.SetActive(true);
        UpdateUI();
    }
    public void Hide()
    {
        if (target != null)
            target.OnDataChanged -= UpdateUI;

        target = null;
        gameObject.SetActive(false);
    }
    private void UpdateUI()
    {
        if (target == null) return;

        SectorData data = target.GetData();

        sectorStateText.text = $"State: {GetSectorStateName(data.sectorState)}";
        remainingResourcesText.text = $"Resources: {data.remainingResources}";
    }
    public static string GetSectorStateName(SectorStateType state)
    {
        return state switch
        {
            SectorStateType.Undiscovered => "Undiscovered",
            SectorStateType.Discovering => "Scanning sector",
            SectorStateType.Discovered => "Discovered",
            SectorStateType.Gathering => "Resource extraction",
            SectorStateType.Empty => "Depleted",
            _ => "Unknown"
        };
    }
}
