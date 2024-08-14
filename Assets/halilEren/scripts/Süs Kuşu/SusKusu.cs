using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusKusu : MonoBehaviour
{
    public AudioSource sfx;
    float timer;
    public float ucmaZamani;

    public bool sola, saga;

    Animator animator;

    public bool  havalaniyor;
    public float hareketHizi;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!havalaniyor)
        {
            timer += Time.deltaTime;
            if (timer > ucmaZamani)
            {

                sfx.Play();
                havalaniyor = true;
                timer = 0;

            }
        }
        if (havalaniyor)
        {
            transform.Translate(transform.up * hareketHizi * Time.deltaTime);

            if(saga)
                transform.Translate(transform.right * 2 * Time.deltaTime);
            if(sola)
                transform.Translate(transform.right * -2 * Time.deltaTime);

            animator.SetBool("ucma", true);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu")||collision.gameObject.CompareTag("ok"))
        {
            sfx.Play();

            havalaniyor = true;
        }
    }
}
