using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elmasTest : MonoBehaviour
{
    public envanterKontrol envanterKontrol;

    void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            envanterKontrol.elmasArttir(1);
            Destroy(gameObject);
        }
    }
}
