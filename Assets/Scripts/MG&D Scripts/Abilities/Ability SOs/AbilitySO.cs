using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    Projectile,
    AoE,
    Orbiter,
    Lobbed,
    BasicAttack,
    AttackSpeed
}

[System.Serializable]
public class ScalingStats
{
    public Stat scalingStat;
    public float scaling;
}


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public AbilitySO upgrade;
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
    public float lobDelayTime = 0f;
    public float explosionRadius = 0f;
    
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    
    public int id;
    public int abilitySlot = 0;

    public bool IsType(AbilityType type)
    {
        return abilityType == type;
    }

    public float GetDamage()
    {
        float bonusDamage = 0f;
        foreach(ScalingStats statPair in scalingStats)
        {
            bonusDamage += PlayerItems.Instance.GetScalingStat(statPair.scalingStat) * statPair.scaling;
        }
        return damage + bonusDamage;
    }


    public void UpgradeAbility(AbilitySO upgradedAbilitySO)
    {
        name = upgradedAbilitySO.name;
        icon = upgradedAbilitySO.icon;
        summary = upgradedAbilitySO.summary;
        description = upgradedAbilitySO.description;
        abilityType = upgradedAbilitySO.abilityType;
        upgrade = upgradedAbilitySO.upgrade;
        damage = upgradedAbilitySO.damage;
        scalingStats = upgradedAbilitySO.scalingStats;
        attackRange = upgradedAbilitySO.attackRange;
        cooldown = upgradedAbilitySO.cooldown;
        cooldownTimer = upgradedAbilitySO.cooldownTimer;
        isPiercing = upgradedAbilitySO.isPiercing;
        numberOfPierces = upgradedAbilitySO.numberOfPierces;
        isStutter = upgradedAbilitySO.isStutter;
        stutterDuration = upgradedAbilitySO.stutterDuration;
        isSplitOnCrit = upgradedAbilitySO.isSplitOnCrit;
        numberOfSplits = upgradedAbilitySO.numberOfSplits;
        appliesOnHit = upgradedAbilitySO.appliesOnHit;
        lobDelayTime = upgradedAbilitySO.lobDelayTime;
        explosionRadius = upgradedAbilitySO.explosionRadius;

        projectileSpeed = upgradedAbilitySO.projectileSpeed;



    }
}
