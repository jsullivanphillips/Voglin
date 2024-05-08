using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public static PlayerAbilityManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private List<AbilitySO> abilities = new List<AbilitySO>();
    [SerializeField]
    private BasicAttackSO basicAttack;

    [SerializeField]
    private GameObject _AoE_Effect;

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }

        // Basic attack
        if (basicAttack.cooldownTimer <= 0f)
        {
            Shoot(basicAttack);
        }
        else
        {
            basicAttack.cooldownTimer -= Time.deltaTime;
        }

        // Abilities
        foreach (AbilitySO ability in abilities)
        {
            if (ability.cooldownTimer <= 0f)
            {
                Activate(ability);
            }
            else
            {
                ability.cooldownTimer -= Time.deltaTime;
            }
        }
    }
    #region Basic Attack
    private void Shoot(BasicAttackSO basicAttack)
    {
        if (DetectMobs(basicAttack.attackRange, out Collider2D closestMob))
        {
            PerformAttack(basicAttack, closestMob.transform.position);
            basicAttack.cooldownTimer = basicAttack.cooldown;
            HUDManager.Instance.SetBasicAttackIconCooldown(basicAttack.cooldown);
        }
    }

    private void PerformAttack(BasicAttackSO basicAttack, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        // Set a fixed offset distance for the projectile spawn position
        float offsetDistance = 1f;
        Vector3 spawnPosition = transform.position + direction * offsetDistance;

        // This is calculated for the sword prefab
        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 112);

        GameObject projectile = Instantiate(basicAttack.projectilePrefab, spawnPosition, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.direction = direction;

        projectileScript.distanceToLive = basicAttack.attackRange;
        projectileScript.SetLifetime();

        projectileScript.damage = basicAttack.damage;
    }
    #endregion

    #region Abilities
    private void Activate(AbilitySO ability)
    {
        if(ability.abilityType == AbilityType.Projectile)
        {
            Shoot(ability);
        }
        else if(ability.abilityType == AbilityType.AoE)
        {
            AoE(ability);
        }
    }

    private void AoE(AbilitySO ability)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ability.attackRange);
        ability.projectilePrefab.GetComponent<Animator>().SetTrigger("Activate");
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Mob"));
            {
                Mob mob = collider.GetComponent<Mob>();
                if(mob != null)
                    mob.TakeDamage(ability.damage);
            }
        }
        ability.cooldownTimer = ability.cooldown;
        AbilityHUDManager.Instance.SetAbilitySlotCooldown(ability.abilitySlot, ability.cooldown);
    }

    private void Shoot(AbilitySO ability)
    {
        if (DetectMobs(ability.attackRange, out Collider2D closestMob))
        {
            PerformAbility(ability, closestMob.transform.position);
            ability.cooldownTimer = ability.cooldown;
            AbilityHUDManager.Instance.SetAbilitySlotCooldown(ability.abilitySlot, ability.cooldown);
        }
    }

    private void PerformAbility(AbilitySO ability, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Set a fixed offset distance for the projectile spawn position
        float offsetDistance = 1f;
        Vector3 spawnPosition = transform.position + direction * offsetDistance;

        // This is calculated for the sword prefab
        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 112);

        GameObject projectile = Instantiate(ability.projectilePrefab, spawnPosition, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.direction = direction;

        projectileScript.distanceToLive = ability.attackRange;
        projectileScript.SetLifetime();

        projectileScript.damage = ability.damage;
    }

    public void AddAbility(AbilitySO ability)
    {
        abilities.Add(ability);
        if(ability.abilityType == AbilityType.AoE)
        {
            GameObject AoE_Effect = Instantiate(_AoE_Effect, transform.position, Quaternion.identity);
            AoE_Effect.transform.SetParent(transform);
            AoE_Effect.transform.localScale = new Vector3(ability.attackRange *2, ability.attackRange *2, 1f);
            ability.projectilePrefab = AoE_Effect;
        }
    }


    public void RemoveAbility(AbilitySO ability)
    {
        abilities.Remove(ability);
    }
    #endregion
    private bool DetectMobs(float range, out Collider2D closestMob)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        closestMob = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Mob")) // Check if the collider's game object has the "Mob" tag
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestMob = collider;
                    closestDistance = distance;
                }
            }
        }

        return closestMob != null;
    }



}
