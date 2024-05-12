using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MobType
{
    Basic,
    Elite,
    Boss
}
public enum AttackStyle
{
    Melee,
    Ranged,
    Swipe,
    AoE1
}
public enum MovementStyle
{
    Direct,
    Spirals,
    ZigZag,
    KeepDistance
}
[CreateAssetMenu(fileName = "New Mob", menuName = "Enemies/New Mob")]
public class MobSO : ScriptableObject
{
    public string mobName;

    public float health;
    public float damage;

    public float orbDropChance;

    public AttackStyle attackStyle;
    public float attackRange;
    public float attackCooldown;

    public MovementStyle movementStyle;
    public float movementSpeed;

    public GameObject mobPrefab;
    public GameObject projectilePrefab;
    public float projectileSpeed;
}
       