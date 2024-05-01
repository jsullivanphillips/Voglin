using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        Debug.Log("Gained " + experience + " experience!");
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
