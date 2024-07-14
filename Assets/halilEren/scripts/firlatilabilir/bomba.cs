using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{
    public GameObject patlamaAlani;
    public bool zamanlamali,anlikPatlamali;
    public GameObject vurulmaSesi;
    public GameObject tozPartikül;
    public float speed = 20f,rotateSpeed;
    Rigidbody2D rb;
    private float angle;

    bool sekti;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void FixedUpdate()
    {
        if(!sekti)
        {
            transform.Rotate(0, 0, rotateSpeed);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            if(anlikPatlamali)
            {
                Patla();
            }
            if (zamanlamali)
            {
                StartCoroutine(patlamaZamani());
                patlamaAlani.SetActive(true);
            }

            if (sekti)
            {
                if (!zamanlamali)
                {
                    Patla();
                }
            }


            sekti = true;


        }
        if (collision.gameObject.CompareTag("dusman"))
        {
            Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(tozPartikül, transform.position, Quaternion.identity);
        }
    }
    void Patla()
    {
        Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
        Instantiate(tozPartikül, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    

    IEnumerator patlamaZamani()
    {
        yield return new WaitForSeconds(2);
        Patla();
    }
}

