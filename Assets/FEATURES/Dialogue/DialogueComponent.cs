using EasyTextEffects;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This needs revising. The instantiation, deletion and getcomponent calls are generally inefficient. But as im dealing with maybe 1 or 2 dialogues at once, the impact is hopefully negligible.
// Will 100% come back to this as this project develops.


[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueComponent : MonoBehaviour
{
    private TextMeshProUGUI TMP;
    /// <summary>
    /// Stores a reference to all the text effects (for this and the options)
    /// </summary>
    private TextEffect textEffect;
    [SerializeField] private string dialogueID;
 
    [Header("Dialogue Options (leave empty for none)")]
    [SerializeField] private GameObject optionButtonPrefab;
    public List<DialogueOptionButton> Options { get; private set; }

    string text;
    DialogueEntry currentEntry;

    private void Awake()
    {
        TMP = GetComponent<TextMeshProUGUI>();
        textEffect = GetComponent<TextEffect>();
        Options = new();
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
            ClearDialogue();
            dialogueID = newID;
            currentEntry = DialogueManager.GetDialogue(dialogueID);
            text = (currentEntry == null) ? dialogueID + " not found" : currentEntry.text;
            TMP.text = text;
        }        

        if (currentEntry.options != null && optionButtonPrefab != null)
        {
            foreach (var option in currentEntry.options)
            {
                DialogueOptionButton button = Instantiate(optionButtonPrefab, transform.parent).GetComponent<DialogueOptionButton>();
                button.Init(option, this);
                Options.Add(button);
                
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
            ClearDialogue();
            return;
        }

        textEffect.StartManualEffects();
        foreach (var option in Options)
        {
            option.GetComponentInChildren<TextEffect>().StartManualEffects();
        }
    }

    

    /// <summary>
    /// Use this to immediatley cut the dialogue. No effects.
    /// </summary>
    public void ClearDialogue()
    {

        foreach (DialogueOptionButton option in Options)
        {
            Destroy(option.gameObject);
        }
        Options.Clear();
        TMP.enabled = false;
        textEffect.enabled = false;
    }

}
