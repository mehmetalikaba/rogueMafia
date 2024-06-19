using UnityEngine;

public class oyuncuAnimasyon : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 movementX,movementY;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX.x = rb.velocity.x;
        if (movementX.x == 0)
        {
            animator.SetBool("run",false);
        }
        else if(movementX.x != 0)
        {
            animator.SetBool("run", true);
        }

        movementY.y = rb.velocity.y;
        if(movementY.y == 0)
        {
            animator.SetBool("jump", false);
            animator.SetBool("fall", false);
        }
        if(movementY.y > 0)
        {
            animator.SetBool("jump", true);
            animator.SetBool("fall", false);

        }
        if (movementY.y < 0)
        {
            animator.SetBool("fall", true);
            animator.SetBool("jump", false);

        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("attack");
        }
    }
}
