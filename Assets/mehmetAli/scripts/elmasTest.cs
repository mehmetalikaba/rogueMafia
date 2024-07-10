using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elmasTest : MonoBehaviour
{
    public envanterKontrol envanterKontrol;
    public float elmasMiktari;

    void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        elmasMiktari = Random.Range(3, 7);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            envanterKontrol.elmasArttir(elmasMiktari);
            Destroy(gameObject);
        }
    }
}
