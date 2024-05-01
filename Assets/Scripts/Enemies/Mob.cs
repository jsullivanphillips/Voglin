using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    [SerializeField]
    private GameObject _ExperienceMotePrefab;
    [SerializeField]
    private GameObject _ItemDropPrefab;

    private float attackCooldown = 1f;
    private float currentCooldown = 0f;
    private float attackRange = 1.5f;

    GameObject player;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, 1f);
        Instantiate(_ExperienceMotePrefab, spawnLocation, Quaternion.Euler(0f, 0f, 90f));
        if(Random.Range(0, 100) < 50)
        {
            Vector3 offsetSpawnLocation = spawnLocation + new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0f);
            Instantiate(_ItemDropPrefab, offsetSpawnLocation, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if (currentCooldown <= 0f)
            {
                player.GetComponent<Player>().TakeDamage(5);
                currentCooldown = attackCooldown;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }
    }
}
