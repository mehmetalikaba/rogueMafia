using UnityEngine;

public class oyuncuAnimasyon : MonoBehaviour
{
    oyuncuHareket oyuncuHareket;
    Rigidbody2D rb;
    Vector2 movementX,movementY;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncuHareket = FindAnyObjectByType<oyuncuHareket>();
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(oyuncuHareket.egilme ==false)
        {
            movementX.x = rb.velocity.x;
            if (movementX.x == 0)
            {
                animator.SetBool("kosu", false);
            }
            else if (movementX.x != 0)
            {
                animator.SetBool("kosu", true);
            }

            movementY.y = rb.velocity.y;
            if (movementY.y == 0)
            {
                animator.SetBool("zipla", false);
                animator.SetBool("dusus", false);
            }
            if (movementY.y > 0)
            {
                animator.SetBool("zipla", true);
                animator.SetBool("dusus", false);

            }
            if (movementY.y < 0)
            {
                animator.SetBool("dusus", true);
                animator.SetBool("zipla", false);

            }
        }
        if(oyuncuHareket.egilme)
        {
            animator.SetBool("egilme", true);
            animator.SetBool("kosu", false);
            animator.SetBool("zipla", false);
            animator.SetBool("dusus", false);
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
            {
                animator.SetBool("kosu", true);

            }
            else
            {
                animator.SetBool("kosu", false);

            }
        }
        else
        {
            animator.SetBool("egilme", false);

        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("saldiri");
        }
        if(Input.GetKeyDown(KeyCode.End))
        {
            animator.SetBool("olum", true);
            oyuncuHareket.enabled = false;
            this.enabled = false;
        }
    }
}
