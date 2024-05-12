using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MonoBehaviour
{
    public Transform _target;
    public Vector3 direction;
    public float speed = 5f;
    public float distanceToLive = 5f;
    private float lifetime = 2f;

    public float damage;

    private MobSO sourceMobSO;

    public void SetMobSO(MobSO mobSO)
    {
        sourceMobSO = mobSO;
        damage = mobSO.damage;
        speed = mobSO.projectileSpeed;
        SetAttackRange(mobSO.attackRange);
    }

    public void SetTarget(Transform target)
    {
        _target = target;

        ChangeDirectionToTarget();
    }

    private void ChangeDirectionToTarget()
    {
        direction = (_target.position - transform.position).normalized;
    }

    private void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }

        Move();

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void SetAttackRange(float attackRange)
    {
        lifetime = (attackRange / speed) + 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
             
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
