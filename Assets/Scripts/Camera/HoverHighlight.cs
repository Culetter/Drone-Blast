using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverHighlight : MonoBehaviour
{
    [SerializeField] SelectionController selectionController;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleHighlight()
    {
        if (highlight != null && highlight != selection)
        {
            SetOutline(highlight, false);
            highlight = null;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit))
        {
            Transform newHover = raycastHit.transform;

            if (newHover == selection)
                return;

            if (IsValid(newHover))
            {
                highlight = newHover;
                SetOutline(highlight, true);
            }
        }
    }

    void HandleClick()
    {
        if (highlight)
        {
            selectionController.Select(highlight.gameObject);

            if (selection != null)
                SetOutline(selection, false);

            selection = highlight;
            SetOutline(selection, true);

            highlight = null;
        }
        else
        {
            if (selection && !EventSystem.current.IsPointerOverGameObject())
            {
                SetOutline(selection, false);
                selection = null;
                selectionController.ClearSelection();
            }
        }
    }

    void SetOutline(Transform t, bool state)
    {
        var outline = t.GetComponent<Outline>();

        if (outline == null && state)
        {
            outline = t.gameObject.AddComponent<Outline>();
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 7f;
        }

        if (outline != null)
            outline.enabled = state;
    }

    bool IsValid(Transform t)
    {
        return t.CompareTag("Sector")
            || t.CompareTag("Base Sector")
            || t.CompareTag("Scout")
            || t.CompareTag("Worker");
    }
}
