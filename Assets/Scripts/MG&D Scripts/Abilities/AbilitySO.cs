using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    Projectile,
    AoE,
    Orbiter
}
public enum ScalingStat
{
    PhysicalPower,
    MagicPower
}
[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public float damage = 6f;
    [TextArea(3, 10)]
    public string description;
    public ScalingStat scalingStat;
    public float scaling = 1f;
    public int abilitySlot = 0;
    public float attackRange = 5f;
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public GameObject projectilePrefab;
    public AbilityType abilityType;
    public int id;
}
