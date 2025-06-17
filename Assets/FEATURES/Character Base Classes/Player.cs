using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A player character, containing unique movement that takes player input
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class Player : Character
{
    PlayerInput input;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInput>();
        RigidBodyComp.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (input.actions == null)
        {
            Debug.LogWarning("Please set the player input in the Player Input component!");
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MovementController.velocityMult = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            MovementController.velocityMult = 0f;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && MovementController.canJump)
        {
            StartCoroutine(MovementController.DisableGroundCheck());
            MovementController.CurrentState.Jump();
        }
    }
}
