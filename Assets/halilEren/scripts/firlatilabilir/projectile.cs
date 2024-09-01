using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int kacDusman;
    public bool dusmandan, bomba;
    public float speed, rotateSpeed, angle;
    public GameObject vurulmaSesi, tozPartikül;
    Rigidbody2D rb;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    canKontrol canKontrol;
    BoxCollider2D boxCollider2d;

    bool carpti;
    private void Awake()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if (!dusmandan)
            speed = oyuncuSaldiriTest.sonSaldiriMenzili;
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
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            if (bomba)
            {
                Collider2D[] oyuncuAlanHasari = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Oyuncu"));
                for (int i = 0; i < oyuncuAlanHasari.Length; i++)
                {
                    if (oyuncuAlanHasari[i].name == "Oyuncu")
                    {
                        canKontrol = FindObjectOfType<canKontrol>();
                        canKontrol.canAzalmasi(25, "patlayanDusman");
                        Destroy(gameObject);
                    }
                }
            }
            carpti = true;
            boxCollider2d.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.freezeRotation = true;
            Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(tozPartikül, transform.position, Quaternion.identity);
            speed = 0;
            gameObject.tag = "Untagged";
        }
    }
}

