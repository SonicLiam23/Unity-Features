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
    [SerializeField] private string dialogueID;
 
    [Header("Dialogue Options (leave empty for none)")]
    [SerializeField] private GameObject optionButtonPrefab;
    List<GameObject> options;

    string text;
    DialogueEntry currentEntry;

    private void Awake()
    {
        TMP = GetComponent<TextMeshProUGUI>();
        textEffect = GetComponent<TextEffect>();
        options = new();
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(dialogueID))
        {
            currentEntry = DialogueManager.GetDialogue(dialogueID);
            text = (currentEntry == null) ? dialogueID + " not found" : currentEntry.text;
            TMP.text = text;
            TMP.enabled = false;
            textEffect.enabled = false;
        }
    }


    public void ChangeID(string ID)
    {
        dialogueID = ID;
    }


    /// <summary>
    /// shows the dialogue from the ID stored on this object.
    /// </summary>
    public void ShowDialogue(string newID = "")
    {
        if (!string.IsNullOrEmpty(newID) && newID != dialogueID)
        {
            Clear();
            dialogueID = newID;
            currentEntry = DialogueManager.GetDialogue(dialogueID);
            text = (currentEntry == null) ? dialogueID + " not found" : currentEntry.text;
            TMP.text = text;
        }        

        if (currentEntry.options != null && optionButtonPrefab != null)
        {
            foreach (var option in currentEntry.options)
            {
                GameObject buttonObject = Instantiate(optionButtonPrefab, transform.parent);
                DialogueOptionButton button = buttonObject.GetComponent<DialogueOptionButton>();
                button.Init(option, this);
                options.Add(buttonObject);
            }
        }
        TMP.enabled = true;
        textEffect.enabled = true;

    }
    
    /// <summary>
    /// Use this to run any exit effects then clear.
    /// </summary>
    public void HideDialogue()
    {
        if (textEffect == null)
        {
            Clear();
            return;
        }

        textEffect.StartManualEffects();
    }

    

    /// <summary>
    /// Use this to immediatley cut the dialogue. No effects.
    /// </summary>
    public void Clear()
    {
        if (optionButtonPrefab != null)
        {
            foreach (GameObject option in options)
            {
                Destroy(option);
            }
            options.Clear();
        }
        TMP.enabled = false;
        textEffect.enabled = false;
    }

}
