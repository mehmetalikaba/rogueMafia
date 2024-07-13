using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool sabitKalsin;
    public GameObject vurulmaSesi;
    public GameObject tozPartikül;
    public float speed = 20f;
    Rigidbody2D rb;
    private float angle;

    bool carpti;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!carpti)
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(tozPartikül, transform.position, Quaternion.identity);

            if (sabitKalsin)
            {
                Debug.Log("Çarpti");
                carpti = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.freezeRotation = true;
                speed = 0;
            }
            if(!sabitKalsin)
            {
                Destroy(gameObject);
            }

        }
        if(collision.gameObject.CompareTag("dusman"))
        {
            Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(tozPartikül, transform.position, Quaternion.identity);
        }
    }
}