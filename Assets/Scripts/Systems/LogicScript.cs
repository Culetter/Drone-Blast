using TMPro;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    [SerializeField] int storageResources;
    [SerializeField] TextMeshProUGUI resourcesText;
    public void SetResources(int resourcesToSet)
    {
        storageResources = resourcesToSet;
        resourcesText.text = $"Resources: {storageResources}";
    }
}
