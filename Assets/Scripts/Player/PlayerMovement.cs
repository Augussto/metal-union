using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    public Transform groundCheck;
    public LayerMask groundLayer; 

    private Rigidbody2D rb;
    public bool isGrounded;
    public bool jumping;

    public bool isDashing;
    public bool canDash;

    private PlayerAttack pa;
    private PlayerAnimations anims;

    void Start()
    {
        canDash = true;
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        pa = GetComponent<PlayerAttack>();
        anims = GetComponent<PlayerAnimations>();
    }

    void Update()
    {
        if (isDashing) return;
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Horizontal movement
        if (canMove)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumping = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        //Attacking
        if (pa.isAttacking)
        {
            canMove = false;
            rb.linearVelocity = new Vector2(0 * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            canMove = true;
        }

        // Dashing
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(anims.isLookingRight)
            {
                StartCoroutine(Dash(1));
            }
            else
            {
                StartCoroutine(Dash(-1));
            }
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator DashCooldown()
    {
        canDash = false;

        yield return new WaitForSeconds(3);

        canDash = true;
    }

    IEnumerator Dash(float x)
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashForce * x, 0f);

        yield return new WaitForSeconds(1);

        isDashing = false;
        rb.gravityScale = originalGravity;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collides with ground, set isGrounded to true
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumping = false;
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // If player exits collision with ground, set isGrounded to false
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
