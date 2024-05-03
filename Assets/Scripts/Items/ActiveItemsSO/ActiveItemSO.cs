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
    public FloatRange cooldownRange = new FloatRange { min = 1f, max = 3f };
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public float attackRange = 5f;
    public float damage = 6f;
    public GameObject projectilePrefab;
}
        
