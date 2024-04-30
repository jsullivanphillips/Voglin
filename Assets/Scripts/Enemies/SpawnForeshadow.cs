using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnForeshadow : MonoBehaviour
{
    private float lifetime = 2.5f;
    private float currentLifetime = 0f;

    public GameObject _MobPrefab;

    private void Update()
    {
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
        Instantiate(_MobPrefab, spawnLocation, Quaternion.identity);
        Destroy(gameObject);
    }
}
