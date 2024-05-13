using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private ItemComponent currentItem;

    private bool isMouseOver = false;


    public void OnDrop(PointerEventData eventData)
    {
        ItemComponent item = eventData.pointerDrag.GetComponent<ItemComponent>();
        if(item != null)
        {
            PlayerItems.Instance.AddItem(item.GetComponentSO());
            
            item.SetOriginalParent();
            item.transform.position = transform.position;
            item.isInRack = true;
            item.currentItemSlot = this;
            currentItem = item;
        }
    }

    public void Swap(ItemComponent itemInRack, ItemComponent newItem)
    {
        itemInRack.isInRack = newItem.isInRack;
        itemInRack.transform.position = newItem.GetOriginalPosition();
        if(!newItem.isInRack)
        {
            itemInRack.currentItemSlot = null;
            itemInRack.SetCraftingAreaParent();
            PlayerItems.Instance.RemoveItem(itemInRack.GetComponentSO());
            PlayerItems.Instance.AddItem(newItem.GetComponentSO());
        }
        else
        {
            itemInRack.currentItemSlot = newItem.currentItemSlot;
        }
        
        newItem.isInRack = true;
        newItem.SetOriginalParent();
        newItem.transform.position = transform.position;
        newItem.SetOriginalPosition(transform.position);
        newItem.currentItemSlot = this;
        
        currentItem = newItem;
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
