using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnForeshadow : MonoBehaviour
{
    private float lifetime = 1.5f;
    private float currentLifetime = 0f;

    public GameObject _MobPrefab;
    public MobSO mobSO;

    private int numberOfIterations = 15;

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime)
        {
            SpawnMob();
        }
    }

    private void SpawnMob()
    {
        if(_MobPrefab == null)
        {
            Debug.LogError("Mob Prefab not set in SpawnForeshadow");
            return;
        }
    
        Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, 0f);
        Collider2D mobCollider = Physics2D.OverlapCircle(spawnLocation, 2f, LayerMask.GetMask("Mob"));
    
        if (mobCollider == null)
        {
            // The spawnLocation is unoccupied, so spawn the mob here
            GameObject mob = Instantiate(_MobPrefab, spawnLocation, Quaternion.identity);
            mob.GetComponent<Mob>().SetMobSO(mobSO);
    
            Destroy(gameObject);
            return;
        }
    
        int distance = 1;
        while (distance <= numberOfIterations)
        {
            // Check the four directions: left, right, up, down
            Vector3[] directions = new Vector3[]
            {
                new Vector3(-distance, 0, 0), // Left
                new Vector3(distance, 0, 0),  // Right
                new Vector3(0, distance, 0),  // Up
                new Vector3(0, -distance, 0)  // Down
            };
    
            foreach (Vector3 direction in directions)
            {
                Vector3 checkPosition = spawnLocation + direction;
                mobCollider = Physics2D.OverlapCircle(checkPosition, 2f, LayerMask.GetMask("Mob"));
                if (mobCollider == null)
                {
                    // The checkPosition is unoccupied, so spawn the mob here
                    GameObject mob = Instantiate(_MobPrefab, checkPosition, Quaternion.identity);
                    mob.GetComponent<Mob>().SetMobSO(mobSO);
    
                    Destroy(gameObject);
                    return;
                }
            }
    
            // Increase the distance for the next iteration
            distance++;
        }
    
        // If we reach this point, all cells are occupied
        Debug.LogWarning("Could not find a valid spawn location for mob");
        Destroy(gameObject);
    }
}
