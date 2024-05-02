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
            if(otherCard.GetCardType() == CardType.Passive)
            {
                simpleCardAnimations.ReturnToOriginalPosition();
                otherCard.SetCraftingFlagTrue(thisDraggableCard);
                //Passive card dropped in crafting
            }
            else if(otherCard.GetCardType() == CardType.Active)
            {
                simpleCardAnimations.ReturnToOriginalPosition();
                otherCard.SetCraftingFlagTrue(thisDraggableCard);
                //Active card dropped in crafting
            }
            thisDraggableCard.StartCraftingAnimation(otherCard);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(thisDraggableCard.isInRack)
            return;

        if (eventData.pointerDrag != null)
        {
            if(!thisDraggableCard.isCrafting)
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
                CraftingTableItemManager.Instance.SpawnActiveCard((ActiveItemSO)result, transform.position);
            }
            Destroy(otherCard.gameObject);
            Destroy(thisCard.gameObject);
        }
    }
}