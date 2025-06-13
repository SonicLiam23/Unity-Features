using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(TimeScaleHandler))]
public class MovementStateMachineController : StateMachineBase<IMovementState, MovementStateMachineController>
{
    [HideInInspector] public float velocityMult;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector2 velocity; 
    [HideInInspector] public bool ignoreGrounded = false;
    [HideInInspector] public int jumpsLeft;
    [HideInInspector] public TimeScaleHandler timeScaleHandler;

    [SerializeField] private FootTrigger foot;
    public bool IsGrounded
    { 
        // if igoreGrounded is true, the player will ALWAYS be seen as off the ground
        get => (foot.IsGrounded && !ignoreGrounded); 
    }
    public float JumpForce = 100f;
    public float maxSpeed = 10f;
    public bool canJump = true;
    public int maxJumps = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeScaleHandler = GetComponent<TimeScaleHandler>();

        states = new Dictionary<Type, IMovementState>
        {
            { typeof(JumpState),    new JumpState(this) },
            { typeof(IdleState),    new IdleState(this) },
            { typeof(WalkState),    new WalkState(this) },
            { typeof(FallingState), new FallingState(this) },
        };

        if (foot == null )
        {
            foot = GetComponentInChildren<FootTrigger>();
            if (foot == null)
                Debug.Log("No foot object found, jumps will not be reset");
        }

        jumpsLeft = maxJumps;

    }
    private void Start()
    {
        ChangeState(typeof(FallingState));
    }

    protected override void Update()
    {
        Debug.Log(CurrentState.GetType());
        Debug.Log("Is on ground: " + IsGrounded);
        base.Update(); 
    }
}


public interface IMovementState : IState
{
    public void Move();
    public void Jump();
}
