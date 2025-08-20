using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]  
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject dialogueObject;

    DialogueComponent Dialogue => input.dialogue;
    DialogueInput input;
    

    private void Awake()
    {
        
        if (dialogueObject == null)
        {
            this.enabled = false;
            return;
        }

        input = dialogueObject.GetComponent<DialogueInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Dialogue.ShowDialogue();
            InputManager.Instance.InputActions.Menu.Enable();
            input.BeginMenu();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Dialogue.HideDialogue();
            InputManager.Instance.InputActions.Menu.Disable();
        }
            
    }
}
