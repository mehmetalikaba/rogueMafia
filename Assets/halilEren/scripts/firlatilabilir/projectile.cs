using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool dusmandan;

    oyuncuSaldiriTest oyuncuSaldiriTest;
    public GameObject vurulmaSesi;
    public GameObject tozPartikül;
    public float speed, rotateSpeed;
    Rigidbody2D rb;
    private float angle;

    bool carpti;
    private void Awake()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        if(!dusmandan)
        {
            speed = oyuncuSaldiriTest.sonSaldiriMenzili;
        }
        rb.velocity = transform.right * speed;
    }


    void FixedUpdate()
    {
        if (!carpti)
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.Rotate(0, 0, rotateSpeed);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            Debug.Log("Çarpti");
            carpti = true;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.freezeRotation = true;
            Instantiate(vurulmaSesi,transform.position,Quaternion.identity);
            Instantiate(tozPartikül, transform.position, Quaternion.identity);
            speed = 0;
            gameObject.tag = "Untagged";
        }
    }
}

