using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInputActions InputActions { get; private set; }

    private void Awake()
    {
        // if this already exists, destroy it.
        if (InputActions != null)
        {
            Destroy(gameObject);
            return;
        }

        InputActions = new PlayerInputActions();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }
}
