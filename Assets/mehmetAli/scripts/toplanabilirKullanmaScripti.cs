using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toplanabilirKullanmaScripti : MonoBehaviour
{

    public float toplanabilirEtkiSuresi, kalanToplanabilirEtkiSuresi, ilkCan, sonCan, artanCan;

    public GameObject toplanabilirObje;

    public Image toplanabilirIconu, toplanabilirEtkiImage;
    public string toplanabilirAdi;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("rTusu")) && !toplanabilirObjeOzelliginiKullandi)
        {
            if (toplanabilirObje != null)
            {
                if (toplanabilirAdi == "Can �ksiri")
                {
                    Debug.Log("can toplanabiliri kullanildi");
                    canObjesiAktif = true;
                    ilkCan = canKontrol.can;
                    sonCan = ilkCan + artanCan;
                    canKontrol.can = sonCan;
                    canKontrol.canBari.fillAmount = canKontrol.can / 100f;
                    canKontrol.toplanabilirCanObjesiAktif = true;
                }
                if (toplanabilirAdi == "Dayan�kl�l�k �ksiri")
                    canKontrol.dayaniklilikObjesiAktif = true;
                if (toplanabilirAdi == "Hareket H�z� �ksiri")
                    oyuncuHareket.hareketHizObjesiAktif = true;
                if (toplanabilirAdi == "Hasar �ksiri")
                    oyuncuSaldiriTest.hasarObjesiAktif = true;

                toplanabilirObjeOzelliginiKullandi = true;
                kalanToplanabilirEtkiSuresi = toplanabilirEtkiSuresi;
            }
            else
                Debug.Log("toplanabilir yok");
        }

        if (toplanabilirObjeOzelliginiKullandi)
        {
            // eger oyuncu, canObjesinin katt���ndan daha fazla hasar al�rsa, toplanabilir obje an�nda kaybolur.
            if (canObjesiAktif)
                if (sonCan - canKontrol.can > 25)
                    toplanabilirObjeKullanildi();
            // eger oyuncu, canObjesinin katt���ndan daha fazla hasar al�rsa, toplanabilir obje an�nda kaybolur.

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
        if (toplanabilirAdi == "Can �ksiri")
        {
            canKontrol.can = ilkCan;
            canKontrol.canBari.fillAmount = canKontrol.can / 100f;
        }
        kalanToplanabilirEtkiSuresi = 0f;
        toplanabilirObjeOzelliginiKullandi = false;
        toplanabilirObje = null;
        toplanabilirAdi = null;
        toplanabilirAciklamaKeyi = null;
        toplanabilirIconu.sprite = null;
        canObjesiAktif = false;
        canKontrol.toplanabilirCanObjesiAktif = false;
        canKontrol.dayaniklilikObjesiAktif = false;
        oyuncuHareket.hareketHizObjesiAktif = false;
        oyuncuSaldiriTest.hasarObjesiAktif = false;
    }
}
