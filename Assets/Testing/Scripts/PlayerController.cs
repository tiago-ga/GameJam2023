using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Initialize variables
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 20;
    private float xAxis;
    Animator anim;


    // Ground settings, serialized to see on Unity as well
    [Header("Ground Check Settings")]
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    public static PlayerController Instance;

    private void Awake()
    {
        // Creates an instance of the character to be called out for the camera
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Initialize the Rigibody and Animator
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // Updates frame by frame the following functions
        getInputs();
        move();
        jump();
        flip();
    }

    void getInputs()
    {
        // Calls the horizontal movement inputs
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void move()
    {
        // Calculates the movement and sets the animation for it
        rb.velocity = new Vector2(moveSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && grounded());
    }

    public bool grounded()
    {
        // Checks if the character is grounded (to make sure is not walking in the air or double jump for example)
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheck.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
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
        // Makes sure the character is not in the air, otherwise it won't allow to jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        // Checks if the character is on the ground and then allows for jump
        if (Input.GetButtonDown("Jump") && grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        // Sets the jumping animation if the character is in the air
        anim.SetBool("Jumping", !grounded());
    }

    void flip()
    {
        // Checks the horizontal input to flip the character accordingly
        if (xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }
}
