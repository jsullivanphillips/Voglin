using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public float cooldown = 2f;
    public float cooldownTimer = 0f;
    public float attackRange = 5f;
    public float damage = 6f;
    public GameObject projectilePrefab;

    //public ActiveItemSO activeItemSO;

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }

        if (cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
