using UnityEngine;
using UnityEngine.EventSystems;

public class PassiveItemZone : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private HUDPassiveItemZone _HUDPassiveItemZone;

    public void OnDrop(PointerEventData eventData)
    {
        if(PlayerPassiveItems.Instance.GetPassiveItemsCount() >= 4)
        {
            Debug.Log("You can't have more than 4 passive items");
            return;
        }
        
        PassiveCard card = eventData.pointerDrag.GetComponent<PassiveCard>();
        if (card != null)
        {
            Debug.Log("Card dropped in passive item zone");
            card.transform.SetParent(this.transform);
            card.isInPassiveItemZone = true;
            card.isInRack = true;
            _HUDPassiveItemZone.AddPassiveItem(card.GetPassiveItemSO(), card.GetId());
            PlayerPassiveItems.Instance.AddPassiveItem(card.GetPassiveItemSO(), card.GetId());
        }
    }
}