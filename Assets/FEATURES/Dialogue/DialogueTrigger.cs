using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Can safely leave blank if Entity is attached, it will find it automatically")]
    [SerializeField] DialogueComponent dialogue;
    

    private void Awake()
    {
        if (dialogue == null)
        {
            dialogue = GetComponent<Entity>().DialogueComp;
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
