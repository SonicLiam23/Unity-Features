using System.Collections;
using System.Collections.Generic;
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

    public float baseDrag = 0f;
    public float baseAngularDrag = 0.05f;
    public Vector2 customGravity = new Vector2(0, -9.81f);

    void Awake() => rb = GetComponent<Rigidbody2D>();

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
        // skip vector calculations if LocalTimeScale is 1, for performance 
        if (Mathf.Approximately(s, 1f))
        {
            rb.velocity = pendingVelocity;
            rb.angularVelocity = pendingAngularVelocity;
            rb.gravityScale = 1f;

            if (jumpQueued)
            {
                pendingVelocity.y = 0f;
                rb.velocity = pendingVelocity;
                rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
                jumpQueued = false;
            }

            rb.drag = baseDrag;
            rb.angularDrag = baseAngularDrag;
            return;
        }

        // only scale X, y is handled below
        Vector2 vel = rb.velocity;       
        vel.x = pendingVelocity.x * s;   
        rb.velocity = vel;

        rb.angularVelocity = pendingAngularVelocity * s;

        // Apply scaled gravity manually
        rb.gravityScale = 0f; // disable built-in gravity
        rb.AddForce(customGravity * s * s, ForceMode2D.Force);

        if (jumpQueued)
        {
            pendingVelocity.y = 0f;
            rb.velocity = pendingVelocity;
            rb.AddForce(Vector2.up * jumpImpulse * s, ForceMode2D.Impulse);
            jumpQueued = false;
        }

        rb.drag = baseDrag * s * s;
        rb.angularDrag = baseAngularDrag * s * s;
    }
}
