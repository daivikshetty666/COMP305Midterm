using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //serialized private variables
    [SerializeField] private float speed = 9.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    //private variables
    private Rigidbody2D rb; 
    private Animator anim;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    int Crouch = Animator.StringToHash("isDucking");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //Movement Physics
    private void FixedUpdate() 
    {
        //wasd movement
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        //jump
        if(isGrounded && Input.GetAxis("Jump") > 0)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }
        rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);

        if ((isFacingRight && rb.velocity.x < 0) || (!isFacingRight && rb.velocity.x > 0))
            {
                Flip();
            }

        
        anim.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);

    }


    private void Update()
    {
                
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("isDucking",true);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("isDucking",false);
        }
    }
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }
  
}
