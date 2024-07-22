using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asamaKontrol : MonoBehaviour
{
    public bool oyuncuGeldi;

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuGeldi = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuGeldi = false;
    }
}
