using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce;
    public Rigidbody2D theRB;
    public Animator anim;

    public LayerMask whatIsGround;
    public Transform groundCheckPoint;

    public bool isGrounded;

    public SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if(Input.GetButtonDown("Jump"))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            theSR.flipX = false;
        } else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            theSR.flipX = true;
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
    }
}
