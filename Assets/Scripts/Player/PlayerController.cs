using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    
    public float speed;
    
    public float jumpForce;
    
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    
    [Header("State Check")]
    public bool isGround;
    public bool isJump;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
        PhysicsCheck();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isJump = true;
        }
    }
    
    
    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // -1~1 包括小数
        rb.velocity = new Vector2(horizontalInput*speed, rb.velocity.y);


        if (horizontalInput != 0)
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        
    }

    void Jump()
    {
        if (isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,  jumpForce);
            isJump = false;
        }
        
    }
    
    
    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
        else
        {
            rb.gravityScale = 4;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
