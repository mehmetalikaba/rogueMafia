using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandikKirma : MonoBehaviour
{
    public bool oyuncuYakin;

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = false;
    }
}
