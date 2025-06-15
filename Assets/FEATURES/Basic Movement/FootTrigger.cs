using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FootTrigger : MonoBehaviour
{
    public bool IsGrounded {  get; private set; } 

    private void Awake()
    {
        IsGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            IsGrounded = false;
    }
}
