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
    public bool canJump;
    
    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;


    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;
    
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
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
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
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rb.velocity = new Vector2(rb.velocity.x,  jumpForce);
            canJump = false;
        }
        
    }

    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position, transform.rotation);

            nextAttack = Time.time + attackRate;
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

    public void LandFX()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
