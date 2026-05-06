using UnityEngine;
using UnityEngine.UI;

public class DroneIconController : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] Sprite searchingIcon;
    [SerializeField] Sprite gatheringIcon;

    public void SetSerachingIcon()
    {
        iconImage.sprite = searchingIcon;
        iconImage.gameObject.SetActive(true);
    }
    public void SetGatheringIcon()
    {
        iconImage.sprite = gatheringIcon;
        iconImage.gameObject.SetActive(true);
    }

    public void HideIcon() => iconImage.gameObject.SetActive(false);
}
