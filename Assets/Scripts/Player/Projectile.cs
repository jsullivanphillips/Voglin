using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 20f;
    public float distanceToLive = 5f;
    private float lifetime = 2f;
    public bool isPiercing = false;
    public int numberOfPierces = 0;
    public bool isStutter = false;
    public float stutterDuration = 0.1f;
    public float damage = 5;
    public bool isSplitOnCrit = false;
    public int numberOfSplits = 0;
    public bool isCrit = false;

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        transform.position += direction * speed * Time.deltaTime;
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
            if(isStutter)
            {
                other.gameObject.GetComponent<MobMovement>().Stutter(stutterDuration);
            }
            if(isSplitOnCrit && isCrit)
            {
                // Ensure there's at least one split to avoid division by zero
                if (numberOfSplits > 1)
                {
                    for (int i = 0; i < numberOfSplits; i++)
                    {
                        float angleStep = 90f / (numberOfSplits - 1); // Divide the total angle by the number of intervals
                        float projectileDirection = -45f + (angleStep * i);
                        Vector3 newDirection = Quaternion.Euler(0, 0, projectileDirection) * direction.normalized;
                        // Calculate the correct rotation for the new direction
                        Quaternion rotation = Quaternion.Euler(0, 0, projectileDirection + transform.eulerAngles.z);
                        GameObject projectile = Instantiate(gameObject, transform.position, rotation);
                        projectile.GetComponent<Projectile>().direction = newDirection;
                        projectile.GetComponent<Projectile>().isCrit = false; // Prevent infinite splitting
                        projectile.GetComponent<Projectile>().damage = damage / 2f;
                    }
                }
                else if (numberOfSplits == 1)
                {
                    // For a single split, you might want to keep the original direction but still adjust the rotation
                    float randomDirection = Random.Range(-45f, 45f);
                    Vector3 newDirection = Quaternion.Euler(0, 0, randomDirection) * direction.normalized;
                    Quaternion rotation = Quaternion.Euler(0, 0, randomDirection + transform.eulerAngles.z);
                    GameObject projectile = Instantiate(gameObject, transform.position, rotation);
                    projectile.GetComponent<Projectile>().direction = newDirection;
                    projectile.GetComponent<Projectile>().isCrit = false; // Prevent infinite splitting
                    projectile.GetComponent<Projectile>().damage = damage / 2f;
                }
            }
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
            else
            {
                numberOfPierces--;
                if(numberOfPierces < 0)
                {
                    Destroy(gameObject);
                }
            }

            other.gameObject.GetComponent<Mob>().TakeDamage(damage);
        }
    }
}
