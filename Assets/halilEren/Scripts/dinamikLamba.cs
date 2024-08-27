using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinamikLamba : MonoBehaviour
{
    CapsuleCollider2D collider2D;
    // Start is called before the first frame update
    void Start()
    {
        collider2D=GetComponent<CapsuleCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            StartCoroutine(waitTime());
        }
    }
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.05f);
        collider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        collider2D.enabled = true;

    }
}
