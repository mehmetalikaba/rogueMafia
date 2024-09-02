using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamikLamba : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu") || collision.gameObject.CompareTag("dusman"))
        {
            if (collision.transform.position.x > transform.position.x)
            {
                animator.SetTrigger("solaHareket");

            }
            if (collision.transform.position.x < transform.position.x)
            {
                animator.SetTrigger("sagaHareket");

            }
        }
    }
}
