using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to define an entity in the world.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TimeScaleHandler))]
public class Entity : MonoBehaviour
{
    public Team team;
    public Rigidbody2D RigidBodyComp { get; protected set; }

    protected virtual void Awake()
    {
        RigidBodyComp = GetComponent<Rigidbody2D>();

    }
}
