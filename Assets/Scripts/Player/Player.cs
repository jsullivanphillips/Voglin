using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    ExperienceBar experienceBar;

    private int xpToLevel = 10;

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

    public delegate void HealthChangedDelegate(float newHealth);
    public event HealthChangedDelegate OnHealthChanged;

    private float _currentHealth = 100;
    public float currentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
        ChooseNewCardManager.Instance.DisplayCardsForLevel(1);
    }

    void Start()
    {
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
