using UnityEngine;

public class BaseSectorController : MonoBehaviour
{
    [SerializeField] int storage = 0;
    private LogicScript logic;
    [SerializeField] bool autoMiningUpgrade = false;
    public bool HasAutoMiningUpgrade() => autoMiningUpgrade;
    public bool BuyAutoMiningUpgrade(int price)
    {
        if (price > storage)
            return false;

        autoMiningUpgrade = true;
        storage -= price;
        SetResources();
        return true;
    }

    private void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<LogicScript>();
    }

    public void LoadStorage(int value)
    {
        storage += value;
        SetResources();
    }
    private void SetResources() => logic.SetResources(storage);
}
