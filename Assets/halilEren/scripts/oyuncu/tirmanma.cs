using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tirmanma : MonoBehaviour
{
    public float vertical, tirmanmaHizi, ilkRbGravity, degisimTimer;
    public bool oyuncuYakin, tirmaniyor, tirmanmaBitti, birinciAnim;
    public Rigidbody2D rb;
    public oyuncuHareket oyuncuHareket;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ilkRbGravity = rb.gravityScale;
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        tirmanmaHizi = oyuncuHareket.hareketHizi;
        degisimTimer = 0.05f;
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (oyuncuYakin && Mathf.Abs(vertical) > 0f)
        {
            tirmanmaBitti = false;
            tirmaniyor = true;

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
            animator.SetBool("tirmaniyor", true);
            animator.SetBool("zipla", false);
            animator.SetBool("kosu", false);
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
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * tirmanmaHizi);
        }
        else
        {
            rb.gravityScale = ilkRbGravity;
        }
    }

    IEnumerator tirmaniyorAnimasyon()
    {
        if (!tirmanmaBitti)
        {
            tirmanmaBitti = true;
            yield return new WaitForSeconds(0.25f);
            animator.SetBool("tirmaniyor", false);
            animator.SetBool("tirmanma1", false);
            animator.SetBool("tirmanma2", false);
        }
    }
}
