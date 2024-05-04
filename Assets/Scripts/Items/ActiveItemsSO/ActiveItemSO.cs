using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FloatRange
{
    public float min;
    public float max;
}

[CreateAssetMenu(fileName = "New Active Item", menuName = "Items/Active Item")]
public class ActiveItemSO : ItemSO
{
    public float damage = 6f;
    public FloatRange cooldownRange = new FloatRange { min = 1f, max = 3f };
    public float attackRange = 5f;
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    [Header("Special Effects Section")]
    public bool showSpecialEffectsSection = false;
    [ConditionalHide("showSpecialEffectsSection", true)]
    public bool isPiercing = false;
    [ConditionalHide("showSpecialEffectsSection", true)]
    public int numberOfPierces = 0;
    [ConditionalHide("showSpecialEffectsSection", true)]
    public bool isStutter = false;
    [ConditionalHide("showSpecialEffectsSection", true)]
    public float stutterDuration = 0.1f;
    [ConditionalHide("showSpecialEffectsSection", true)]
    public bool isSplitOnCrit = false;
    [ConditionalHide("showSpecialEffectsSection", true)]
    public int numberOfSplits = 0;

    public List<PassiveEffect> passiveEffects;
    public List<float> effectValues;
    public GameObject projectilePrefab;
    
}
        
