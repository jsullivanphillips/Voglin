using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _SpawnForeshadowPrefab;

    [SerializeField]
    [Range(1f, 5f)]
    private float spawnCooldown = 5f;
    private float currentCooldown = 0f;

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        if (currentCooldown <= 0f)
        {
            SpawnMob();
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void ReduceSpawnCooldown()
    {
        if (spawnCooldown > 1f)
        {
            spawnCooldown -= 0.25f;
        }
    }

    private void SpawnMob()
    {
        Vector3 spawnLocation = GetRandomSpawnPosition();
        Instantiate(_SpawnForeshadowPrefab, spawnLocation, Quaternion.identity);
        MobSO mobToSpawn = GetMobSOForWave(0);
        _SpawnForeshadowPrefab.GetComponent<SpawnForeshadow>()._MobPrefab = mobToSpawn.mobPrefab;
        _SpawnForeshadowPrefab.GetComponent<SpawnForeshadow>().mobSO = mobToSpawn;
        currentCooldown = spawnCooldown;
        ReduceSpawnCooldown();
    }

    private MobSO GetMobSOForWave(int wave)
    {
        var result = EnemyDatabase.Instance.GetMobByName("Slime");
        if(result != null)
        {
            return result;
        }
        else
        {
            Debug.LogError("Could not find mobSO for wave " + wave);
            return null;
        }
        
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-16f, 16f), Random.Range(-12f, 12f), 1f);
    }

}
