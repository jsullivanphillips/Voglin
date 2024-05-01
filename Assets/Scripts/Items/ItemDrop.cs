using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : PickupableObject
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            CraftingTableItemManager.Instance.CreateOrb();
            Destroy(gameObject);
        }
    }
}
