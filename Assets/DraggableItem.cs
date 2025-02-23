using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Falls das Item auf dem Amboss liegt, setze isOnAnvil auf false,
        // da es jetzt aus dem Amboss genommen wird
        MaterialItem material = GetComponent<MaterialItem>();
        if (material != null && material.isOnAnvil)
        {
            material.isOnAnvil = false;
            Debug.Log($"{gameObject.name} wurde aus dem Amboss entfernt!");
        }

        // Speichere den ursprünglichen Parent, um ihn später wiederherzustellen
        originalParent = transform.parent;
        // Reparenting: Das Item wird an den Canvas angehängt
        transform.SetParent(canvas.transform);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Hier kannst du das Item wieder einem passenden Parent zuordnen,
        // falls es über einer gültigen Dropzone losgelassen wurde.
        if (eventData.pointerEnter != null)
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else
        {
            transform.SetParent(originalParent);
        }
    }
}
