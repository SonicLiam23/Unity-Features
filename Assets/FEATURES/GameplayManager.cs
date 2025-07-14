using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour, PlayerInputActions.IGlobalActions
{
    static public GameplayManager Instance { get; private set; }

    [SerializeField] GameObject pauseMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        InputManager.Instance.InputActions.Global.SetCallbacks(Instance);
        InputManager.Instance.InputActions.Global.Enable(); // Enables the action map
    }

    public void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Instance.TogglePause();
        }
    }

    public void TogglePause()
    {
        bool isPaused = !pauseMenu.activeSelf;
        pauseMenu.SetActive(isPaused);

        if (isPaused) InputManager.Instance.InputActions.Player.Disable();
        else InputManager.Instance.InputActions.Player.Enable();

        float pauseTime = isPaused ? 0f : 1f;
        Time.timeScale = pauseTime; 
    }
}
