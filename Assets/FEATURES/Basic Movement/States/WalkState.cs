using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IMovementState
{
    public WalkState(MovementStateMachineController Pcontroller)
    {
        stateController = Pcontroller;
        rb = stateController.rb;
    }
    private readonly MovementStateMachineController stateController;
    private readonly Rigidbody2D rb;

    public void Move()
    {
        stateController.velocity.y = rb.velocity.y;
        stateController.velocity.x = stateController.velocityMult * stateController.maxSpeed;
        stateController.timeScaleHandler.SetDesiredVelocity(stateController.velocity);

    }

    public void Jump()
    {
        stateController.ChangeState(typeof(JumpState));
    }

    public void OnEnter()
    {
        stateController.canJump = true;
    }
    public void UpdateState()
    {
        Move();   
        if (stateController.velocityMult == 0f)
        {
            stateController.ChangeState(typeof(IdleState));
        }
        // in case ground randomly drops, start falling
        else if (!stateController.IsGrounded)
        {
            stateController.ChangeState(typeof(FallingState));
        }
    }
}
