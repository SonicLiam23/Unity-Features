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
    /// <summary>
    /// When true, player is always seen as in air. Used to prevent false positives on this "IsGrounded" when jumping.
    /// </summary>
    [HideInInspector] public bool ignoreGrounded = false;
    [HideInInspector] public int jumpsLeft;
    [HideInInspector] public TimeScaleHandler timeScaleHandler;

    [SerializeField] public FootTrigger foot;
    public bool IsGrounded
    {

        get => (foot.IsGrounded);
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
        base.Update(); 
    }

    public IEnumerator DisableGroundCheck()
    {
        // only set it back AFTER the foot trigger has left the ground
        ignoreGrounded = true;
        while (foot.IsGrounded)
        {
            Debug.Log("Ground Disabled!");
            yield return null;
        }
        ignoreGrounded = false;
        
    }
}


public interface IMovementState : IState
{
    public void Move();
    public void Jump();
}


