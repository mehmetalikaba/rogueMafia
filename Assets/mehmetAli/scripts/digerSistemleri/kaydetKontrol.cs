using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaydetKontrol : MonoBehaviour
{
    public GameObject silah1, silah2, toplanabilirObje, ozelGuc1, ozelGuc2;
    public silahOzellikleri silah1Ozellikleri, silah2Ozellikleri, silah1Kaydet, silah2Kaydet;
    public kaydedilecekler kaydedilecekler;
    scriptKontrol scriptKontrol;

    void Awake()
    {
        scriptKontrol = FindAnyObjectByType<scriptKontrol>();
        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        toplanabilirObje = GameObject.Find("toplanabilirEsyaOyuncu");
        ozelGuc1 = GameObject.Find("ozelGuc1");
        ozelGuc2 = GameObject.Find("ozelGuc2");
    }

    void Start()
    {
        envanterGetir();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num6Tusu")))
            envanterKaydet();
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num5Tusu")))
            envanterGetir();
        else if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num4Tusu")))
            envanterKayitTemizle();
    }
    // OYUNCU OLDUGUNDE ENVANTER KAYITLARI TEMIZLENMELI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void envanterKayitTemizle()
    {
        Debug.Log("kayitlarTemizlendi");
        //kaydedilecekler.oyuncuCan = 0;
        //kaydedilecekler.aniPuani = 0;
        kaydedilecekler.ejderParasi = 0;
        kaydedilecekler.silah1Dayaniklilik = 0f;
        kaydedilecekler.silah2Dayaniklilik = 0f;
        kaydedilecekler.toplanabilirObje = null;
        kaydedilecekler.ozelGuc1Obje = null;
        kaydedilecekler.ozelGuc2Obje = null;
        kaydedilecekler.silah1Secimi.tumSilahlar = silahSecimi.silahlar.yumruk;
        kaydedilecekler.silah2Secimi.tumSilahlar = silahSecimi.silahlar.yumruk;

    }

    public void envanterKaydet()
    {
        Debug.Log("envanterKaydedildi");
        kaydedilecekler.oyuncuCan = scriptKontrol.canKontrol.can;
        kaydedilecekler.ejderParasi = scriptKontrol.envanterKontrol.ejderParasi;
        kaydedilecekler.aniPuani = scriptKontrol.envanterKontrol.aniPuani;
        kaydedilecekler.toplanabilirObje = toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirObje;
        kaydedilecekler.ozelGuc1Obje = ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi;
        kaydedilecekler.ozelGuc2Obje = ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi;
        kaydedilecekler.silah1Secimi = silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi;
        kaydedilecekler.silah2Secimi = silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi;
        kaydedilecekler.silah1Dayaniklilik = silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
        kaydedilecekler.silah2Dayaniklilik = silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
    }

    public void envanterGetir()
    {
        Debug.Log("envanterGeldi");
        scriptKontrol.canKontrol.can = kaydedilecekler.oyuncuCan;
        scriptKontrol.envanterKontrol.ejderParasi = kaydedilecekler.ejderParasi;
        scriptKontrol.envanterKontrol.aniPuani = kaydedilecekler.aniPuani;
        toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirObje = kaydedilecekler.toplanabilirObje;
        if (toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirObje != null)
            toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirObjeOzellikleriniGetir();
        ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = kaydedilecekler.ozelGuc1Obje;
        ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = kaydedilecekler.ozelGuc2Obje;
        silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi = kaydedilecekler.silah1Secimi;
        silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi = kaydedilecekler.silah2Secimi;
        silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = kaydedilecekler.silah1Dayaniklilik;
        silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = kaydedilecekler.silah2Dayaniklilik;
    }
}
