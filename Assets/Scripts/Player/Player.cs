using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ExperienceBar experienceBar;

    private int xpToLevel = 4;

    // Health
    [SerializeField]
    private float _StartingMaxHealth = 100;
    private float _currentHealth = 100;
    private float _maxHealth = 100;

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
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
        currentXP = 0;
        xpToLevel = (int)(xpToLevel * 1.2f);
        experienceBar.maxExperience = xpToLevel;
        AbilityUpgradeManager.Instance.LevelUp();
    }

    void Start()
    {
        _maxHealth = _StartingMaxHealth;
        experienceBar.maxExperience = xpToLevel;
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
