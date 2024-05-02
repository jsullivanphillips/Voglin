using UnityEngine;
using UnityEngine.EventSystems;

public class DisposalZone : MonoBehaviour, IDropHandler
{
    [SerializeField]
    CraftingTableItemManager craftingTableItemManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard card = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (card != null)
        {
            Debug.Log("Card dropped in disposal zone");
            craftingTableItemManager.RemoveCard(card.GetId());
        }
    }
}