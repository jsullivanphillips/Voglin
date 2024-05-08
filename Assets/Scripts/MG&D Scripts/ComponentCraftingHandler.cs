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
           bool recipeExists = CraftingRecipes.Instance.DoesRecipeExist(thisItemComponent.GetComponentSO(), otherComponent.GetComponentSO());
           if(!recipeExists)
           {
                return;
           }
           otherComponent.SetCraftingFlagTrue(thisItemComponent);
           otherComponent.transform.position = transform.position + new Vector3(0f, -30f, 0f);

           thisItemComponent.StartCraftingAnimation(otherComponent);
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

    public void CraftItems(ComponentSO thisComponent, ComponentSO otherComponent)
    {
        if(CraftingRecipes.Instance.DoesRecipeExist(thisComponent, otherComponent))
        {
            ComponentSO result = CraftingRecipes.Instance.GetCraftingResult(thisComponent, otherComponent);

            CraftingTable.Instance.SpawnItemComponent(result);

            CraftingTable.Instance.RemoveItemComponent(otherComponent.id);
            CraftingTable.Instance.RemoveItemComponent(thisComponent.id);
        }
    }
}

