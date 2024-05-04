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
