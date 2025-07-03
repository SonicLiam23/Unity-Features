using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DialogueOptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

        StartCoroutine(Delay());
    }

    public void OnClick()
    {
        Debug.Log("Click");
        dialogueComponent.ShowDialogue(dialogueOption.next);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TMP.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TMP.color = Color.white;
    }

    IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();

        TMP.enabled = true;
        effect.enabled = true;
    }
}
