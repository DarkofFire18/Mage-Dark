using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public float moveSpeed = 5f;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            RectTransform dropZoneRect = GetComponent<RectTransform>();
            RectTransform draggedRect = draggedObject.GetComponent<RectTransform>();

            if (dropZoneRect != null && draggedRect != null)
            {
                // Starte eine Coroutine, um das Objekt sanft in die Mitte zu bewegen
                StartCoroutine(MoveToCenter(draggedRect, dropZoneRect));
            }

            // Falls das Objekt ein MaterialItem ist, setzen wir es als "im Ofen"
            MaterialItem material = draggedObject.GetComponent<MaterialItem>();
            if (material != null)
            {
                material.SetInOven(true);
            }

            Debug.Log($"{draggedObject.name} wurde in die Ofenzone gelegt!");
        }
    }

    private System.Collections.IEnumerator MoveToCenter(RectTransform draggedRect, RectTransform dropZoneRect)
    {
        Vector3 startPosition = draggedRect.position;
        Vector3 targetPosition = dropZoneRect.position;
        targetPosition.y -= 50;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            draggedRect.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }

        // Am Ziel sicherstellen, dass die Position exakt passt
        draggedRect.position = targetPosition;
    }

    // Falls das Material den Ofen verlässt, stoppen wir die Erwärmung
    private void OnTriggerExit(Collider other)
    {
        MaterialItem material = other.GetComponent<MaterialItem>();
        if (material != null)
        {
            material.SetInOven(false);
        }
    }
}
