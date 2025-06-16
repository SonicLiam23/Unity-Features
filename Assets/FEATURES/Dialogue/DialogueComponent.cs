using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueComponent : MonoBehaviour
{
    private TextMeshProUGUI TMP;
    [Header("Dialogue Options (leave empty for none)")]
    [SerializeField] private GameObject optionsContainer;
    [SerializeField] private GameObject optionButtonPrefab;

    private void Awake()
    {
        TMP = GetComponent<TextMeshProUGUI>();
    }


    public void ShowDialogue(string ID)
    {
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
    }
    
}
