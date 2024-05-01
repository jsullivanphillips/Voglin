using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offset = Vector2.zero;
    public RectTransform boundsBox; // The box that defines the bounds
    [SerializeField]
    private CanvasGroup canvasGroup;
    private Guid id;

    private void Awkae()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        offset = new Vector2(this.transform.position.x, this.transform.position.y) - eventData.position;
    
        // Move this card to the end of its parent's list of children so it appears on top
        this.transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position + offset;
    
        // Get the corners of the card in screen space
        Vector3[] cardCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(cardCorners);
    
        // Calculate the size of the card in screen space
        Vector2 size = RectTransformUtility.WorldToScreenPoint(null, cardCorners[2]) - RectTransformUtility.WorldToScreenPoint(null, cardCorners[0]);
    
        // Convert the bounds of the box to screen space
        Vector3[] boxCorners = new Vector3[4];
        boundsBox.GetWorldCorners(boxCorners);
    
        // Adjust the clamping values to account for the size of the card
        float clampedX = Mathf.Clamp(pos.x, boxCorners[0].x + size.x / 2, boxCorners[2].x - size.x / 2);
        float clampedY = Mathf.Clamp(pos.y, boxCorners[0].y + size.y / 2, boxCorners[2].y - size.y / 2);
    
        this.transform.position = new Vector2(clampedX, clampedY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        canvasGroup.blocksRaycasts = true;
    }

    public void SetGuid(Guid guid)
    {
        id = guid;
    }

    public Guid GetGuid()
    {
        return id;
    }
}
