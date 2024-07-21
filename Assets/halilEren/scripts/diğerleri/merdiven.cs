using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merdiven : MonoBehaviour
{
    public tirmanma tirmanma;

    void Start()
    {
        tirmanma = FindObjectOfType<tirmanma>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            tirmanma.oyuncuYakin = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            tirmanma.oyuncuYakin = false;
            tirmanma.tirmaniyor = false;
        }
    }
}
