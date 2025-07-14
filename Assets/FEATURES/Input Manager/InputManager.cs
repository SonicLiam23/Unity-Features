using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public PlayerInputActions InputActions { get; private set; }
    private PlayerInputActions.IMenuActions currentMenuHandler;

    private void Awake()
    {
        // if this already exists, destroy it.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InputActions = new PlayerInputActions();
        InputActions.Menu.Navigate.started += ctx => currentMenuHandler?.OnNavigate(ctx);
        InputActions.Menu.Select.started += ctx => currentMenuHandler?.OnSelect(ctx);
    }

    public void SetCurrentMenu(PlayerInputActions.IMenuActions newHandler)
    {
        Instance.currentMenuHandler = newHandler;
        Instance.InputActions.Menu.Enable();
    }

    public static void RemoveCurrentMenu()
    {

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
