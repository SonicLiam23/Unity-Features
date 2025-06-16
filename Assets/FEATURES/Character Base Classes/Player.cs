using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : Character
{
    PlayerInput input;

    private void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInput>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (input.actions == null)
        {
            Debug.LogWarning("Please set the player input in the Player Input component!");
        }
    }
}
