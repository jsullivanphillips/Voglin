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

    public bool isCrafting = false;
    public Transform originalParent;


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

        // Check if the item is outside the bounds box and not in an item slot
        if ((itemCorners[0].x < boxCorners[0].x || itemCorners[2].x > boxCorners[2].x || 
            itemCorners[0].y < boxCorners[0].y || itemCorners[2].y > boxCorners[2].y) &&
            !ItemSlotManager.Instance.isItemSlotMousedOver())
        {
            this.transform.position = originalPosition;
        }

        if(ItemSlotManager.Instance.isItemSlotMousedOver())
        {
            SetOriginalParent();
        }
        else if (!(itemCorners[0].x < boxCorners[0].x || itemCorners[2].x > boxCorners[2].x || 
            itemCorners[0].y < boxCorners[0].y || itemCorners[2].y > boxCorners[2].y))
        {
            SetCraftingAreaParent();
        }


        canvasGroup.blocksRaycasts = true;
    } 

    public void SetOriginalParent()
    {
        this.transform.SetParent(originalParent);
    }

    public void SetCraftingAreaParent()
    {
        this.transform.SetParent(craftingArea);
    }
}
