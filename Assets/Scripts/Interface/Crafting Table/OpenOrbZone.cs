using System;
using System.Collections.Generic;
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
            Rarity rarity = orb.GetRarity();
            // do drop chance logic here
            for (int i = 0; i < 2; i++)
            {
                ItemSO item = ItemDatabase.Instance.GetRandomItemAtRarity(rarity);
                if (item is ActiveItemSO)
                {
                    ActiveItemSO activeItem = item as ActiveItemSO;
                    activeItem.cooldown = UnityEngine.Random.Range(activeItem.cooldownRange.min, activeItem.cooldownRange.max);
                    craftingTableItemManager.SpawnActiveCard(activeItem);
                }
                else if (item is PassiveItemSO)
                {
                    craftingTableItemManager.SpawnPassiveCard(item as PassiveItemSO);
                }
            }
            Destroy(orb.gameObject);
        }
    }
}
