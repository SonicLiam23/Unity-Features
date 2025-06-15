using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStateMachineController))]
[RequireComponent(typeof(Collider2D))] 
public class Character : MonoBehaviour
{
    protected MovementStateMachineController movementController;
    protected Rigidbody2D rb;
    protected void Awake()
    {
        movementController = GetComponent<MovementStateMachineController>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementController.velocityMult = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            movementController.velocityMult = 0f;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && movementController.canJump)
        {
            movementController.CurrentState.Jump();
        }
    }
}
