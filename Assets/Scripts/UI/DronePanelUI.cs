using TMPro;
using UnityEngine;

public class DronePanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] TextMeshProUGUI gatheringTimeText;
    [SerializeField] TextMeshProUGUI resourcesPerGatherText;

    [SerializeField] TextMeshProUGUI discoveringTimeText;

    private DroneController target;

    public void Show(DroneController newTarget)
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
        var data = target.GetData();

        typeText.text = GetRoleName(data.role);
        stateText.text = $"State: {GetStateName(data.state)}";

        discoveringTimeText.gameObject.SetActive(data.discoveringTime.HasValue);

        if (data.discoveringTime.HasValue)
            discoveringTimeText.text = $"Discovering Time: {data.discoveringTime:0.0} s/unit";

        inventoryText.gameObject.SetActive(data.remainingInventory.HasValue);

        if (data.remainingInventory.HasValue)
            inventoryText.text = $"Inventory: {data.inventoryCapacity - data.remainingInventory}/{data.inventoryCapacity}";

        gatheringTimeText.gameObject.SetActive(data.gatheringTime.HasValue);

        if (data.gatheringTime.HasValue)
            gatheringTimeText.text = $"Gather: {data.gatheringTime:0.0} s/unit";

        resourcesPerGatherText.gameObject.SetActive(data.resourcesPerGather.HasValue);

        if (data.resourcesPerGather.HasValue)
            resourcesPerGatherText.text = $"Yield per action: {data.resourcesPerGather}";
    }
    private static string GetRoleName(DroneRole role)
    {
        return role switch
        {
            DroneRole.Worker => "Worker Drone",
            DroneRole.Scout => "Scout Drone",
            _ => "Unknown"
        };
    }

    private static string GetStateName(DroneStateType state)
    {
        return state switch
        {
            DroneStateType.Idle => "Idle",
            DroneStateType.MovingToSector => "Moving to sector",
            DroneStateType.Discovering => "Discovering area",
            DroneStateType.MovingToBaseSector => "Returning to base",
            DroneStateType.Gathering => "Gathering resources",
            DroneStateType.Searching => "Searching",
            DroneStateType.Unloading => "Unloading cargo",
            _ => "Unknown"
        };
    }
}
