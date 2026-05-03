using UnityEngine;

public class BaseSectorController : MonoBehaviour
{
    [SerializeField] int storage = 0;
    private LogicScript logic;

    private void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<LogicScript>();
    }

    public void LoadStorage(int value)
    {
        storage += value;
        logic.setResources(storage);
    }
}
