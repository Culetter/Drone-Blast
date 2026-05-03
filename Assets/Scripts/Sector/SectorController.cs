using UnityEngine;
using UnityEngine.Assemblies;

public class SectorController : MonoBehaviour
{
    ResourcesController resourcesController;

    [SerializeField] Material discoveredMaterial;

    private bool isDiscovered = false;

    private void Awake()
    {
        resourcesController = GetComponent<ResourcesController>();
    }

    public bool IsDiscovered() => isDiscovered;

    [ContextMenu("Change State")]
    public void ChangeDiscoveredState()
    {
        isDiscovered = true;
        GetComponent<Renderer>().material = discoveredMaterial;
        resourcesController.Init();
    }

    public void Gather(int amount)
    {
        resourcesController.UpdateResources(amount);
    }
}
