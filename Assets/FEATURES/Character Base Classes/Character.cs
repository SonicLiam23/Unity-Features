using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using System;
using Newtonsoft.Json;


/// <summary>
/// A character entity, includes movement and health.
/// </summary>
[RequireComponent(typeof(MovementStateMachineController))]
[RequireComponent(typeof(HealthBase))]
public class Character : Entity
{
    /// <summary>
    /// Vector of the direction the character is looking.
    /// </summary>
    public Vector2 LookVec { get; protected set; }
    public MovementStateMachineController MovementController { get; protected set; }
    public HealthBase HealthComp { get; protected set; }

    public Weapon HeldWeapon = null;
    
    protected override void Awake()
    {
        base.Awake();
        MovementController = GetComponent<MovementStateMachineController>();
        HealthComp = GetComponent<HealthBase>();

        HeldWeapon = GetComponentInChildren<Weapon>();

    }

    public virtual void Move(Vector2 newVelocity)
    {
        RigidBodyComp.linearVelocity = newVelocity;
    }

    public virtual void MoveTo(Transform destination)
    {
        throw new NotImplementedException();
    }

    public virtual void MoveTo(Vector2 destination)
    {
        throw new NotImplementedException();
    }
}
