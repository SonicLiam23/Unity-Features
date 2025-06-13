using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class JumpState : IMovementState
{
    public JumpState(MovementStateMachineController Pcontroller)
    {
        stateController = Pcontroller;
        rb = stateController.rb;
    }
    private readonly MovementStateMachineController stateController;
    private readonly Rigidbody2D rb;

    public void Move()
    {
        // movement should still happen in the air
        stateController.velocity.y = rb.velocity.y;
        stateController.velocity.x = stateController.velocityMult * stateController.maxSpeed;
        stateController.timeScaleHandler.SetDesiredVelocity(stateController.velocity);
    }

    public void Jump()
    {
        // only called when in jump state and pressed jump again
        // Run OnEnter again, "psuedo" re-enter the state... if that makes sense, as this doesnt call OnExit() or anything
        OnEnter();
    }

    public void OnEnter()
    {
        // entering the jump state, obviously, jump! lol
        
        if (stateController.jumpsLeft > 0)
        {
            stateController.jumpsLeft--;
            if (stateController.jumpsLeft == 0)
                // one of the prerequisites of IMovementState.Jump() being ran is this, so this "cuts it off at the source"
                stateController.canJump = false;


            DisableGroundCheckFor(0.2f);
            stateController.timeScaleHandler.QueueJump(stateController.JumpForce);
        }
    }
    public void UpdateState()
    {
        Move();
        if (rb.velocity.y <= 0)
        {
            stateController.ChangeState(typeof(FallingState));
        }
    }

    private IEnumerator DisableGroundCheckFor(float duration)
    {
        stateController.ignoreGrounded = true;
        yield return new WaitForSeconds(duration);
        stateController.ignoreGrounded = false;
    }
}
