using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float distanceToLive = 5f;
    private float lifetime = 2f;

    public bool isCrit = false;

    private AbilitySO ability;
    private int numberOfPierces = 0;
    [SerializeField]
    private GameObject _LobExplosion;

    public void SetAbilitySO(AbilitySO _ability)
    {
        ability = _ability;
        numberOfPierces = ability.numberOfPierces;
    }

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }

        Move();

        if(ability.IsType(AbilityType.Orbiter))
            return;

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            if(ability.IsType(AbilityType.Lobbed))
            {
                _LobExplosion.SetActive(true);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ability.explosionRadius);

                foreach (Collider2D collider in colliders)
                {
                    Mob mob = collider.GetComponent<Mob>();
                    if (mob != null)
                        mob.TakeDamage(ability.GetDamage());
                }
                lifetime = 0.3f;
                Destroy(gameObject, 0.2f);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }


    private void Move()
    {
        transform.position += direction * ability.projectileSpeed * Time.deltaTime;
    }

    public void SetLifetime()
    {
        if(ability.IsType(AbilityType.Lobbed))
        {
            lifetime = ability.lobDelayTime;
        }
        else
        {
            lifetime = ability.attackRange / ability.projectileSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ability.IsType(AbilityType.Lobbed))
            return;
             
        if (other.gameObject.tag == "Mob")
        {
            other.gameObject.GetComponent<Mob>().TakeDamage(ability.GetDamage());

            if(ability.isStutter)
            {
                other.gameObject.GetComponent<MobMovement>().Stutter(ability.stutterDuration);
            }
            if(ability.isSplitOnCrit && isCrit)
            {
                Split();
            }
            if (!ability.isPiercing && !ability.IsType(AbilityType.Orbiter))
            {
                Destroy(gameObject);
            }
            else
            {
                if(ability.isPiercing)
                {
                    numberOfPierces--;
                    if(numberOfPierces < 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }

            
        }
    }



    private void Split()
    {
        // Ensure there's at least one split to avoid division by zero
        if (ability.numberOfSplits > 1)
        {
            for (int i = 0; i < ability.numberOfSplits; i++)
            {
                float angleStep = 90f / (ability.numberOfSplits - 1); // Divide the total angle by the number of intervals
                float projectileDirection = -45f + (angleStep * i);
                Vector3 newDirection = Quaternion.Euler(0, 0, projectileDirection) * direction.normalized;
                // Calculate the correct rotation for the new direction
                Quaternion rotation = Quaternion.Euler(0, 0, projectileDirection + transform.eulerAngles.z);
                GameObject projectile = Instantiate(gameObject, transform.position, rotation);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.direction = newDirection;
                projectileScript.isCrit = false; 
                projectileScript.SetAbilitySO(Instantiate(ability)); 

                
            }
        }
        else if (ability.numberOfSplits == 1)
        {
            // For a single split, you might want to keep the original direction but still adjust the rotation
            float randomDirection = Random.Range(-45f, 45f);
            Vector3 newDirection = Quaternion.Euler(0, 0, randomDirection) * direction.normalized;
            Quaternion rotation = Quaternion.Euler(0, 0, randomDirection + transform.eulerAngles.z);
            GameObject projectile = Instantiate(gameObject, transform.position, rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.direction = newDirection;
            projectileScript.isCrit = false; 
            projectileScript.SetAbilitySO(Instantiate(ability)); 
        }
    }
}
