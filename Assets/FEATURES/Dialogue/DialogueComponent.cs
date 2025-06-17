using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueComponent : MonoBehaviour
{
    private TextMeshProUGUI TMP;
    private TextEffect textEffect;

    [SerializeField] private List<string> entryTextAnimTags;
    [SerializeField] private List<string> exitTextAnimTags;

    [Header("Dialogue Options (leave empty for none)")]
    [SerializeField] private GameObject optionsContainer;
    [SerializeField] private GameObject optionButtonPrefab;

    private void Awake()
    {
        TMP = GetComponent<TextMeshProUGUI>();
        textEffect = GetComponent<TextEffect>();
    }


    public void ShowDialogue(string ID)
    {
        // clear any previous dialogue
        Clear();

        DialogueEntry entry = DialogueManager.GetDialogue(ID);
        string text = (entry==null) ? ID : entry.text;

        TMP.text = text;

        if (optionsContainer != null && entry.options != null)
        {
            foreach (var option in entry.options)
            {
                GameObject buttonObject = Instantiate(optionButtonPrefab, optionsContainer.transform);

            }
        }
        TMP.enabled = true;
        textEffect.enabled = true;
    }
    
    public void HideDialogue()
    {
        if (textEffect == null)
        {
            Clear();
            return;
        }

        textEffect.StartManualEffects();

    }

    public void Clear()
    {
        TMP.text = "";
        if (optionsContainer != null)
        {
            foreach (GameObject option in optionsContainer.transform)
            {
                Destroy(option.gameObject);
            }
        }
        TMP.enabled = false;
        textEffect.enabled = false;
    }

}
