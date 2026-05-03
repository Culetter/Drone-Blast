using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    [SerializeField] int storageResources;
    [SerializeField] TextMeshProUGUI resourcesText;

    public void setResources(int resourcesToSet)
    {
        storageResources = resourcesToSet;
        resourcesText.text = $"Resources: {storageResources}";
    }
}
