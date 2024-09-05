using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fare : MonoBehaviour
{
    oyuncuHareket oyuncu;
    Animator animator;
    public bool kac,kacmayaBasladi;
    public float speed=2;
    // Start is called before the first frame update
    void Start()
    {
        oyuncu=FindObjectOfType<oyuncuHareket>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         float f = Vector2.Distance(transform.transform.position, oyuncu.transform.position);
        if(f<1)
        {
            kac = true;
        }
        if(f>2)
        {
            kac = false;
        }
        if(kac)
        {
            animator.SetBool("walk", true);
            if(oyuncu.transform.position.x<transform.position.x)
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        if(!kac)
        {
            animator.SetBool("walk", false);
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
