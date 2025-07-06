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
    private PlayerInputActions inputActions;

    protected override void Awake()
    {
        base.Awake();

        RigidBodyComp.constraints = RigidbodyConstraints2D.FreezeRotation;

        inputActions = new PlayerInputActions();
        inputActions.Player.SetCallbacks(this);
        inputActions.Enable(); // Enables the whole action map
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
