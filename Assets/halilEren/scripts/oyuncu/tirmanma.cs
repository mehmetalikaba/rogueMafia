using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tirmanma : MonoBehaviour
{
    oyuncuSaldiriTest oyuncuSaldiriTest;
    Rigidbody2D rb;
    public bool tirmaniyor;
    int adim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oyuncuSaldiriTest = GetComponent<oyuncuSaldiriTest>();
    }
    void Update()
    {
        if (tirmaniyor)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            oyuncuSaldiriTest.animator.SetBool("tirmaniyor", true);


            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("wTusu")))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.35f);
                adim++;
                if (adim == 2)
                {
                    adim = 0;
                }
                oyuncuSaldiriTest.animator.SetInteger("adim", adim);

            }
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sTusu")))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.35f);
                adim--;
                if (adim == -1)
                {
                    adim = 1;
                }
                oyuncuSaldiriTest.animator.SetInteger("adim", adim);

            }
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftControlTusu")) || (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("spaceTusu"))))
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
