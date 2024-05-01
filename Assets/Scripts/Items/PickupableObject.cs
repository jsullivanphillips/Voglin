using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    private float speed = 2f;
    private Transform playerTransform;
    private bool isMovingToPlayer;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(isMovingToPlayer)
        {
            MoveToPlayer();
        }
    }

    public void MoveToPlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();

        float acceleration = 10f; // Adjust the acceleration value as needed
        speed += acceleration * Time.deltaTime;

        Vector2 movement = direction * speed * Time.deltaTime;
        
        transform.position += new Vector3(movement.x, movement.y, 0f);
    }

    public void SetMoveToPlayer(bool value)
    {
        isMovingToPlayer = value;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Base pickup");
            Destroy(gameObject);
        }
    }
}
