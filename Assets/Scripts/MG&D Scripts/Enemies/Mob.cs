using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField]
    MobMovement _MobMovement;

    string mobName;


    [SerializeField]
    private float _StartingMaxHealth = 5;
    private float _currentHealth = 5;
    private float _maxHealth = 5;

    [SerializeField]
    private GameObject _ExperienceMotePrefab;

    private AttackStyle attackStyle;
    private float attackCooldown = 1f;
    private float currentCooldown = 0f;
    private float attackRange = 1.5f;
    private float damage = 5f;

    private MobSO mobSO;

    GameObject playerGO;

    public delegate void HealthChangedDelegate(float newHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    public float currentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }


    public void TakeDamage(float damage)
    {
        DamageNumberDisplay.Instance.DisplayDamageNumber((int)damage, transform.position);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, 1f);
        Instantiate(_ExperienceMotePrefab, spawnLocation, Quaternion.Euler(0f, 0f, 90f));
        Destroy(gameObject);
    }

    public void SetMobSO(MobSO mobSO)
    {
        this.mobSO = mobSO;

        GetComponent<MobHUD>().SetMob(this);

        mobName = mobSO.mobName;

        _StartingMaxHealth = mobSO.health;
        _maxHealth = mobSO.health;
        currentHealth = _maxHealth;

        attackStyle = mobSO.attackStyle;
        attackCooldown = mobSO.attackCooldown;
        attackRange = mobSO.attackRange;
        damage = mobSO.damage;

        _MobMovement.speed = mobSO.movementSpeed;
        
        _MobMovement.movementStyle = mobSO.movementStyle;
        
    }

    private void Start()
    {
        playerGO = GameObject.Find("Player");
    }

    

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        if (playerGO != null && Vector3.Distance(transform.position, playerGO.transform.position) <= attackRange)
        {
            if (currentCooldown <= 0f)
            {
                Attack(playerGO.GetComponent<Player>());
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }
    }

    private void Attack(Player player)
    {
        switch (attackStyle)
        {
            case AttackStyle.Melee:
                player.TakeDamage(damage);
                break;
            case AttackStyle.Ranged:
                FireProjectile(player);
                break;
        }
        currentCooldown = attackCooldown;
    }

    private void FireProjectile(Player player)
    {
        _MobMovement.Stutter(0.5f);
        GameObject projectile = Instantiate(mobSO.projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<MobProjectile>().SetMobSO(mobSO);
        projectile.GetComponent<MobProjectile>().SetTarget(player.transform);
    }
}
