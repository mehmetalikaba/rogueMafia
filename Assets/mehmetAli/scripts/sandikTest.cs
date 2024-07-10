using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandikTest : MonoBehaviour
{
    public envanterKontrol envanterKontrol;

    public bool oyuncuYakin;

    void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (oyuncuYakin)
            {
                int randomHpPotuMiktar = Random.Range(1, 2);
                envanterKontrol.hpPotuGeldi(randomHpPotuMiktar);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = false;
    }
}
