using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    [Header("Set References")]
    public Animator animator;
    public SpriteRenderer sr;

    [Header("Movement")]
    public float maxSpeed = 5f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    private Vector2 input;
    private Vector2 currentVelocity;

    [Header("Rotation")]
    private int facingDirection = 1;
    bool isFacingRight = true;
 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }


    public void HandleInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;
        animator.SetFloat("speed", input.sqrMagnitude);
    }

    public void HandleMovement()
    {
        rb.velocity = input * maxSpeed;

        CheckMovementDirection();
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && input.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && input.x > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDirection *= -1;
        sr.flipX = !isFacingRight;
        //transform.Rotate(0, 180, 0);
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }
}
