using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerItems : MonoBehaviour
{
    #region Singleton
    public static PlayerItems Instance { get; private set; }

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
    #endregion

    [SerializeField]
    private Player player;
    [SerializeField]
    private PlayerMovement playerMovement;

    private List<ComponentSO> playerItems = new List<ComponentSO>();

    public void AddItem(ComponentSO item)
    {
        // Check if playerItems already contains an item with the same id
        if (playerItems.Any(existingItem => existingItem.id == item.id))
        {
            return; 
        }
    
        Debug.Log("Adding item: " + item.name);
        playerItems.Add(item);
        RecalculateStats();
    }

    public void RemoveItem(ComponentSO itemToRemove)
    {
        int idToRemove = itemToRemove.id;
        playerItems.RemoveAll(item => item.id == idToRemove);
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        player.RecalculateHealthBonuses();
    }

    public float GetHealthBonus()
    {
        float healthBonus = 0;
        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                if (stat.stat == Stat.Health)
                {
                    healthBonus += stat.value;
                }
            }
        }
        return healthBonus;
    }

    public float GetHealthRegenBonus()
    {
        float healthRegenBonus = 0;
        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                if (stat.stat == Stat.HealthRegen)
                {
                    healthRegenBonus += stat.value;
                }
            }
        }
        return healthRegenBonus;
    }

    public float GetAttackSpeed()
    {
        float attackSpeed = 0;
        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                if (stat.stat == Stat.AttackSpeed)
                {
                    attackSpeed += stat.value;
                }
            }
        }
        attackSpeed += PlayerAbilityManager.Instance.GetAttackSpeed();
        return attackSpeed;
    }

    public float GetScalingStat(Stat scalingStat)
    {
        if(scalingStat == Stat.Health)
        {
            return GetMaxHealth();
        }

        
        float value = 0;
        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                if (stat.stat == scalingStat)
                {
                    value += stat.value;
                }
            }
        }
        return value;
    }

    public string GetStatHexColor(Stat scalingStat)
    {
        switch (scalingStat)
        {
            case Stat.PhysicalPower:
                return "<color=\"orange\">";
            case Stat.MagicPower:
                return "<color=#00FFFF>";
            case Stat.Health:
                return "<color=#FF0000>";
            default:
                return "<color=#FFFFFF>";
        }

    }

    public float GetMaxHealth()
    {
        return player.GetMaxHealth();
    }

}
