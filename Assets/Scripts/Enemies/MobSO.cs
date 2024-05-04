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
    Projectile,
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
public class ItemSO : ScriptableObject
{
    public string mobName;

    public float health;

    public AttackStyle attackStyle;
    public float attackRange;
    public float attackCooldown;

    public MovementStyle movementStyle;
    public float movementSpeed;
}
       