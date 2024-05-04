using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    float speed = 2f;
    Transform playerTransform;
    float stutterTimer = 0f;

    [SerializeField] BoxCollider2D _BoxCollider2D;
    [Tooltip("The layers that the player should collide with.")]
    [SerializeField] LayerMask _CollisionLayerMask;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }

        // If the stutter timer is greater than 0, reduce it by the time since the last frame and return
        if (stutterTimer > 0f)
        {
            stutterTimer -= Time.deltaTime;
            return;
        }

        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(playerTransform.position.x - currentPosition.x, playerTransform.position.y - currentPosition.y);
        direction.Normalize();

        Vector2 horizontalMovement = new Vector2(direction.x, 0) * speed * Time.deltaTime;
        Vector2 verticalMovement = new Vector2(0, direction.y) * speed * Time.deltaTime;

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_CollisionLayerMask);

        List<Collider2D> collisions = new List<Collider2D>();
        Physics2D.OverlapBox(currentPosition + horizontalMovement, _BoxCollider2D.size, 0f, contactFilter, collisions);
        
        if(collisions.Count - 1  == 0)
        {
            currentPosition += horizontalMovement;
        }

        collisions.Clear();

        Physics2D.OverlapBox(currentPosition + verticalMovement, _BoxCollider2D.size, 0f, contactFilter, collisions);

        if(collisions.Count - 1 == 0)
        {
            currentPosition += verticalMovement;
        }

        transform.position = new Vector3(currentPosition.x, currentPosition.y, 0f);
    }

    public void Stutter(float stutterDuration)
    {
        stutterTimer += stutterDuration;
    }
}