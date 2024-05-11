using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ExperienceBar experienceBar;

    private int xpToLevel = 6;

    // Health
    [SerializeField]
    private float _StartingMaxHealth = 100;
    private float _currentHealth = 100;
    private float _maxHealth = 100;
    private float _StartingHealthRegen = 0.1f;
    private float _healthRegen = 0.1f;
    private int level = 1;

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

    public void RecalculateHealthBonuses()
    {
        _maxHealth = _StartingMaxHealth + PlayerItems.Instance.GetHealthBonus();
        _healthRegen = _StartingHealthRegen + PlayerItems.Instance.GetHealthRegenBonus();
        HUDManager.Instance.SetHealthRegenText(_healthRegen);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    // XP
    public delegate void XPChangedDelegate(int newXP);
    public event XPChangedDelegate OnXPChanged;

    private int _currentXP = 0;
    public int currentXP
    {
        get { return _currentXP; }
        set
        {
            _currentXP = value;
            OnXPChanged?.Invoke(_currentXP);
        }
    }

    public void GainExperience(int experience)
    {
        currentXP += experience;
        if(currentXP >= xpToLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        HUDManager.Instance.SetLevelText(level);

        currentXP = 0;
        xpToLevel = (int)(xpToLevel * 1.2f);
        experienceBar.maxExperience = xpToLevel;
        AbilityUpgradeManager.Instance.LevelUp();
    }

    void Start()
    {
        _maxHealth = _StartingMaxHealth;
        experienceBar.maxExperience = xpToLevel;
        InvokeRepeating(nameof(RegenerateHealth), 1f, 1f);
    }

    private void RegenerateHealth()
    {
        if(GameStateManager.Instance.IsPaused())
        {
            return;
        }
        currentHealth = Mathf.Min(_currentHealth + _healthRegen, _maxHealth);
    }

    void Update()
    {
        CheckForPickupableObjects();
    }

    private float pickupRadius = 2f;

    private void CheckForPickupableObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("PickupableObject"))
            {
                PickupableObject pickupableObject = collider.GetComponent<PickupableObject>();
                if (pickupableObject != null)
                {
                    pickupableObject.SetMoveToPlayer(true);
                }
            }
        }
    }
}
