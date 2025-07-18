using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    UP, DOWN, LEFT, RIGHT
}

/// <summary>
/// A player character, containing unique movement that takes player input
/// </summary>
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(AttackManager))]
public class Player : Character, PlayerInputActions.IPlayerActions
{
    // start facing right
    Vector2 lookVec = Vector2.right;
    Direction facingLR;
    AttackManager attackManager;
    [SerializeField] private float verticalAttackDeadzone = 0.7f;
    [SerializeField] private float horizontalMoveDeadzone = 0.3f;

    protected override void Awake()
    {
        base.Awake();

        RigidBodyComp.constraints = RigidbodyConstraints2D.FreezeRotation;

        InputManager.Instance.InputActions.Player.SetCallbacks(this);
        InputManager.Instance.InputActions.Player.Enable(); // Enables the player action map

        attackManager = GetComponent<AttackManager>();
    }

    private Direction GetDirection()
    {
        float dot = Vector2.Dot(Vector2.up, lookVec);

        // is the dot product greater than the deadzone? (default 0.7)
        if (Mathf.Abs(dot) >= verticalAttackDeadzone)
        {
            // if yes, we are attacking up or down
            bool attackUp = (dot >= 0f);
            if (attackUp)
            {
                return Direction.UP;
            }
            else
            {
                return Direction.DOWN;
            }
        }
        else
        {
            return facingLR;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float newVelocity = context.ReadValue<float>();
            if (Mathf.Abs(newVelocity) > horizontalMoveDeadzone)
            {
                Debug.Log(newVelocity);
                MovementController.velocityMult = newVelocity;

                bool facingLeft = (newVelocity <= 0f);
                if (facingLeft)
                {
                    facingLR = Direction.LEFT;
                }
                else
                {
                    facingLR = Direction.RIGHT;
                }
            }

        }
        if (context.canceled)
        {
            MovementController.velocityMult = 0f;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && MovementController.canJump)
        {
            MovementController.CurrentState.Jump();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lookVec = context.ReadValue<Vector2>();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !attackManager.Attacking)
        {
            attackManager.ChangeAttackType(AttackType.ATTACK);
            attackManager.Attack(GetDirection());
        }
    }
    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.started && !attackManager.Attacking)
        {
            attackManager.ChangeAttackType(AttackType.PARRY);
            attackManager.Attack(GetDirection());
        }
    }





}