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
    private AbilitySO basicAttack;

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

    #region Abilities
    private void Activate(AbilitySO ability)
    {
        switch (ability.abilityType)
        {
            case AbilityType.Projectile:
                Shoot(ability);
                break;
            case AbilityType.AoE:
                AoE(ability);
                break;
            case AbilityType.Orbiter:
                break;
            case AbilityType.AttackSpeed:
                break;
            case AbilityType.Lobbed:
                Lob(ability);
                break;
        }

    }

    private void AoE(AbilitySO ability)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ability.attackRange);
        ability.projectilePrefab.GetComponent<Animator>().SetTrigger("Activate");
        
        float damage = ability.GetDamage();

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Mob"))
            {
                Mob mob = collider.GetComponent<Mob>();
                if(mob != null)
                    mob.TakeDamage(damage);
            }
        }
        ability.cooldownTimer = ability.cooldown;
        AbilityHUDManager.Instance.SetAbilitySlotCooldown(ability.abilitySlot, ability.cooldown);
    }

    private void Lob(AbilitySO ability)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ability.attackRange);
        Collider2D furthestMob = null;
        float maxDistance = 0;
    
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Mob"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    furthestMob = collider;
                }
            }
        }
    
        if (furthestMob != null)
        {
            // Spawn the projectile at the position of the furthest enemy
            var lobPrefab = Instantiate(ability.projectilePrefab, furthestMob.transform.position, Quaternion.identity);
            lobPrefab.transform.localScale = new Vector3(ability.explosionRadius, ability.explosionRadius, 1f);
            Projectile lobProjectile = lobPrefab.GetComponent<Projectile>();
            lobProjectile.SetAbilitySO(ability);
            lobProjectile.SetLifetime();
            ability.cooldownTimer = ability.cooldown;
            AbilityHUDManager.Instance.SetAbilitySlotCooldown(ability.abilitySlot, ability.cooldown);
        }
    }

    

    private void Shoot(AbilitySO ability)
    {
        if (DetectMobs(ability.attackRange, out Collider2D closestMob))
        {
            PerformAbility(ability, closestMob.transform.position);
            
            if(ability.IsType(AbilityType.BasicAttack))
            {
                float cooldownReduction = PlayerItems.Instance.GetAttackSpeed() / 100f;
                cooldownReduction = Mathf.Min(cooldownReduction, 0.65f); // Ensure the reduction does not exceed 65%
                float cooldown = ability.cooldown * (1 - cooldownReduction);
                ability.cooldownTimer = cooldown;
                HUDManager.Instance.SetBasicAttackIconCooldown(cooldown);
            }
            else
            {
                ability.cooldownTimer = ability.cooldown;
                AbilityHUDManager.Instance.SetAbilitySlotCooldown(ability.abilitySlot, ability.cooldown);
            }
        }
    }

    public float GetAttackSpeed()
    {
        float attackSpeed = 0;
        foreach (AbilitySO ability in abilities)
        {
            if (ability.IsType(AbilityType.AttackSpeed))
            {
                attackSpeed += ability.damage;
            }
        }
        return attackSpeed;
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

        projectileScript.SetAbilitySO(ability);

        projectileScript.direction = direction;
        projectileScript.SetLifetime();
    }

    public void AddAbility(AbilitySO ability)
    {
        abilities.Add(ability);
        ability.id = Random.Range(0, int.MaxValue);

        switch (ability.abilityType)
        {
            case AbilityType.AoE:
                GameObject AoE_Effect = Instantiate(_AoE_Effect, transform.position, Quaternion.identity);
                AoE_Effect.transform.SetParent(transform);
                AoE_Effect.transform.localScale = new Vector3(ability.attackRange *2, ability.attackRange *2, 1f);
                ability.projectilePrefab = AoE_Effect;
                break;
            case AbilityType.Orbiter:
                GameObject orbiterGO = Instantiate(ability.projectilePrefab, transform.position + new Vector3(ability.attackRange, ability.attackRange, 0f), Quaternion.identity);
                
                orbiterGO.transform.SetParent(transform);
                orbiterGO.transform.localScale = Vector3.one;

                Orbiter orbiter = orbiterGO.GetComponent<Orbiter>();
                orbiter.target = transform;
                orbiter.SetAbilitySO(ability);
                ability.projectilePrefab = orbiterGO;

                Projectile projectileScript = orbiterGO.GetComponent<Projectile>();
                projectileScript.SetAbilitySO(ability);
                break;
            case AbilityType.AttackSpeed:
                break;
            default:
                break;
        }

    }

    public void UpdateAoeEffectSize(int id)
    {
        foreach(AbilitySO ability in abilities)
        {
            if(ability.id == id)
            {
                GameObject AoE_Effect = ability.projectilePrefab;
                AoE_Effect.transform.localScale = new Vector3(ability.attackRange * 2, ability.attackRange * 2, 1f);
            }
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
