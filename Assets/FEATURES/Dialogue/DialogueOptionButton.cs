using EasyTextEffects;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueOptionButton : MonoBehaviour, IMenuButton 
{
    [SerializeField] TextMeshProUGUI TMP;
    [SerializeField] TextEffect effect;
    DialogueOption dialogueOption;
    DialogueComponent dialogueComponent;

    public void Init(DialogueOption option, DialogueComponent dialogue)
    { 
        dialogueOption = option;
        dialogueComponent = dialogue;

        TMP.text = "[ " + option.text + " ]";

        StartCoroutine(DelayEnable());
    }

    public void OnClick()
    {
        Debug.Log("Click");
        dialogueComponent.ShowDialogue(dialogueOption.next);
    }

    public void Focus()
    {
        TMP.color = Color.yellow;
    }

    public void UnFocus()
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
