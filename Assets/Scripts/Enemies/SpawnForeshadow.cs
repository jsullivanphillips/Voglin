using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnForeshadow : MonoBehaviour
{
    private float lifetime = 1.5f;
    private float currentLifetime = 0f;

    public GameObject _MobPrefab;
    public MobSO mobSO;

    [SerializeField]
    private int iterationMax = 25;

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
        int i = 0;
        while (mobCollider != null)
        {
            spawnLocation += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
            mobCollider = Physics2D.OverlapCircle(spawnLocation, 2f, LayerMask.GetMask("Mob"));
            i++;
            if(i > iterationMax)
            {
                Debug.LogWarning($"Could not find a valid spawn location for mob after {iterationMax} iterations");
                Destroy(gameObject);
                return;
            }
        }

        GameObject mob = Instantiate(_MobPrefab, spawnLocation, Quaternion.identity);
        mob.GetComponent<Mob>().SetMobSO(mobSO);

        Destroy(gameObject);
    }
}
