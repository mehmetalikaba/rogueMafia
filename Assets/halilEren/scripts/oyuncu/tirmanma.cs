using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tirmanma : MonoBehaviour
{
    public GameObject yurumeSes, yurumeSes1;
    public float dikeyHareket, tirmanmaHizi, ilkRbGravity, degisimTimer;
    public bool oyuncuYakin, tirmaniyor, tirmanmaBitti, birinciAnim, yukariBasiyor, asagiBasiyor;
    public Rigidbody2D rb;
    public oyuncuHareket oyuncuHareket;
    public Animator animator;
    public oyuncuSaldiriTest oyuncuSaldiriTest;

    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        rb = GetComponent<Rigidbody2D>();
        ilkRbGravity = rb.gravityScale;
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        degisimTimer = 0.075f;
    }

    void Update()
    {
        if (oyuncuYakin)
        {
            dikeyHareket = Input.GetAxis("Vertical");

            if (dikeyHareket != 0f && !oyuncuHareket.atiliyor)
            {
                animator.SetBool("tirmaniyor", true);
                animator.SetBool("zipla", false);
                animator.SetBool("kosu", false);

                tirmanmaBitti = false;
                tirmaniyor = true;
                oyuncuHareket.yuruyor = false;
                oyuncuHareket.havada = false;

                degisimTimer -= Time.deltaTime;

                if (degisimTimer < 0)
                {
                    if (!birinciAnim)
                    {
                        birinciAnim = true;
                        degisimTimer = 0.15f;
                        animator.SetBool("tirmanma1", true);
                        animator.SetBool("tirmanma2", false);
                    }
                    else
                    {
                        birinciAnim = false;
                        degisimTimer = 0.15f;
                        animator.SetBool("tirmanma1", false);
                        animator.SetBool("tirmanma2", true);
                    }
                }
            }
        }
        else
        {
            tirmaniyor = false;
            if (!tirmanmaBitti)
                StartCoroutine(tirmaniyorAnimasyon());
        }
    }

    void FixedUpdate()
    {
        if (tirmaniyor)
        {
            oyuncuSaldiriTest.silahlarKilitli = true;
            yurumeSes.SetActive(false);
            yurumeSes1.SetActive(false);
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, dikeyHareket * tirmanmaHizi);
        }
        else
        {
            if (rb != null)
                rb.gravityScale = ilkRbGravity;
        }
    }

    public IEnumerator tirmaniyorAnimasyon()
    {
        if (!tirmanmaBitti)
        {
            tirmanmaBitti = true;
            yield return new WaitForSeconds(0.05f);
            animator.SetBool("tirmaniyor", false);
            animator.SetBool("tirmanma1", false);
            animator.SetBool("tirmanma2", false);
            oyuncuSaldiriTest.silahlarKilitli = false;
        }
    }
}
