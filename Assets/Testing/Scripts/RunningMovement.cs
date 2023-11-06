using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10;
    private float xAxis;
    Animator anim;
    

    [Header("Ground Check Settings")]
    [SerializeField] private float jumpForce = 45;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        getInputs();
        move();
        jump();
        flip();
    }

    private void FixedUpdate()
    {


    }
    void getInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void move()
    {
        rb.velocity = new Vector2(moveSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && grounded());
    }

    public bool grounded()
    {
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, whatIsGround) 
            || Physics2D.Raycast(groundCheck.position+new Vector3(groundCheckX,0,0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheck.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void jump()
    {
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
        }
        if (Input.GetButtonDown("Jump") && grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }


        anim.SetBool("Jumping", !grounded());
    }

    void flip()
    {
        if (xAxis<0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }else if (xAxis>0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }
}
