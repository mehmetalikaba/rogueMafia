using System;
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
    public AudioSource aldi;
    oyuncuHareket oyuncuHareket;


    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        yokOlmaSuresi = 15f;

        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        spriteRenderer = GetComponent<SpriteRenderer>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        aldi = GameObject.Find("aldi").GetComponent<AudioSource>();

        silah1OzellikleriniGetir = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2OzellikleriniGetir = silah2.GetComponent<silahOzellikleriniGetir>();

        spriteRenderer.sprite = dusenSilah.silahIcon;
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.silahAldi)
        {
            if (dusenSilah.silahTuru == "yakin")
            {
                if (!oyuncuSaldiriTest.yumruk1)
                    silahKontrol.silah1YereAt();
                silah1Getir();
            }
            else if (dusenSilah.silahTuru == "menzilli")
            {
                if (!oyuncuSaldiriTest.yumruk2)
                    silahKontrol.silah2YereAt();
                silah2Getir();
            }
        }

        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
            Destroy(gameObject);
    }


    public void silah1Getir()
    {
        try
        {
            aldi.Play();
            silahKontrol.silahAldi = true;
            oyuncuSaldiriTest.yumruk1 = false;
            oyuncuSaldiriTest.silahUltileri.silah1Ulti = 0f;
            silah1OzellikleriniGetir.secilenSilahOzellikleri = dusenSilah;
            silah1OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi.ToLower());
            silah1OzellikleriniGetir.silahOzellikleriniGuncelle();
            oyuncuSaldiriTest.animator.runtimeAnimatorController = silah1OzellikleriniGetir.karakterAnimator;
            silah1OzellikleriniGetir.silahDayanikliligi = dayaniklilik;
            oyuncuSaldiriTest.silah1DayanikliligiImage.fillAmount = oyuncuSaldiriTest.silah1Script.silahDayanikliligi / oyuncuSaldiriTest.silah1Script.silahDayanikliligi;
        }
        catch (Exception e)
        {
            Debug.Log("HATA OLDU SILAH GETIR KONTROL ET <==> " + e.Message);
        }
        finally
        {
            Destroy(gameObject);
        }
    }

    public void silah2Getir()
    {
        try
        {
            aldi.Play();
            silahKontrol.silahAldi = true;
            oyuncuSaldiriTest.yumruk2 = false;
            oyuncuSaldiriTest.silahUltileri.silah2Ulti = 0f;
            silah2OzellikleriniGetir.secilenSilahOzellikleri = dusenSilah;
            silah2OzellikleriniGetir.silahSecimi.silahSec(dusenSilah.silahAdi.ToLower());
            silah2OzellikleriniGetir.silahOzellikleriniGuncelle();
            oyuncuSaldiriTest.animator.runtimeAnimatorController = silah2OzellikleriniGetir.karakterAnimator;
            silah2OzellikleriniGetir.silahDayanikliligi = dayaniklilik;
            oyuncuSaldiriTest.silah2DayanikliligiImage.fillAmount = oyuncuSaldiriTest.silah2Script.silahDayanikliligi / oyuncuSaldiriTest.silah1Script.silahDayanikliligi;
        }
        catch (Exception e)
        {
            Debug.Log("HATA OLDU SILAH GETIR KONTROL ET <==> " + e.Message);
        }
        finally
        {
            Destroy(gameObject);
        }
    }

}
