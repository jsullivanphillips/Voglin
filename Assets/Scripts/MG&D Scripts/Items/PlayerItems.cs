using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float GetPhysicalPower()
    {
        float physicalPower = 0;
        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                if (stat.stat == Stat.PhysicalPower)
                {
                    physicalPower += stat.value;
                }
            }
        }
        return physicalPower;
    }

    public float GetMagicPower()
    {
        float magicPower = 0;
        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                if (stat.stat == Stat.MagicPower)
                {
                    magicPower += stat.value;
                }
            }
        }
        return magicPower;
    }

    public float GetScalingStat(ScalingStat scalingStat)
    {
        float scalingStatValue = 0;

        if(scalingStat == ScalingStat.MaxHealth)
        {
            return GetMaxHealth();
        }


        foreach (ComponentSO item in playerItems)
        {
            foreach (StatFloatPair stat in item.stats)
            {
                switch (scalingStat)
                {
                    case ScalingStat.PhysicalPower:
                        if (stat.stat == Stat.PhysicalPower)
                        {
                            scalingStatValue += stat.value;
                        }
                        break;
                    case ScalingStat.MagicPower:
                        if (stat.stat == Stat.MagicPower)
                        {
                            scalingStatValue += stat.value;
                        }
                        break;
                }
            }
        }

        return scalingStatValue;
    }

    public string GetStatHexColor(ScalingStat scalingStat)
    {
        switch (scalingStat)
        {
            case ScalingStat.PhysicalPower:
                return "<color=\"orange\">";
            case ScalingStat.MagicPower:
                return "<color=#00FFFF>";
            case ScalingStat.MaxHealth:
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
