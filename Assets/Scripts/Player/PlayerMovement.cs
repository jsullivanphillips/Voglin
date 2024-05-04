using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 4f;
    [SerializeField] BoxCollider2D _BoxCollider2D;
    [Tooltip("The layers that the player should collide with.")]
    [SerializeField] LayerMask _CollisionLayerMask;

    public float speedBoostAmount = 10f; // The amount of speed to add
    public float speedBoostDuration = 2f; // How long the speed boost lasts
    private float speedBoostTimer = 0f; // A timer to track the boost duration
    private bool isSpeedBoosted = false; // To check if speed boost is active

    public void ActivateSpeedBoost()
    {
        isSpeedBoosted = true;
        speedBoostTimer = speedBoostDuration; // Reset the timer to the duration of the boost
    }

    void Update()
    {
        if (isSpeedBoosted)
        {
            // Decrease the timer
            speedBoostTimer -= Time.deltaTime;

            // Check if the speed boost duration has ended
            if (speedBoostTimer <= 0)
            {
                isSpeedBoosted = false;
                speedBoostTimer = 0;
            }
        }
    }


    public void MovePlayer(Vector2 inputVector)
    {
        #region Preparing input
        if (inputVector.magnitude > 1)
        {
            inputVector = inputVector.normalized;
        }

        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        // Calculate the current speed, including any active speed boost
        float currentSpeed = speed + PlayerPassiveItems.Instance.GetMoveSpeedBonuses();
        if (isSpeedBoosted)
        {
            currentSpeed += speedBoostAmount * (speedBoostTimer / speedBoostDuration); // Apply decaying boost
        }

        Vector2 horizontalMovement = new Vector2(inputVector.x, 0) * (speed + PlayerPassiveItems.Instance.GetMoveSpeedBonuses()) * Time.deltaTime;
        Vector2 verticalMovement = new Vector2(0, inputVector.y) * (speed + PlayerPassiveItems.Instance.GetMoveSpeedBonuses()) * Time.deltaTime;
        #endregion

        #region Collision filters
        // Create a ContactFilter2D to ignore the player's own collider
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_CollisionLayerMask);
        #endregion

        #region Horizontal collision detection & movement
        // Check if the new horizontal position would be inside a collider
        List<Collider2D> collisions = new List<Collider2D>();
        Physics2D.OverlapBox(currentPosition + horizontalMovement, _BoxCollider2D.size, 0f, contactFilter, collisions);
       

        // If the new horizontal position is not inside a collider, move the player horizontally
        if (collisions.Count == 0)
        {
            currentPosition += horizontalMovement;
        }
        #endregion

        // Clear the collisions list
        collisions.Clear();

        #region Vertical collision detection & movement
        // Check if the new vertical position would be inside a collider
        Physics2D.OverlapBox(currentPosition + verticalMovement, _BoxCollider2D.size, 0f, contactFilter, collisions);

        // If the new vertical position is not inside a collider, move the player vertically
        if (collisions.Count == 0)
        {
            currentPosition += verticalMovement;
        }
        #endregion

        transform.position = new Vector3(currentPosition.x, currentPosition.y, 0f);
    }


    
}
