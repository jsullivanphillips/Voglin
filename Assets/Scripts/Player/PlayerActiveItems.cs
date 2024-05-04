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
        foreach (int id in activeItems.Keys)
        {
            if(GameStateManager.Instance.IsPaused())
            {
                return;
            }
            if(activeItems[id].cooldownTimer <= 0f)
            {
                Shoot(activeItems[id]);
            }
            else
            {
                activeItems[id].cooldownTimer -= Time.deltaTime;
                HUDActiveItemZone.Instance.UpdateCooldown(id, activeItems[id].cooldownTimer / activeItems[id].cooldown);
                CraftingTableItemManager.Instance.UpdateCooldown(id, activeItems[id].cooldownTimer / activeItems[id].cooldown);
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

    public int GetActiveItemsCount()
    {
        return activeItems.Count;
    }

    private void Shoot(ActiveItemSO item)
    {
        if (DetectMobs(item.attackRange + PlayerPassiveItems.Instance.GetRangeBonus(), out Collider2D closestMob))
        {
            PerformAttack(item, closestMob.transform.position);
            item.cooldownTimer = item.cooldown * (1f - PlayerPassiveItems.Instance.GetCooldownReductionBonus());
        }
        
    }

    private Quaternion GetProjectileRotation(ActiveItemSO item, Vector3 direction)
    {
        Quaternion rotation;
        switch (item.itemName)
        {
            case "Hunting Bow":
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 226);
                break;
            case "Training Sword":
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 112);
                break;
            case "Crossbow":
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 226);
                break;
            case "Iron Mace":
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 112);
                break;
            case "Elven Bow":
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 226);
                break;
            case "Elven Sword":
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 112);
                break;
            default:
                rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
                break;
        }
        return rotation;
    }

    private void PerformAttack(ActiveItemSO item, Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion rotation = GetProjectileRotation(item, direction);
        
    
        // Set a fixed offset distance for the projectile spawn position
        float offsetDistance = 1f;
        Vector3 spawnPosition = transform.position + direction * offsetDistance;

        GameObject projectile = Instantiate(item.projectilePrefab, spawnPosition, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.distanceToLive = item.attackRange + PlayerPassiveItems.Instance.GetRangeBonus();

        // Damage
        float damage = item.damage + PlayerPassiveItems.Instance.GetDamageBonuses();
        float critChance = PlayerPassiveItems.Instance.GetCritChance() + GetActiveItemsCriticalChanceBonus();
        if(Random.Range(0f, 1f) < critChance)
        {
            damage *= 2f + GetActiveCriticalStrikeDamageBonus() + PlayerPassiveItems.Instance.GetCriticalStrikeDamageBonus();
            projectileScript.isCrit = true;
        }
        projectileScript.damage = damage;

        // Stutter
        projectileScript.isStutter = item.isStutter;
        projectileScript.stutterDuration = item.stutterDuration;

        // Piercing
        projectileScript.isPiercing = item.isPiercing;
        projectileScript.numberOfPierces = item.numberOfPierces;

        // Split on crit
        projectileScript.isSplitOnCrit = item.isSplitOnCrit;
        projectileScript.numberOfSplits = item.numberOfSplits;


        projectileScript.SetLifetime();
        
        projectileScript.direction = direction;

    }

    private float GetActiveCriticalStrikeDamageBonus()
    {
        float critDamageBonus = 0f;
        foreach (ActiveItemSO item in activeItems.Values)
        {
            foreach (PassiveEffect effect in item.passiveEffects)
            {
                if (effect == PassiveEffect.CriticalStrikeDamage)
                {
                    critDamageBonus += item.effectValues[0];
                }
            }
        }
        return critDamageBonus * 0.01f;
    }

    private float GetActiveItemsCriticalChanceBonus()
    {
        float critChanceBonus = 0f;
        foreach (ActiveItemSO item in activeItems.Values)
        {
            foreach(PassiveEffect effect in item.passiveEffects)
            {
                if(effect == PassiveEffect.CriticalChance)
                {
                    critChanceBonus += item.effectValues[0];
                }
            }
        }
        return critChanceBonus;
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
