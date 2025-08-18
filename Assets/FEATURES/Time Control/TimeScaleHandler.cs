using UnityEngine;

public enum TimeScaleMode { MODIFY, UNDO }

[RequireComponent(typeof(Rigidbody2D))]
public class TimeScaleHandler : MonoBehaviour
{
    private float LocalTimeScale = 1f;

    Rigidbody2D rb;
    Vector2 pendingVelocity;
    float pendingAngularVelocity;
    bool jumpQueued;
    float jumpImpulse;
    float startGravityScale;

    public float baseDrag = 0f;
    public float baseAngularDrag = 0f;

    Animator[] animators;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startGravityScale = rb.gravityScale;
    }

    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    public void SetDesiredVelocity(Vector2 vel)
    {
        pendingVelocity = vel;
        // I mainly want rb.linearVelocity.y to be controlled by gravity, so set it once here.
        rb.linearVelocityY = vel.y;

    }
    public void SetDesiredAngularVel(float aVel) => pendingAngularVelocity = aVel;
    public void QueueJump(float impulse) 
    {
        jumpQueued = true;
        jumpImpulse = impulse;
    }
    public void ModifyTime(float factor, TimeScaleMode mode = TimeScaleMode.MODIFY)
    {
        Vector2 vel = rb.linearVelocity;
        if (mode == TimeScaleMode.MODIFY)
        {
            vel.y *= factor;
            LocalTimeScale *= factor;

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].speed *= factor;
            }
        }
        else
        {
            vel.y /= factor;
            LocalTimeScale /= factor;

            for (int i = 0; i < animators.Length; ++i)
            {
                animators[i].speed /= factor;
            }
        }
        rb.linearVelocity = vel;
    }
    public void ResetTime()
    {
        Vector2 vel = rb.linearVelocity;
        vel /= LocalTimeScale;
        LocalTimeScale = 1f;
        rb.linearVelocity = vel;

        for (int i = 0; i < animators.Length; ++i)
        {
            animators[i].speed = 1f;
        }
    }

    void FixedUpdate()
    {
        float s = LocalTimeScale;
        Vector2 vel = rb.linearVelocity;

        // skip vector calculations if LocalTimeScale is 1, for performance 
        if (Mathf.Approximately(s, 1f))
        {
            vel.x = pendingVelocity.x;
            
            rb.angularVelocity = pendingAngularVelocity;

            rb.gravityScale = startGravityScale;

            if (jumpQueued)
            {
                vel.y = jumpImpulse;
                jumpQueued = false;

            }

            rb.linearDamping = baseDrag;
            rb.angularDamping = baseAngularDrag;
            rb.linearVelocity = vel;
            return;
        }
           
        vel.x = pendingVelocity.x * s;
        rb.angularVelocity = pendingAngularVelocity * s;

        // Apply scaled gravity
        rb.gravityScale = startGravityScale * s * s; 

        if (jumpQueued)
        {
            vel.y = jumpImpulse * s;
            jumpQueued = false;
        }

        rb.linearDamping = baseDrag * s * s;
        rb.angularDamping = baseAngularDrag * s * s;
        rb.linearVelocity = vel;

    }
}
