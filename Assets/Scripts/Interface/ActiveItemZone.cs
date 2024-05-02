using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveItemZone : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private HUDActiveItemZone _HUDActiveItemZone;

    public void OnDrop(PointerEventData eventData)
    {
        if(PlayerActiveItems.Instance.GetActiveItemsCount() >= 4)
        {
            Debug.Log("You can't have more than 4 active items");
            return;
        }

        ActiveCard card = eventData.pointerDrag.GetComponent<ActiveCard>();
        if (card != null)
        {
            Debug.Log("Card dropped in active item zone");
            card.transform.SetParent(this.transform);
            card.isInActiveItemsZone = true;
            card.isInRack = true;
            _HUDActiveItemZone.AddActiveItem(card.GetActiveItemSO(), card.GetId());
            PlayerActiveItems.Instance.AddActiveItem(card.GetActiveItemSO(), card.GetId());
        }
    }
}
