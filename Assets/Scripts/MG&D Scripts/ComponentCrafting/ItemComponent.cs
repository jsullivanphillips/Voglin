using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform boundsBox;
    public Transform craftingArea;

    [SerializeField]
    protected CanvasGroup canvasGroup;
    protected bool isDraggable = true;

    private Vector2 originalPosition;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        
        originalPosition = this.transform.position;

        this.transform.SetParent(craftingArea);
        this.transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }
        // Calculate the position of the item
        Vector2 position = eventData.position;

        // Set this objects position
        this.transform.position = position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if(!isDraggable)
        {
            return;
        }

        // Get the corners of the item in screen space
        Vector3[] itemCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(itemCorners);

        // Convert the bounds of the box to screen space
        Vector3[] boxCorners = new Vector3[4];
        boundsBox.GetWorldCorners(boxCorners);
        Debug.Log("IsOver Item Slot: " + ItemSlotManager.Instance.isItemSlotMousedOver());
        // Check if the item is outside the bounds box and not in an item slot
        if ((itemCorners[0].x < boxCorners[0].x || itemCorners[2].x > boxCorners[2].x || 
            itemCorners[0].y < boxCorners[0].y || itemCorners[2].y > boxCorners[2].y) &&
            !ItemSlotManager.Instance.isItemSlotMousedOver())
        {
            Debug.Log("Settig to original position");
            // If it is, return the ItemComponent to its original position
            this.transform.position = originalPosition;
        }

        canvasGroup.blocksRaycasts = true;
    } 
}
