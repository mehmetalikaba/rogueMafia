using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cim : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (animator != null)
            animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu") || collision.gameObject.CompareTag("dusman"))
        {
            if (collision.transform.position.x > transform.position.x)
            {
                if (animator != null)
                    animator.SetTrigger("cimSol");

            }
            if (collision.transform.position.x < transform.position.x)
            {
                if (animator != null)
                    animator.SetTrigger("cimSag");
            }
        }
    }
}
