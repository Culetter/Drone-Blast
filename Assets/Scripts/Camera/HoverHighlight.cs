using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverHighlight : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private Vector2 offset = new Vector2(100, 100);
    private GameObject targetSector;
    [SerializeField] Button discoverButton;
    [SerializeField] Button collectButton;
    private DroneAvailability droneAvailability;

    private void Awake()
    {
        droneAvailability = GameObject.FindGameObjectWithTag("Logic Manager").GetComponent<DroneAvailability>();
    }

    void Update()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if ((highlight.CompareTag("Sector") || highlight.CompareTag("Base Sector")) && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else highlight = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;

                if (highlight.gameObject.GetComponent<SectorController>().IsDiscovered())
                    ShowButton(collectButton, highlight);
                else ShowButton(discoverButton, highlight);

                highlight = null;
            }
            else
            {
                if (selection)
                    HideButtons();
            }
        }
    }
    void ShowButton(Button button, Transform target)
    {
        targetSector = target.gameObject;

        RectTransform parent = (RectTransform)button.transform.parent;

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(target.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            screenPoint,
            null,
            out Vector2 localPoint
        );

        button.GetComponent<RectTransform>().anchoredPosition = localPoint + offset;
        button.gameObject.SetActive(true);
    }
    void HideButtons()
    {
        selection.gameObject.GetComponent<Outline>().enabled = false;
        selection = null;

        discoverButton.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(false);
        targetSector = null;
    }
    public void Gather()
    {
        if (targetSector == null)
            return;

        DroneController drone = droneAvailability.GetAvailableDrone(DroneRole.Worker);

        if (drone != null && drone is IWorkerDrone worker)
        {
            worker.Gather(targetSector);
            HideButtons();
        }
    }

    public void Discover()
    {
        if (targetSector == null)
            return;

        DroneController drone = droneAvailability.GetAvailableDrone(DroneRole.Scout);

        if (drone != null && drone is IScoutDrone scout)
        {
            scout.Discover(targetSector);
            HideButtons();
        }
    }
}
