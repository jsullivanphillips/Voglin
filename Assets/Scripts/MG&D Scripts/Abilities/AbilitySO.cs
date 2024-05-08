using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    Projectile,
    AoE
}
[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite icon;
    public float damage = 6f;
    public int abilitySlot = 0;
    public float attackRange = 5f;
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public GameObject projectilePrefab;
    public AbilityType abilityType;
}
