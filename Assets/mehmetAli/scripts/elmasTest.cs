using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elmasTest : MonoBehaviour
{
    public envanterKontrol envanterKontrol;
    public int aniMiktari;

    void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        aniMiktari = Random.Range(3, 7);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            envanterKontrol.elmasArttir(aniMiktari);
            Destroy(gameObject);
        }
    }
}
