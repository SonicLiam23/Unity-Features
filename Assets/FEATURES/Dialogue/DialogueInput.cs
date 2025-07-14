using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueComponent))]
public class DialogueInput : MenuInput<DialogueOptionButton>
{
    public DialogueComponent dialogue {  get; private set; }

    private void Awake()
    {
        dialogue = GetComponent<DialogueComponent>();
    }

    // in start as DialogueComponent.Options is set in Awake
    private void Start()
    {
        
        buttons = dialogue.Options;
    }
}
