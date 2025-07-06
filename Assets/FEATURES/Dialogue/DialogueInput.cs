using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueComponent))]
public class DialogueInput : MonoBehaviour, PlayerInputActions.IDialogueActions
{
    public DialogueComponent dialogue {  get; private set; }
    int currOptionIndex;

    private void Awake()
    {
        dialogue = GetComponent<DialogueComponent>();
        InputManager.InputActions.Dialogue.SetCallbacks(this);
        
        currOptionIndex = 0;
    }    
    
    public void BeginDialogue()
    {
        Debug.Log($"Dialogue started at {currOptionIndex}.");
        dialogue.Options[currOptionIndex].Select();
    }

    public void OnNavigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dialogue.Options[currOptionIndex].Deselect();
            currOptionIndex += (int)context.ReadValue<float>();
            

            int optionCount = dialogue.Options.Count;
            if (currOptionIndex < 0)
            {
                currOptionIndex = optionCount - 1;
            }
            else if (currOptionIndex >= optionCount) 
            {
                currOptionIndex = 0;
            }

            dialogue.Options[currOptionIndex].Select();
        }
    }

    public void OnSelect(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dialogue.Options[currOptionIndex]?.OnClick();
            // reset for the next option
            currOptionIndex = 0;
        }
            
    }
}
