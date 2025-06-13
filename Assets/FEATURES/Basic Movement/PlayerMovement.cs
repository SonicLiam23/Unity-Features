using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(MovementStateMachineController))]
public class PlayerMovement : MonoBehaviour
{
    MovementStateMachineController controller;
    private void Awake()
    {
        controller = GetComponent<MovementStateMachineController>();
    }

    private void Update()
    {
        // needs to be continious, not just when i move

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.velocityMult = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            controller.velocityMult = 0f;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && controller.canJump)
        {
            controller.CurrentState.Jump();
        }
    }
}
