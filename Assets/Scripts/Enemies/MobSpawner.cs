using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _MobPrefab;
    [SerializeField]
    private GameObject _SpawnForeshadowPrefab;

    private float spawnCooldown = 5f;
    private float currentCooldown = 0f;

    private void Update()
    {
        if (currentCooldown <= 0f)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-16f, 16f), Random.Range(-12f, 12f), 1f);
            Instantiate(_SpawnForeshadowPrefab, spawnLocation, Quaternion.identity);
            _SpawnForeshadowPrefab.GetComponent<SpawnForeshadow>()._MobPrefab = _MobPrefab;
            currentCooldown = spawnCooldown;
            ReduceSpawnCooldown();
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

}
