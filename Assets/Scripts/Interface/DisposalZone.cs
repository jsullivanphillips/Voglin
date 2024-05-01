using UnityEngine;
using UnityEngine.EventSystems;

public class DisposalZone : MonoBehaviour, IDropHandler
{
    [SerializeField]
    CraftingTableCardManager craftingTableCardManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard card = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (card != null)
        {
            Debug.Log("Card dropped in disposal zone");
            craftingTableCardManager.RemoveCard(card.GetGuid());
        }
    }
}