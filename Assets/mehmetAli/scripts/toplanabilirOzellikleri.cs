using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toplanabilirOzellikleri : MonoBehaviour
{
    public string toplanabilirAdi, toplanabilirAciklamaKeyi;

    public Sprite toplanabilirIcon;

    public canKontrol canKontrol;

    public oyuncuSaldiriTest oyuncuSaldiriTest;

    public oyuncuHareket oyuncuHareket;

    public float toplanabilirEtkiSuresi;

    public bool buObjeCan, buObjeDayaniklilik, buObjeHasar, buObjeHareketHizi;

    public void toplanabilirObjeOzelliginiKullan()
    {

        if (buObjeCan)
        {
            canObjesiOzelligi();
        }
        if (buObjeDayaniklilik)
        {
            dayaniklilikObjesiOzelligi();
        }
        if (buObjeHasar)
        {
            hasarObjesiOzelligi();
        }
        if (buObjeHareketHizi)
        {
            hareketHiziObjesiOzelligi();
        }
    }


    public void canObjesiOzelligi()
    {
        Debug.Log("can toplanabiliri kullanildi");
        canKontrol = FindObjectOfType<canKontrol>();

        if (canKontrol.can < 75)
        {
            canKontrol.can += ((canKontrol.can / 100) * 25);
        }
        else if (canKontrol.can > 75)
        {
            float fazlaOlanCanMiktari = 100 - canKontrol.can;

            canKontrol.can = 75;

            canKontrol.can += ((canKontrol.can / 100) * 25);
        }
    }
    public void dayaniklilikObjesiOzelligi()
    {
        Debug.Log("dayaniklilik toplanabiliri kullanildi");
        canKontrol = FindObjectOfType<canKontrol>();

        canKontrol.dayaniklilikObjesiAktif = true;
    }
    public void hasarObjesiOzelligi()
    {
        Debug.Log("hasar toplanabiliri kullanildi");
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        oyuncuSaldiriTest.hasarObjesiAktif = true;
    }
    public void hareketHiziObjesiOzelligi()
    {
        Debug.Log("hareket hizi toplanabiliri kullanildi");
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        oyuncuHareket.hareketHizObjesiAktif = true;
    }




}
