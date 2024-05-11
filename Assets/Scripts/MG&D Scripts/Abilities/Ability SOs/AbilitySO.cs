using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    Projectile,
    AoE,
    Orbiter,
    Lobbed,
    BasicAttack
}

public enum ScalingStat
{
    PhysicalPower,
    MagicPower,
    MaxHealth
}

[System.Serializable]
public class ScalingStats
{
    public ScalingStat scalingStat;
    public float scaling;
}


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public string summary;
    [TextArea(3, 10)]
    public string description;
    public AbilityType abilityType;
    
    public float damage = 6f;
    public List<ScalingStats> scalingStats;
    public float attackRange = 5f;
    public float cooldown = 2f;
    public float cooldownTimer = 0f;

    public bool isPiercing = false;
    public int numberOfPierces = 0;
    public bool isStutter = false;
    public float stutterDuration = 0f;
    public bool isSplitOnCrit = false;
    public int numberOfSplits = 0;
    public bool appliesOnHit = false;
    
    public GameObject projectilePrefab;
    
    public int id;
    public int abilitySlot = 0;

    public bool IsOrbiter()
    {
        return abilityType == AbilityType.Orbiter;
    }

    public bool IsAoE()
    {
        return abilityType == AbilityType.AoE;
    }

    public bool IsLobbed()
    {
        return abilityType == AbilityType.Lobbed;
    }

    public bool IsProjectile()
    {
        return abilityType == AbilityType.Projectile;
    }

    public bool IsBasicAttack()
    {
        return abilityType == AbilityType.BasicAttack;
    }

    public float GetDamage()
    {
        float bonusDamage = 0f;
        foreach(ScalingStats statPair in scalingStats)
        {
            switch(statPair.scalingStat)
            {
                case ScalingStat.PhysicalPower:
                    bonusDamage += PlayerItems.Instance.GetPhysicalPower() * statPair.scaling;
                    break;
                case ScalingStat.MagicPower:
                    bonusDamage += PlayerItems.Instance.GetMagicPower() * statPair.scaling;
                    break;
                case ScalingStat.MaxHealth:
                    bonusDamage += PlayerItems.Instance.GetMaxHealth() * statPair.scaling;
                    break;
            }
        }
        return damage + bonusDamage;
    }
}
