using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 20f;
    public float distanceToLive = 5f;
    private float lifetime = 2f;

    public bool isCrit = false;

    private AbilitySO ability;
    private int numberOfPierces = 0;

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
        transform.position += direction * speed * Time.deltaTime;

        if(ability.IsOrbiter())
            return;

        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetLifetime()
    {
        lifetime = distanceToLive / speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
            if (!ability.isPiercing && !ability.IsOrbiter())
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
