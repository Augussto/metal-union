using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    private float speed;
    private PlayerMovement pm;
    private PlayerAttack pa;
    [SerializeField] private LedgeClimb ledgeClimb;

    private string currentState;

    private string p_idle = "Idle";
    private string p_walk = "Run";
    private string p_jump = "jump";
    private string p_attack = "Dash-Attack";
    private string p_takeDamage = "Hurt";
    private string p_die = "die_player";
    private string p_fall = "falling_player";
    private string p_crouch = "crouch_player";
    private string p_crouch_attack = "crouch_attack_player";
    private string p_roll = "Dash";
    private string p_slide = "Slide";
    private string p_hang = "Edge-Idle";


    public bool isLookingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.linearVelocity.magnitude;
        if((int)rb.linearVelocity.x < 0)
        {
            isLookingRight = false;
        }
        else if((int)rb.linearVelocity.x > 0)
        {
            isLookingRight = true;
        }
        sprite.flipX = !isLookingRight;


        //Animation Change
        if (speed > 0 && !pm.jumping && !pa.isAttacking && !pm.isDashing && !ledgeClimb.isHanging) ChangeAnimatorState(p_walk);
        if (speed == 0 && !pm.jumping && !pa.isAttacking && !pm.isDashing && !ledgeClimb.isHanging) ChangeAnimatorState(p_idle);
        if (pm.jumping && !pa.isAttacking) ChangeAnimatorState(p_jump);
        if (pa.isAttacking) ChangeAnimatorState(p_attack);
        if (ledgeClimb.isHanging) ChangeAnimatorState(p_hang);
        if (pm.isGrounded && pm.isDashing) ChangeAnimatorState(p_slide);
        if (!pm.isGrounded && pm.isDashing) ChangeAnimatorState(p_roll);


    }
    public void TakeDamage()
    {
        ChangeAnimatorState(p_takeDamage);
    }

    public void ChangeAnimatorState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
