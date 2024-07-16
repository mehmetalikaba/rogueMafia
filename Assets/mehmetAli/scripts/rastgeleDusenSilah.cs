using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rastgeleDusenSilah : MonoBehaviour
{
    public silahOzellikleri dusenSilah;
    public GameObject silah1, silah2;
    public silahOzellikleriniGetir silah1OzellikleriniGetir, silah2OzellikleriniGetir;
    public SpriteRenderer silahIconu;
    public silahSecimi silahSecimi;
    public bool oyuncuYakin;

    void Start()
    {
        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        silahIconu = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && (oyuncuYakin))
        {
            if (dusenSilah.silahTuru == "yakin")
            {
                silah1OzellikleriniGetir = silah1.GetComponent<silahOzellikleriniGetir>();
                silah1Getir();
            }
            else if (dusenSilah.silahTuru == "menzilli")
            {
                silah2OzellikleriniGetir = silah2.GetComponent<silahOzellikleriniGetir>();
                silah2Getir();
            }
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

    public void silah1Getir()
    {
        silah1OzellikleriniGetir.seciliSilah = dusenSilah;
        silah1OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi);
        silah1OzellikleriniGetir.silahOzellikleriniGuncelle();
        Destroy(gameObject);
    }

    public void silah2Getir()
    {
        silah2OzellikleriniGetir.seciliSilah = dusenSilah;
        silah2OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi.ToLower());
        silah2OzellikleriniGetir.silahOzellikleriniGuncelle();
        Destroy(gameObject);
    }
}
