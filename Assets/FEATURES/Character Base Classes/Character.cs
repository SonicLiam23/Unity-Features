using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementStateMachineController))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(HealthComponent))]
public class Character : MonoBehaviour
{
    protected MovementStateMachineController movementController;
    protected Rigidbody2D rb;
    protected DialogueComponent dialogueComponent;
    protected void Awake()
    {
        movementController = GetComponent<MovementStateMachineController>();
        rb = GetComponent<Rigidbody2D>();
        dialogueComponent = GetComponentInChildren<DialogueComponent>();

        if (dialogueComponent == null)
            Debug.LogWarning($"DialogueComponent not found in {gameObject.name}. Dialogue on this object will not be shown.\nIf you did mean to add it, ensure it is attatched to a child of {gameObject.name}.");
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
            StartCoroutine(movementController.DisableGroundCheck());
            movementController.CurrentState.Jump();
        }
    }


}
