using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IMovementState
{
    readonly MovementStateMachineController stateController;
    public IdleState(MovementStateMachineController Pcontroller)
    {
        stateController = Pcontroller;
    }
    public void Move()
    {
        
    }

    public void Jump()
    {
        stateController.ChangeState(typeof(JumpState));
    }

    public void OnEnter()
    {
        // some idle anim
        stateController.canJump = true;  
    }

    public void UpdateState()
    {
        if (stateController.velocityMult != 0f)
        {
            stateController.ChangeState(typeof(WalkState));
        }
        else if (!stateController.IsGrounded)
        {
            stateController.ChangeState(typeof(FallingState));
        }
    }
}
