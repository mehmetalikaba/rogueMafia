using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ejderParasiScripti : MonoBehaviour
{
    //public GameObject vfx;
    public envanterKontrol envanterKontrol;

    void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            //Instantiate(vfx, transform.position, Quaternion.identity);
            envanterKontrol.ejderhaPuaniArttir(envanterKontrol.ejderhaPuaniArtmaMiktari);
            Destroy(gameObject);
        }
    }
}
