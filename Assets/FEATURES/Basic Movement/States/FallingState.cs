using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IMovementState
{
    readonly MovementStateMachineController stateController;
    readonly Rigidbody2D rb;
    public FallingState(MovementStateMachineController Pcontroller)
    {
        stateController = Pcontroller;
        rb = stateController.rb;
    }

    public void Jump()
    {
        stateController.ChangeState(typeof(JumpState));
    }

    public void Move()
    {
        stateController.velocity.y = rb.velocity.y;
        stateController.velocity.x = stateController.velocityMult * stateController.maxSpeed;
        stateController.timeScaleHandler.SetDesiredVelocity(stateController.velocity);

    }

    // Update is called once per frame
    public void UpdateState()
    {
        Move();
        if (stateController.IsGrounded)
        {
            stateController.jumpsLeft = stateController.maxJumps;
            // are we moving at all
            if (stateController.velocityMult == 0f)
            {
                stateController.ChangeState(typeof(IdleState));
                // play landing anim
            }
            else
            {
                stateController.ChangeState(typeof(WalkState));
            }
        }
    }
}
