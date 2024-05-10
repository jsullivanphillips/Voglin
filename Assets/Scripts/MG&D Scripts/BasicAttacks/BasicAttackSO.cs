using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Attack", menuName = "Basic Attack/Basic Attack")]
public class BasicAttackSO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    [TextArea(3, 10)]
    public string description;
    public float damage = 6f;
    public float attackRange = 5f;
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public ScalingStat scalingStat;
    public float scaling = 1f;
    public GameObject projectilePrefab;
}
