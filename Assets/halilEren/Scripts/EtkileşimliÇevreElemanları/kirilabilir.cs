using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kirilabilir : MonoBehaviour
{
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public GameObject kirilmisObj, dusecekOlanIksir, fx;
    public bool oyuncuYakin, vazoKirildi, iksirDustu;
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && oyuncuYakin && !oyuncuSaldiriTest.yumruk1)
            kirildi();
    }
    public void kirildi()
    {
        if (!vazoKirildi)
        {
            vazoKirildi = true;
            spriteRenderer.enabled = false;
            Instantiate(fx, transform.position, Quaternion.identity);
            Instantiate(kirilmisObj, transform.position, kirilmisObj.transform.rotation);
            float random = Random.Range(0, 100);
            if (random <= 25 && !iksirDustu)
            {
                iksirDustu = true;
                Instantiate(dusecekOlanIksir, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shuriken") || (collision.CompareTag("ok")))
            kirildi();
    }
}