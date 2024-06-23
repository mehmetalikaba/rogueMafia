using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tirmanma : MonoBehaviour
{
    oyuncuSaldiriTest oyuncuSaldiriTest;
    Rigidbody2D rb;
    public bool tirmaniyor;
    int adim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oyuncuSaldiriTest=GetComponent<oyuncuSaldiriTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tirmaniyor)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            oyuncuSaldiriTest.animator.SetBool("tirmaniyor",true);


            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.35f);
                adim++;
                if (adim == 2)
                {
                    adim = 0;
                }
                oyuncuSaldiriTest.animator.SetInteger("adim", adim);

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.35f);
                adim--;
                if (adim == -1)
                {
                    adim = 1;
                }
                oyuncuSaldiriTest.animator.SetInteger("adim", adim);

            }
            if(Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.Space))
            {
                
                tirmaniyor = false;
            }
        }
        else
        {
            tirmaniyor = false;
            rb.isKinematic = false;

            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;

            oyuncuSaldiriTest.animator.SetBool("tirmaniyor", false);
        }
    }
}
