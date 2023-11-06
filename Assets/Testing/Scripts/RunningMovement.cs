using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMovement : MonoBehaviour
{
    private float horizontal;
    private float speed=8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;


    Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator = this.GetComponent<Animator>();
        if (Input.GetButton("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetTrigger("Jump");
        }
        if (Input.GetButton("Jump") && rb.velocity.y>0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
            animator.SetTrigger("Jump");
        }

        Flip();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
        if (horizontal*speed==0)
        {
            animator.SetTrigger("Stop");
        }
        else
        {
            animator.SetTrigger("Run");
        }
        

    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal <0f || isFacingRight && horizontal > 0f) { 
            isFacingRight = !isFacingRight;
            Vector3 localSacle = transform.localScale;
            localSacle.x *= -1f;
            transform.localScale = localSacle;
        }
    }
}
