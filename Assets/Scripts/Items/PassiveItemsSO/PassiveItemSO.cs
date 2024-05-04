using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PassiveEffect
{
    DamageBoost,
    CriticalChance,
    MoveSpeed,
    CooldownReduction,
    RangeBoost,
    CriticalStrikeDamage
}
[CreateAssetMenu(fileName = "New Passive Item", menuName = "Items/Passive Item")]
public class PassiveItemSO : ItemSO
{
    public List<PassiveEffect> passiveEffects;
    public List<float> effectValues;
}
        
