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
        if (otherCard != null && !thisDraggableCard.isCrafting)
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
            thisDraggableCard.StartCraftingAnimation();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
}