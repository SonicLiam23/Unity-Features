using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueComponent dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogue.ShowDialogue("player_dialogue_test");
    }
}
