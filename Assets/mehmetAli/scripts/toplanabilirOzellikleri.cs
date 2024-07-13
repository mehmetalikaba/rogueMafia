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

    public float toplanabilirEtkiSuresi, ilkCan;

    public bool buObjeCan, buObjeDayaniklilik, buObjeHasar, buObjeHareketHizi, canObjesiAktif;

    toplanabilirKullanmaScripti toplanabilirKullanmaScripti;

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
        canObjesiAktif = true;
        float sonCan, artanCan;
        ilkCan = canKontrol.can;
        artanCan = 25;
        sonCan = ilkCan + artanCan;
        canKontrol.can = sonCan;
        //StartCoroutine(canObjesiBasladi());
        if ((sonCan - canKontrol.can) > artanCan)
            canObjesiAktif = false;
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
    IEnumerator canObjesiBasladi()
    {
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        yield return new WaitForSeconds(toplanabilirKullanmaScripti.kalanToplanabilirEtkiSuresi);
        canObjesiAktif = false;
        canKontrol.can = ilkCan;
    }
}
