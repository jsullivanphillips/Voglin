using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PassiveEffect
{
    DamageBoost,
    CooldownReduction,
    RangeBoost,
}
[CreateAssetMenu(fileName = "New Passive Item", menuName = "Items/Passive Item")]
public class PassiveItemSO : ScriptableObject
{
    public string itemName = "New Passive Item";
    public int id;
    public List<PassiveEffect> passiveEffects;
    public List<float> effectValues;
}
        
