using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]  
public class DialogueTrigger : MonoBehaviour
{
    DialogueComponent dialogue;
    

    private void Awake()
    {
        dialogue = GetComponentInChildren<DialogueComponent>();
        if (dialogue == null)
        {
            Debug.LogWarning($"DialogueComponent not found in {gameObject.name}. Dialogue on this object will not be shown.\nIf you did mean to add it, ensure it is attatched to a child of {gameObject.name}.");
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            dialogue.ShowDialogue();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            dialogue.HideDialogue();
    }
}
