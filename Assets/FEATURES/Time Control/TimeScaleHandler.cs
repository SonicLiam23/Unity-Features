using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TimeScaleHandler : MonoBehaviour
{
    public float LocalTimeScale = 1f;

    Rigidbody2D rb;
    Vector2 pendingVelocity;
    float pendingAngularVelocity;
    bool jumpQueued;
    float jumpImpulse;

    public float baseDrag = 0f;
    public float baseAngularDrag = 0.05f;
    public Vector2 customGravity = new Vector2(0, -9.81f); // match your project settings

    void Awake() => rb = GetComponent<Rigidbody2D>();

    public void SetDesiredVelocity(Vector2 vel) => pendingVelocity = vel;
    public void SetDesiredAngularVel(float aVel) => pendingAngularVelocity = aVel;
    public void QueueJump(float impulse) { jumpQueued = true; jumpImpulse = impulse; }


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
                rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
                jumpQueued = false;
            }

            rb.drag = baseDrag;
            rb.angularDrag = baseAngularDrag;
            return;
        }

        rb.velocity = pendingVelocity * s;
        rb.angularVelocity = pendingAngularVelocity * s;

        // Apply scaled gravity manually
        rb.gravityScale = 0f; // disable built-in gravity
        rb.AddForce(customGravity * s, ForceMode2D.Force);

        if (jumpQueued)
        {
            rb.AddForce(Vector2.up * jumpImpulse * s, ForceMode2D.Impulse);
            jumpQueued = false;
        }

        rb.drag = baseDrag * s * s;
        rb.angularDrag = baseAngularDrag * s * s;
    }
}
