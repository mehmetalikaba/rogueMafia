using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toplanabilirKullanmaScripti : MonoBehaviour
{
    public float toplanabilirEtkiSuresi, kalanToplanabilirEtkiSuresi, ilkCan, sonCan, artanCan;

    public GameObject toplanabilirObje;

    public Image toplanabilirIconu, toplanabilirEtkiImage;
    public string toplanabilirKeyi, toplanabilirAdi;
    public string toplanabilirAciklamaKeyi;

    public toplanabilirOzellikleri toplanabilirOzellikleri;

    public bool toplanabilirObjeOzelliginiKullandi, canObjesiAktif;

    canKontrol canKontrol;
    oyuncuHareket oyuncuHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        toplanabilirKeyi = toplanabilirOzellikleri.toplanabilirKeyi;
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("rTusu")) && !toplanabilirObjeOzelliginiKullandi)
        {
            if (toplanabilirObje != null)
            {
                if (toplanabilirAdi == "Can Ýksiri")
                {
                    canKontrol.canIksiriKatkisi = 25f;
                    canObjesiAktif = true;
                    canKontrol.toplanabilirCanObjesiAktif = true;
                }
                if (toplanabilirAdi == "Dayanýklýlýk Ýksiri")
                    canKontrol.dayaniklilikObjesiAktif = true;
                if (toplanabilirAdi == "Hareket Hýzý Ýksiri")
                {
                    oyuncuHareket.hareketHizObjesiAktif = true;
                    canKontrol.hareketHiziObjesiAktif = true;
                }
                if (toplanabilirAdi == "Hasar Ýksiri")
                {
                    oyuncuSaldiriTest.hasarObjesiAktif = true;
                    canKontrol.hareketHiziObjesiAktif = true;
                }


                toplanabilirObjeOzelliginiKullandi = true;
                kalanToplanabilirEtkiSuresi = toplanabilirEtkiSuresi;
            }
        }

        if (toplanabilirObjeOzelliginiKullandi)
        {
            // eger oyuncu, canObjesinin kattýðýndan daha fazla hasar alýrsa, toplanabilir obje anýnda kaybolur.
            if (canObjesiAktif)
                if (canKontrol.canIksiriKatkisi <= 0)
                    toplanabilirObjeKullanildi();
            // eger oyuncu, canObjesinin kattýðýndan daha fazla hasar alýrsa, toplanabilir obje anýnda kaybolur.

            kalanToplanabilirEtkiSuresi -= Time.deltaTime;
            toplanabilirEtkiImage.fillAmount = kalanToplanabilirEtkiSuresi / toplanabilirEtkiSuresi;
            if (kalanToplanabilirEtkiSuresi <= 0)
                toplanabilirObjeKullanildi();
        }
    }
    public void toplanabilirObjeOzellikleriniGetir()
    {
        toplanabilirOzellikleri = toplanabilirObje.GetComponent<toplanabilirOzellikleri>();
        toplanabilirAdi = toplanabilirOzellikleri.toplanabilirAdi;
        toplanabilirAciklamaKeyi = toplanabilirOzellikleri.toplanabilirAciklamaKeyi;
        toplanabilirIconu.sprite = toplanabilirOzellikleri.toplanabilirIcon;
    }
    public void toplanabilirObjeKullanildi()
    {
        if (toplanabilirAdi == "Can Ýksiri")
        {
            canKontrol.canText.text = canKontrol.can.ToString("F0") + "/" + canKontrol.maxCan.ToString("F0");
            canKontrol.canIksiriKatkisi = 0f;
            canKontrol.canIksiriBari.fillAmount = 0f;
            canKontrol.maxCan = 100f;
            canKontrol.pozisyonBelirlendi = false;
        }
        kalanToplanabilirEtkiSuresi = 0f;
        toplanabilirObjeOzelliginiKullandi = false;
        toplanabilirObje = null;
        toplanabilirAdi = null;
        toplanabilirAciklamaKeyi = null;
        toplanabilirIconu.sprite = oyuncuSaldiriTest.yumrukSprite;
        canObjesiAktif = false;
        canKontrol.toplanabilirCanObjesiAktif = false;
        canKontrol.dayaniklilikObjesiAktif = false;
        oyuncuHareket.hareketHizObjesiAktif = false;
        oyuncuSaldiriTest.hasarObjesiAktif = false;
    }
}
