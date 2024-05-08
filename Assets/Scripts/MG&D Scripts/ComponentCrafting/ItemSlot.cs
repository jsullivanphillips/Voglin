using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private ItemComponent currentItem;

    private bool isMouseOver = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item dropped in item slot");
        ItemComponent item = eventData.pointerDrag.GetComponent<ItemComponent>();
        if(item != null)
        {
            if(currentItem != null)
            {
                
            }
            item.transform.SetParent(transform);
            item.transform.position = transform.position;
            currentItem = item;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    public bool IsMouseOver()
    {
        return isMouseOver;
    }
}
