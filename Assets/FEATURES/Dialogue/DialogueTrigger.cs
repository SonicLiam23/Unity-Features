using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]  
public class DialogueTrigger : MonoBehaviour
{
    DialogueComponent dialogue;
    DialogueInput input;
    

    private void Awake()
    {
        input = GetComponentInChildren<DialogueInput>();
        dialogue = input.dialogue;
        if (dialogue == null)
        {
            Debug.LogWarning($"DialogueComponent not found in {gameObject.name}. You must attach a DialogueInput to this or a child.");
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.ShowDialogue();
            InputManager.Instance.InputActions.Menu.Enable();
            input.BeginMenu();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.HideDialogue();
            InputManager.Instance.InputActions.Menu.Disable();
        }
            
    }
}
