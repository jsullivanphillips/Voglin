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
            List<Tuple<CardType, int>> drops = CardDropChanceManager.Instance.GetOrbDrops(orb.GetRank());
            foreach (Tuple<CardType, int> drop in drops)
            {
                // CardType is card type
                // int is rank of card (i.e. 1, 2, 3, etc.)
                if(drop.Item1 == CardType.Active)
                {
                    ActiveItemSO activeItem = CardSpawnMaster.Instance.GetRandomActiveItem(drop.Item2);
                    craftingTableItemManager.SpawnActiveCard(activeItem);
                }
                else
                {
                    PassiveItemSO passiveItem = CardSpawnMaster.Instance.GetRandomPassiveItem(drop.Item2);
                    craftingTableItemManager.SpawnPassiveCard(passiveItem);
                }
                
            }
            Destroy(orb.gameObject);
        }
    }
}
