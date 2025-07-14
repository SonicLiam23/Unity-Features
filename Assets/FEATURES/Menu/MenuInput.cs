using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  handles menu input and cycles through them based on buttons list. ButtonType must inherit from, IMenuButton
/// </summary>
/// <typeparam name="ButtonType">The type of menu button used in the menu. Must implement the <see cref="IMenuButton"/> interface.</typeparam>
public class MenuInput<ButtonType> : MonoBehaviour, PlayerInputActions.IMenuActions
    where ButtonType : IMenuButton
{
    public List<ButtonType> buttons { get; protected set; }
    int currOptionIndex;
      
    private void Awake()
    {
        currOptionIndex = 0;
    }    
    
    public virtual void BeginMenu()
    {
        InputManager.Instance.SetCurrentMenu(this);
        buttons[currOptionIndex].Focus();
    }

    public virtual void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            buttons[currOptionIndex].UnFocus();
            currOptionIndex += (int)context.ReadValue<float>();
            

            int optionCount = buttons.Count;
            if (currOptionIndex < 0)
            {
                currOptionIndex = optionCount - 1;
            }
            else if (currOptionIndex >= optionCount) 
            {
                currOptionIndex = 0;
            }

            buttons[currOptionIndex].Focus();
        }
    }

    public virtual void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            buttons[currOptionIndex]?.OnClick();
            // reset for the next option
            currOptionIndex = 0;
            BeginMenu();
        }
            
    }
}
