using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePanelUI : MonoBehaviour
{
    private BaseSectorController target;
    [SerializeField] Button buyButton;
    private int AutoGatheringUpgradePrice = 5; 

    public void Show(BaseSectorController newTarget)
    {
        target = newTarget;

        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        target = null;
        gameObject.SetActive(false);
    }
    public void Buy()
    {
        if (target.BuyAutoMiningUpgrade(AutoGatheringUpgradePrice))
            buyButton.interactable = false;
    }
}
