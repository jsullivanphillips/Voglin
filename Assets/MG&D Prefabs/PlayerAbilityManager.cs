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
    }

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
