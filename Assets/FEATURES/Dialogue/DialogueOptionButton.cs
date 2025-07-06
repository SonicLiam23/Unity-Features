using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DialogueOptionButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP;
    [SerializeField] TextEffect effect;
    DialogueOption dialogueOption;
    DialogueComponent dialogueComponent;

    public void Init(DialogueOption option, DialogueComponent dialogue)
    { 
        dialogueOption = option;
        dialogueComponent = dialogue;
        //TMP = GetComponentInChildren<TextMeshProUGUI>();

        TMP.text = "[ " + option.text + " ]";

        StartCoroutine(DelayEnable());
    }

    public void OnClick()
    {
        Debug.Log("Click");
        dialogueComponent.ShowDialogue(dialogueOption.next);
    }

    public void Select()
    {
        TMP.color = Color.yellow;
    }

    public void Deselect()
    {
        TMP.color = Color.white;
    }

    IEnumerator DelayEnable()
    {
        yield return new WaitForEndOfFrame();

        TMP.enabled = true;
        effect.enabled = true;
    }
}
