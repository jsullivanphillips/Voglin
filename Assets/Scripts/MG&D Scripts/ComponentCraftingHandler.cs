using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ComponentCraftingHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    ItemComponent thisItemComponent;

    void Awake()
    {
        thisItemComponent = GetComponent<ItemComponent>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemComponent otherComponent = eventData.pointerDrag.GetComponent<ItemComponent>();

        if(otherComponent != null && !thisItemComponent.isCrafting)
        {
            Debug.Log("Craft");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // okay
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // okay
    }
}

