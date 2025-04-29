using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    [SerializeField] private float radius;
    public bool onLedge, canLedge, isHanging;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform playerPos;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private BoxCollider2D collider;

    [SerializeField] private PlayerMovement pm;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canLedge)
        {
            onLedge = Physics2D.OverlapCircle(transform.position, radius,whatIsGround);

            if (onLedge && !isHanging)
            {
                StartCoroutine(StartHanging());
            }
        }
        else
        {
            isHanging = false;
            onLedge = false;
        }

        if (isHanging)
        {
            playerRb.linearVelocity = Vector2.zero;
            collider.isTrigger = false;
        }
        else if(!isHanging)
        {
            collider.isTrigger = true;
        }

        if(isHanging && Input.GetKey(KeyCode.S))
        {
            isHanging = false;
            canLedge = false;
            onLedge = false;
        }
        if (isHanging && Input.GetButtonDown("Jump"))
        {
            isHanging = false;
            pm.jumping = true;
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, pm.jumpForce);
        }
    }


    IEnumerator StartHanging()
    {
        yield return new WaitForSeconds(0.08f);
        if(onLedge)
        {
            isHanging = true;
        }
        else
        {
            isHanging = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canLedge = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canLedge = true;
    }
}
