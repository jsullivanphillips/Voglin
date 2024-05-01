using UnityEngine;
using UnityEngine.EventSystems;

public class OpenOrbZone : MonoBehaviour, IDropHandler
{
    [SerializeField]
    CraftingTableItemManager craftingTableItemManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableOrb orb = eventData.pointerDrag.GetComponent<DraggableOrb>();
        if (orb != null)
        {
            Debug.Log("Orb dropped in opening zone");
            craftingTableItemManager.Create3CardsBtn();
            Destroy(orb.gameObject);
        }
    }
}
