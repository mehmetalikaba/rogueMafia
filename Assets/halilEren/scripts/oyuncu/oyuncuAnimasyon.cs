using UnityEngine;

public class oyuncuAnimasyon : MonoBehaviour
{
    public GameObject katanaOyuncu, yumiOyuncu;
    public Animator[] animator;
    public int i;

    oyuncuHareket oyuncuHareket;
    Vector2 movementX,movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncuHareket = FindAnyObjectByType<oyuncuHareket>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            i = 1;
            katanaOyuncu.SetActive(true);
            yumiOyuncu.SetActive(false);
        }
        if(Input.GetMouseButtonDown(1))
        {
            i = 2;
            yumiOyuncu.SetActive(true);
            katanaOyuncu.SetActive(false);
        }
        if(oyuncuHareket.egilme ==false)
        {
            movementX.x = oyuncuHareket.rb.velocity.x;
            if (movementX.x == 0)
            {
                animator[i].SetBool("kosu", false);
            }
            else if (movementX.x != 0)
            {
                animator[i].SetBool("kosu", true);
            }

            movementY.y = oyuncuHareket.rb.velocity.y;
            if (movementY.y == 0)
            {
                animator[i].SetBool("zipla", false);
                animator[i].SetBool("dusus", false);
            }
            if (movementY.y > 0)
            {
                animator[i].SetBool("zipla", true);
                animator[i].SetBool("dusus", false);

            }
            if (movementY.y < 0)
            {
                animator[i].SetBool("dusus", true);
                animator[i].SetBool("zipla", false);

            }
        }
        if(oyuncuHareket.egilme)
        {
            animator[i].SetBool("egilme", true);
            animator[i].SetBool("kosu", false);
            animator[i].SetBool("zipla", false);
            animator[i].SetBool("dusus", false);
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
            {
                animator[i].SetBool("kosu", true);

            }
            else
            {
                animator[i].SetBool("kosu", false);

            }
        }
        else
        {
            animator[i].SetBool("egilme", false);

        }

        if (Input.GetKeyDown(KeyCode.End))
        {
            animator[i].SetBool("olum", true);
            oyuncuHareket.enabled = false;
            this.enabled = false;
        }
    }
}
