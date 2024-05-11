using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    [SerializeField] PlayerMovement _PlayerMovement;

    private void Start()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        if (GameStateManager.Instance.IsPaused())
        {
            return;
        }
        MovementInput();
    }

    private void MovementInput()
    {
        float inputHorizontal = 0;
        float inputVertical = 0;

        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (up ^ down) // XOR operator, true if either up or down is true, but not both
        {
            inputVertical = up ? 1 : -1;
        }

        if (left ^ right) // XOR operator, true if either left or right is true, but not both
        {
            inputHorizontal = right ? 1 : -1;
        }

        Vector2 inputVector = new Vector2(inputHorizontal, inputVertical);  

        _PlayerMovement.MovePlayer(inputVector);
    }
}
