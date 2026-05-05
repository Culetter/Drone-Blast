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
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if ((highlight.CompareTag("Sector") || highlight.CompareTag("Base Sector")) && highlight != selection)
            {
                Outline outline = highlight.gameObject.GetComponent<Outline>();
                if (outline != null)
                    outline.enabled = true;
                else
                {
                    outline = highlight.gameObject.AddComponent<Outline>();

                    outline.enabled = true;
                    outline.OutlineColor = Color.red;
                    outline.OutlineWidth = 7.0f;
                }
            }
            else highlight = null;
        }
    }

    void HandleClick()
    {
        if (highlight)
        {
            selectionController.Select(highlight.gameObject);

            if (selection != null)
                selection.gameObject.GetComponent<Outline>().enabled = false;

            selection = raycastHit.transform;
            selection.gameObject.GetComponent<Outline>().enabled = true;

            highlight = null;
        }
        else
        {
            if (selection && !EventSystem.current.IsPointerOverGameObject())
            {
                selection.gameObject.GetComponent<Outline>().enabled = false;
                selection = null;
                selectionController.ClaerSelection();
            }
        }
    }
}
