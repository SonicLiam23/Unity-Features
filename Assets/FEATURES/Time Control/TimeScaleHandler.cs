using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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

    public float baseDrag = 0f;
    public float baseAngularDrag = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDesiredVelocity(Vector2 vel) => pendingVelocity = vel;
    public void SetDesiredAngularVel(float aVel) => pendingAngularVelocity = aVel;
    public void QueueJump(float impulse) 
    {
        jumpQueued = true;
        jumpImpulse = impulse;
    }
    public void ModifyTime(float factor, TimeScaleMode mode = TimeScaleMode.MODIFY)
    {
        Vector2 vel = rb.velocity;
        if (mode == TimeScaleMode.MODIFY)
        {
            vel.y *= factor;
            LocalTimeScale *= factor;
        }
        else
        {
            vel.y /= factor;
            LocalTimeScale /= factor;
        }
        rb.velocity = vel;
    }
    public void ResetTime()
    {
        Vector2 vel = rb.velocity;
        vel /= LocalTimeScale;
        LocalTimeScale = 1f;
        rb.velocity = vel;
    }

    void FixedUpdate()
    {
        float s = LocalTimeScale;
        Vector2 vel = rb.velocity;
        vel = rb.velocity;

        // skip vector calculations if LocalTimeScale is 1, for performance 
        if (Mathf.Approximately(s, 1f))
        {
            vel.x = pendingVelocity.x;
            
            rb.angularVelocity = pendingAngularVelocity;

            rb.gravityScale = 1f;

            if (jumpQueued)
            {
                vel.y = jumpImpulse;
                jumpQueued = false;

            }

            rb.drag = baseDrag;
            rb.angularDrag = baseAngularDrag;
            rb.velocity = vel;
            return;
        }
           
        vel.x = pendingVelocity.x * s;
        rb.angularVelocity = pendingAngularVelocity * s;

        // Apply scaled gravity
        rb.gravityScale = s * s; 

        if (jumpQueued)
        {
            vel.y = jumpImpulse * s;
            jumpQueued = false;
        }

        rb.drag = baseDrag * s * s;
        rb.angularDrag = baseAngularDrag * s * s;
        rb.velocity = vel;

    }
}
