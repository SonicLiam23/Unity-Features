using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A player character, containing unique movement that takes player input
/// </summary>
[RequireComponent(typeof(PlayerHealth))]
public class Player : Character, PlayerInputActions.IPlayerActions
{

    protected override void Awake()
    {
        base.Awake();

        RigidBodyComp.constraints = RigidbodyConstraints2D.FreezeRotation;

        InputManager.InputActions.Player.SetCallbacks(this);
        InputManager.InputActions.Player.Enable(); // Enables the player action map
    }
    public void OnMove(InputAction.CallbackContext context)
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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && MovementController.canJump)
        {
            MovementController.CurrentState.Jump();
        }
    }
}
