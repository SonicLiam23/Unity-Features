using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using System;


/// <summary>
/// A character entity, includes movement.
/// </summary>
[RequireComponent(typeof(MovementStateMachineController))]
[RequireComponent(typeof(HealthComponent))]
public class Character : Entity
{
    public MovementStateMachineController MovementController { get; protected set; }
    
    protected override void Awake()
    {
        base.Awake();
        MovementController = GetComponent<MovementStateMachineController>();

    }

    public virtual void Move(Vector2 newVelocity)
    {
        RigidBodyComp.velocity = newVelocity;
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
