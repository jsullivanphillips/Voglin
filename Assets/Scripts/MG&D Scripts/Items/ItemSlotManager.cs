using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    public static ItemSlotManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    public bool isItemSlotMousedOver()
    {
        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.IsMouseOver())
            {
                return true;
            }
        }
        return false;
    }

}
