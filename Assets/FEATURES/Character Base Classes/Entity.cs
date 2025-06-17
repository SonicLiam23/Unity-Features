using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to define an entity in the world with a Rigidbody and optional dialogue box.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DialogueTrigger))]
public class Entity : MonoBehaviour
{
    public Rigidbody2D RigidBodyComp { get; protected set; }
    public DialogueComponent DialogueComp { get; protected set; }

    protected virtual void Awake()
    {
        RigidBodyComp = GetComponent<Rigidbody2D>();
        DialogueComp = GetComponentInChildren<DialogueComponent>();

        if (DialogueComp == null)
            Debug.LogWarning($"DialogueComponent not found in {gameObject.name}. Dialogue on this object will not be shown.\nIf you did mean to add it, ensure it is attatched to a child of {gameObject.name}.");
    }
}
