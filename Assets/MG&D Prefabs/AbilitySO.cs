using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
    public float damage = 6f;
    public float attackRange = 5f;
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public GameObject projectilePrefab;

}
