using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    [SerializeField] BoxCollider2D _BoxCollider2D;
    [Tooltip("The layers that the player should collide with.")]
    [SerializeField] LayerMask _CollisionLayerMask;

    public void MovePlayer(Vector2 inputVector)
    {
        #region Preparing input
        if (inputVector.magnitude > 1)
        {
            inputVector = inputVector.normalized;
        }

        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

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
