using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rastgeleDusenSilah : MonoBehaviour
{
    public silahOzellikleri dusenSilah;
    public GameObject silah1, silah2;
    public silahKontrol silahKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public silahOzellikleriniGetir silah1OzellikleriniGetir, silah2OzellikleriniGetir;
    public SpriteRenderer spriteRenderer;
    public bool oyuncuYakin;


    void Start()
    {
        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        spriteRenderer = GetComponent<SpriteRenderer>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        spriteRenderer.sprite = dusenSilah.silahIcon;
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && (oyuncuYakin))
        {
            if (dusenSilah.silahTuru == "yakin")
            {
                silahKontrol.silah1YereAt();
                silahKontrol.silahAldi = true;
                silah1OzellikleriniGetir = silah1.GetComponent<silahOzellikleriniGetir>();
                silah1Getir();
            }
            else if (dusenSilah.silahTuru == "menzilli")
            {
                silahKontrol.silah2YereAt();
                silahKontrol.silahAldi = true;
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
        oyuncuSaldiriTest.yumruk1 = false;
        oyuncuSaldiriTest.silahUltileri.silah1Ulti = 0f;
        silah1OzellikleriniGetir.secilenSilahOzellikleri = dusenSilah;
        silah1OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi.ToLower());
        silah1OzellikleriniGetir.silahOzellikleriniGuncelle();
        Destroy(gameObject);
    }

    public void silah2Getir()
    {
        oyuncuSaldiriTest.yumruk2 = false;
        oyuncuSaldiriTest.silahUltileri.silah2Ulti = 0f;
        silah2OzellikleriniGetir.secilenSilahOzellikleri = dusenSilah;
        silah2OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi.ToLower());
        silah2OzellikleriniGetir.silahOzellikleriniGuncelle();
        Destroy(gameObject);
    }
}
