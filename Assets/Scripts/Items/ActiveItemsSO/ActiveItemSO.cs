using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Item", menuName = "Items/Active Item")]
public class ActiveItemSO : ItemSO
{
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public float attackRange = 5f;
    public float damage = 6f;
    public GameObject projectilePrefab;
}
        
