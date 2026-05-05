using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUI : MonoBehaviour
{
    public event System.Action<SelectionAction> OnActionClicked;

    [SerializeField] Button discoverButton;
    [SerializeField] Button collectButton;

    private readonly Dictionary<SelectionAction, Button> actionButtons = new();

    private RectTransform parent;
    private Camera cam;

    private Vector2 offset = new Vector2(100, 100);

    private Transform target;


    private void Awake()
    {
        cam = Camera.main;
        parent = (RectTransform)discoverButton.transform.parent;

        actionButtons[SelectionAction.Discover] = discoverButton;
        actionButtons[SelectionAction.Gather] = collectButton;

        Hide();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        UpdatePosition();
    }

    public void Show(Transform worldTarget, List<SelectionAction> actions)
    {
        Hide();

        if (actions == null)
            return;

        target = worldTarget;

        UpdatePosition();

        foreach (var action in actions)
        {
            if (!actionButtons.TryGetValue(action, out var button))
                continue;

            button.gameObject.SetActive(true);
        }
    }
    private void UpdatePosition()
    {
        Vector2 screenPoint = cam.WorldToScreenPoint(target.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            screenPoint,
            null,
            out Vector2 localPoint
        );

        Vector2 finalPos = localPoint + offset;

        foreach (var button in actionButtons.Values)
        {
            if (button.gameObject.activeSelf)
                button.GetComponent<RectTransform>().anchoredPosition = finalPos;
        }
    }
    public void Hide()
    {
        foreach (var btn in actionButtons.Values)
            btn.gameObject.SetActive(false);
    }
    public void OnDiscoverClicked()
    {
        OnActionClicked?.Invoke(SelectionAction.Discover);
    }
    public void OnGatherClicked()
    {
        OnActionClicked?.Invoke(SelectionAction.Gather);
    }
}