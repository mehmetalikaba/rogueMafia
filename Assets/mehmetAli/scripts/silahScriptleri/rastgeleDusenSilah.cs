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
    public float yokOlmaSuresi, dayaniklilik;


    void Start()
    {
        yokOlmaSuresi = 7.5f;

        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        spriteRenderer = GetComponent<SpriteRenderer>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        silah1OzellikleriniGetir = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2OzellikleriniGetir = silah2.GetComponent<silahOzellikleriniGetir>();


        spriteRenderer.sprite = dusenSilah.silahIcon;
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && (oyuncuYakin))
        {
            if (dusenSilah.silahTuru == "yakin")
            {

                if (silah1OzellikleriniGetir == null)
                {
                    Debug.Log("silah1OzellikleriniGetir null geldi");
                }

                if (silah1OzellikleriniGetir.secilenSilahOzellikleri.silahAdi != "YUMRUK" && silah1OzellikleriniGetir.secilenSilahOzellikleri.silahAdi == null)
                {
                    Debug.Log(silah1OzellikleriniGetir.secilenSilahOzellikleri.silahAdi);
                    silahKontrol.silah1YereAt();
                }

                silahKontrol.silahAldi = true;
                silah1Getir();
            }
            else if (dusenSilah.silahTuru == "menzilli")
            {

                if (silah2OzellikleriniGetir == null)
                {
                    Debug.Log("silah2OzellikleriniGetir null geldi");
                }

                if (silah2OzellikleriniGetir.secilenSilahOzellikleri.silahAdi != "YUMRUK" && silah2OzellikleriniGetir.secilenSilahOzellikleri.silahAdi == null)
                {
                    Debug.Log(silah1OzellikleriniGetir.secilenSilahOzellikleri.silahAdi);
                    silahKontrol.silah2YereAt();
                }

                silahKontrol.silahAldi = true;
                silah2Getir();
            }
        }


        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
        {
            Destroy(gameObject);
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
        oyuncuSaldiriTest.animator.runtimeAnimatorController = silah1OzellikleriniGetir.karakterAnimator;
        silah1OzellikleriniGetir.silahDayanikliligi = dayaniklilik;
        oyuncuSaldiriTest.silah1DayanikliligiImage.fillAmount = oyuncuSaldiriTest.silah1Script.silahDayanikliligi / 100;
        Destroy(gameObject);
    }

    public void silah2Getir()
    {
        oyuncuSaldiriTest.yumruk2 = false;
        oyuncuSaldiriTest.silahUltileri.silah2Ulti = 0f;
        silah2OzellikleriniGetir.secilenSilahOzellikleri = dusenSilah;
        silah2OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi.ToLower());
        silah2OzellikleriniGetir.silahOzellikleriniGuncelle();
        oyuncuSaldiriTest.animator.runtimeAnimatorController = silah2OzellikleriniGetir.karakterAnimator;
        silah2OzellikleriniGetir.silahDayanikliligi = dayaniklilik;
        oyuncuSaldiriTest.silah2DayanikliligiImage.fillAmount = oyuncuSaldiriTest.silah2Script.silahDayanikliligi / 100;
        Destroy(gameObject);
    }
}
