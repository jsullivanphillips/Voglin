using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItems : MonoBehaviour
{
    public static PlayerActiveItems Instance { get; private set; }

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

    Dictionary<int, ActiveItemSO> activeItems = new Dictionary<int, ActiveItemSO>();

    void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        foreach (ActiveItemSO item in activeItems.Values)
        {
            if(GameStateManager.Instance.IsPaused())
            {
                return;
            }
            if(item.cooldownTimer <= 0f)
            {
                Shoot(item);
            }
            else
            {
                item.cooldownTimer -= Time.deltaTime;
                HUDActiveItemZone.Instance.UpdateCooldown(item.id, item.cooldownTimer / item.cooldown);
                CraftingTableItemManager.Instance.UpdateCooldown(item.id, item.cooldownTimer / item.cooldown);
            }
        }
    }

    public void AddActiveItem(ActiveItemSO item, int id)
    {
        activeItems[id] = item;
        item.id = id;
    }

    public void RemoveActiveItem(int id)
    {
        activeItems.Remove(id);
    }

    private void Shoot(ActiveItemSO item)
    {
        if (DetectMobs(item.attackRange, out Collider2D closestMob))
        {
            PerformAttack(item, closestMob.transform.position);
            item.cooldownTimer = item.cooldown;
        }
        
    }

    private void PerformAttack(ActiveItemSO item, Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);

        // Set a fixed offset distance for the projectile spawn position
        float offsetDistance = 1f;
        Vector3 spawnPosition = transform.position + direction * offsetDistance;

        GameObject projectile = Instantiate(item.projectilePrefab, spawnPosition, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.distanceToLive = item.attackRange;
        projectileScript.damage = item.damage;
        projectileScript.SetLifetime();
        
        projectileScript.direction = direction;

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
