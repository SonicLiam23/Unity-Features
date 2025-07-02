using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueOptionButton : MonoBehaviour
{
    TextMeshProUGUI TMP;
    DialogueOption dialogueOption;
    DialogueComponent dialogueComponent;
    TextEffect effect;

    public void Init(DialogueOption option, DialogueComponent dialogue)
    { 
        dialogueOption = option;
        dialogueComponent = dialogue;
        TMP = GetComponentInChildren<TextMeshProUGUI>();
        effect = GetComponentInChildren<TextEffect>();

        TMP.text = "[ " + option.text + " ]";
    }

    public void OnClick()
    {
        Debug.Log("Click");
        dialogueComponent.ShowDialogue(dialogueOption.next);
    }
}
