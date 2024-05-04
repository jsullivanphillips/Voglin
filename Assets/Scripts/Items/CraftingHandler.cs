using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    SimpleCardAnimations simpleCardAnimations;
    [SerializeField]
    DraggableCard thisDraggableCard;
    
    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard otherCard = eventData.pointerDrag.GetComponent<DraggableCard>();
        
        if (otherCard != null && !thisDraggableCard.isCrafting && !thisDraggableCard.isInRack)
        {
            bool recipeExists = CraftingRecipes.Instance.DoesRecipeExist(thisDraggableCard.GetItemSO(), otherCard.GetItemSO());
            if(!recipeExists)
            {
                return;
            }
            simpleCardAnimations.ReturnToOriginalPosition();
            otherCard.SetCraftingFlagTrue(thisDraggableCard);
            otherCard.transform.position = transform.position + new Vector3(0f, -30f, 0f);
        
            
            thisDraggableCard.StartCraftingAnimation(otherCard);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(thisDraggableCard.isInRack)
            return;

        if (eventData.pointerDrag != null)
        {
            DraggableCard otherCard = eventData.pointerDrag.GetComponent<DraggableCard>();
            if(otherCard == null)
            {
                return;
            }

            bool recipeExists = CraftingRecipes.Instance.DoesRecipeExist(thisDraggableCard.GetItemSO(), otherCard.GetItemSO());
            if(!thisDraggableCard.isCrafting && recipeExists)
            {
                simpleCardAnimations.StartHoverWithDraggedCardAnimation();
            }
           //Mouse entered while dragging
        }
        else
        {
            simpleCardAnimations.ReturnToOriginalPosition();
            //Mouse entered
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if(thisDraggableCard.isInRack)
            return;

        if (eventData.pointerDrag != null)
        {
            simpleCardAnimations.StopHoverWithDraggedCardAnimation();
            //Mouse exited while dragging
        }
        else
        {
            simpleCardAnimations.ReturnToOriginalPosition();
            //Mouse exited
        }
    }

    public void CraftItems(DraggableCard thisCard, DraggableCard otherCard)
    {
        Debug.Log("We crafting Items");
        if(CraftingRecipes.Instance.DoesRecipeExist(thisCard.GetItemSO(), otherCard.GetItemSO()))
        {
            Debug.Log("Recipe exists");
            ItemSO result = CraftingRecipes.Instance.GetCraftingResult(thisCard.GetItemSO(), otherCard.GetItemSO());

            if(result.cardType == CardType.Passive)
            {
                CraftingTableItemManager.Instance.SpawnPassiveCard((PassiveItemSO)result, transform.position);
            }
            else if(result.cardType == CardType.Active)
            {
                ActiveItemSO activeResult = (ActiveItemSO)result;
                activeResult.cooldown = Random.Range(activeResult.cooldownRange.min, activeResult.cooldownRange.max);
                CraftingTableItemManager.Instance.SpawnActiveCard(activeResult, transform.position);
            }
            CraftingTableItemManager.Instance.RemoveCard(otherCard.GetId());
            CraftingTableItemManager.Instance.RemoveCard(thisCard.GetId());
        }
    }
}