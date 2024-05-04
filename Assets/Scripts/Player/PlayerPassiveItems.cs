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

    public float GetCritChance()
    {
        float critChance = 0f;
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.passiveEffects.Contains(PassiveEffect.CriticalChance))
            {
                critChance += item.effectValues[item.passiveEffects.IndexOf(PassiveEffect.CriticalChance)];
            }
        }
        if(ContainsGoldenClover())
        {
            critChance *= 2;
        }
        if(critChance > 100f)
        {
            critChance = 100f;
        }
        return critChance * 0.01f;
    }

    public float GetCriticalStrikeDamageBonus()
    {
        float criticalStrikeDamageBonus = 0f;
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.passiveEffects.Contains(PassiveEffect.CriticalStrikeDamage))
            {
                criticalStrikeDamageBonus += item.effectValues[item.passiveEffects.IndexOf(PassiveEffect.CriticalStrikeDamage)];
            }
        }
        if(ContainsGoldenClover())
        {
            criticalStrikeDamageBonus -= 20;
        }
        return criticalStrikeDamageBonus * 0.01f;
    }

    public float GetMoveSpeedBonuses()
    {
        float moveSpeedBonuses = 0f;
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.passiveEffects.Contains(PassiveEffect.MoveSpeed))
            {
                moveSpeedBonuses += item.effectValues[item.passiveEffects.IndexOf(PassiveEffect.MoveSpeed)];
            }
        }
        return moveSpeedBonuses / 25f;
    }

    public float GetRangeBonus()
    {
        float rangeBonus = 0f;
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.passiveEffects.Contains(PassiveEffect.RangeBoost))
            {
                rangeBonus += item.effectValues[item.passiveEffects.IndexOf(PassiveEffect.RangeBoost)];
            }
        }
        return rangeBonus;
    }

    public float GetCooldownReductionBonus()
    {
        float cooldownReductionBonus = 0f;
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.passiveEffects.Contains(PassiveEffect.CooldownReduction))
            {
                cooldownReductionBonus += item.effectValues[item.passiveEffects.IndexOf(PassiveEffect.CooldownReduction)];
            }
        }
        return cooldownReductionBonus;
    }

    public bool ContainsGoldenClover()
    {
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.itemName == "Golden Clover") // Assuming the name property is called itemName
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsGreenFeather()
    {
        foreach (PassiveItemSO item in passiveItems.Values)
        {
            if (item.itemName == "Green Feather") // Assuming the name property is called itemName
            {
                return true;
            }
        }
        return false;
    }
}
