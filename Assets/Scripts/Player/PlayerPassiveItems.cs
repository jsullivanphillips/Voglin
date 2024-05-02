using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveItems : MonoBehaviour
{
    public static PlayerPassiveItems Instance { get; private set; }

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

    Dictionary<int, PassiveItemSO> passiveItems = new Dictionary<int, PassiveItemSO>();

    public void AddPassiveItem(PassiveItemSO item, int id)
    {
        passiveItems[id] = item;
        item.id = id;
    }

    public void RemovePassiveItem(int id)
    {
        passiveItems.Remove(id);
    }

    public int GetPassiveItemsCount()
    {
        return passiveItems.Count;
    }

    public float GetDamageBonuses()
    {
        float damageBonuses = 0f;
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.passiveEffects.Contains(PassiveEffect.DamageBoost))
            {
                damageBonuses += item.effectValues[item.passiveEffects.IndexOf(PassiveEffect.DamageBoost)];
            }
        }
        return damageBonuses;
    }
}
