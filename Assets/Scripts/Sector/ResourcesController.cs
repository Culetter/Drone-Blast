using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    [SerializeField] GameObject largeResourcePrefab;
    [SerializeField] GameObject mediumResourcePrefab;
    [SerializeField] GameObject smallResourcePrefab;

    [SerializeField] int minResourceAmount = 1;
    [SerializeField] int maxResourceAmount = 15;

    private GameObject currentResourceObject;
    [SerializeField] int currentResourceAmount;

    private float largeThreshold;
    private float mediumThreshold;

    ResourceType currentResourceType;

    public void Init()
    {
        currentResourceAmount = Random.Range(minResourceAmount, maxResourceAmount + 1);
        currentResourceType = ResourceType.None;

        int range = maxResourceAmount - minResourceAmount;

        largeThreshold = minResourceAmount + range * 0.66f;
        mediumThreshold = minResourceAmount + range * 0.33f;

        UpdateResources(0);
    }

    public int GetResourcesAmoun() => currentResourceAmount;

    public void UpdateResources(int gatheredAmount)
    {
        currentResourceAmount -= gatheredAmount;

        ResourceType newType = SelectResourceType(currentResourceAmount);

        GameObject selectedPrefab = GetPrefab(newType);

        if (currentResourceType != newType)
        {
            currentResourceType = newType;
            SetResourcePrefab(selectedPrefab);
        }
    }

    private ResourceType SelectResourceType(int amount)
    {
        if (amount >= largeThreshold)
            return ResourceType.Large;

        if (amount >= mediumThreshold)
            return ResourceType.Medium;

        if (amount >= minResourceAmount)
            return ResourceType.Small;

        return ResourceType.None;
    }

    private GameObject GetPrefab(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Large: return largeResourcePrefab;
            case ResourceType.Medium: return mediumResourcePrefab;
            case ResourceType.Small: return smallResourcePrefab;
            default: return null;
        }
    }

    private void SetResourcePrefab(GameObject newPrefab)
    {
        if (newPrefab == null)
        {
            if (currentResourceObject != null)
                Destroy(currentResourceObject);

            currentResourceObject = null;
            return;
        }

        if (currentResourceObject != null)
            Destroy(currentResourceObject);

        currentResourceObject = Instantiate(newPrefab, transform);
        currentResourceObject.transform.localScale = new Vector3(
            1f / transform.lossyScale.x,
            1f / transform.lossyScale.y,
            1f / transform.lossyScale.z
        );
    }
}