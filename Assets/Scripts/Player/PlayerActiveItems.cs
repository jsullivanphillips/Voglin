using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItems : MonoBehaviour
{
    [SerializeField] private GameObject _ProjectilePrefab;

    [SerializeField]
    private float cooldown = 2f;
    private float cooldownTimer = 0;

    private float attackRange = 5f;

    void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(DetectMobs(out Collider2D closestMob))
        {
            PerformAttack(closestMob.transform.position);
        }
        cooldownTimer = cooldown;
    }

    private void PerformAttack(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);

        // Set a fixed offset distance for the projectile spawn position
        float offsetDistance = 1f;
        Vector3 spawnPosition = transform.position + direction * offsetDistance;

        GameObject projectile = Instantiate(_ProjectilePrefab, spawnPosition, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.distanceToLive = attackRange + 1;
        projectileScript.SetLifetime();
        
        projectileScript.direction = direction;

    }


    private bool DetectMobs(out Collider2D closestMob)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange + 1);
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
