using System.Collections.Generic;
using UnityEngine;

public class yetenekKontrol : MonoBehaviour
{
    public yetenekAgaclari yetenekAgaclari;
    public List<yetenekObjesi> menzilliYeteneklerListesi, pasifYeteneklerListesi, yakinYeteneklerListesi;
    public silahOzellikleri[] butunMenzilliSilahlar, butunYakinSilahlar;

    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public envanterKontrol envanterKontrol;
    public canKontrol canKontrol;

    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, toplanabilir;

    public silahOzellikleri silah1Ozellikleri, silah2Ozellikleri;

    public silahOzellikleriniGetir silah1OzellikleriGetir, silah2OzellikleriGetir;

    void Start()
    {
        menzilliYeteneklerListesi = yetenekAgaclari.menzilliYetenekler;
        pasifYeteneklerListesi = yetenekAgaclari.pasifYetenekler;
        yakinYeteneklerListesi = yetenekAgaclari.yakinYetenekler;

        silah1OzellikleriGetir = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2OzellikleriGetir = silah2.GetComponent<silahOzellikleriniGetir>();

        silah1Ozellikleri = silah1OzellikleriGetir.secilenSilahOzellikleri;
        silah2Ozellikleri = silah1OzellikleriGetir.secilenSilahOzellikleri;

    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num9Tusu")))
        {
            skilleriUygulama();
        }
    }

    public void skilleriUygulama()
    {
        menzilliSkillEtkileriniUygula();
        pasif1SkillEtkileriniUygula();
        pasif2SkillEtkileriniUygula();
        pasif3SkillEtkileriniUygula();
        yakinSkillEtkileriniUygula();
    }

    public void normalleriGetirme()
    {
        butunMenzilliSilahlar[0].silahSaldiriHasari = 15;
        butunMenzilliSilahlar[1].silahSaldiriHasari = 10;
        oyuncuSaldiriTest.silah2DayanikliligiAzalmaMiktari = 10f;

        butunYakinSilahlar[0].silahSaldiriHasari = 25;
        butunYakinSilahlar[1].silahSaldiriHasari = 20;
        oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari = 10;

        ozelGuc1KullanmaScripti.ozelGuc1ToplamSure = 10;
        ozelGuc2KullanmaScripti.ozelGuc2ToplamSure = 10;

        envanterKontrol.olunceAniMiktariAzalmaYuzdesi = 2;
        envanterKontrol.ejderhaPuaniArtmaMiktari = 50;
    }

    public void menzilliSkillEtkileriniUygula()
    {
        if (yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi == 1 && !yetenekAgaclari.menzilliYetenekler[0].oyunaUygulandi)
        {
            yetenekAgaclari.menzilliYetenekler[0].oyunaUygulandi = true;
            butunMenzilliSilahlar[0].silahSaldiriHasari += butunMenzilliSilahlar[0].silahSaldiriHasari * 0.05f;
            butunMenzilliSilahlar[1].silahSaldiriHasari += butunMenzilliSilahlar[1].silahSaldiriHasari * 0.05f;
        }
        if (yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi == 1 && !yetenekAgaclari.menzilliYetenekler[1].oyunaUygulandi)
        {
            yetenekAgaclari.menzilliYetenekler[1].oyunaUygulandi = true;
            butunMenzilliSilahlar[0].silahSaldiriHasari += butunMenzilliSilahlar[0].silahSaldiriHasari * 0.5f;
            butunMenzilliSilahlar[1].silahSaldiriHasari += butunMenzilliSilahlar[1].silahSaldiriHasari * 0.5f;
        }
        if (yetenekAgaclari.menzilliYetenekler[2].yetenekSeviyesi == 1 && !yetenekAgaclari.menzilliYetenekler[2].oyunaUygulandi)
        {
            yetenekAgaclari.menzilliYetenekler[2].oyunaUygulandi = true;
            oyuncuSaldiriTest.silah2DayanikliligiAzalmaMiktari -= 5;
        }
    }

    public void pasif1SkillEtkileriniUygula()
    {
        if (yetenekAgaclari.pasifYetenekler[0].yetenekSeviyesi == 1 && !yetenekAgaclari.pasifYetenekler[0].oyunaUygulandi)
        {
            yetenekAgaclari.pasifYetenekler[0].oyunaUygulandi = true;
            envanterKontrol.olunceAniMiktariAzalmaYuzdesi = 1.5f;
        }
    }
    public void pasif2SkillEtkileriniUygula()
    {
        if (yetenekAgaclari.pasifYetenekler[1].yetenekSeviyesi == 1 && !yetenekAgaclari.pasifYetenekler[1].oyunaUygulandi)
        {
            yetenekAgaclari.pasifYetenekler[1].oyunaUygulandi = true;
            ozelGuc1KullanmaScripti.ozelGuc1ToplamSure = 7.5f;
            ozelGuc2KullanmaScripti.ozelGuc2ToplamSure = 7.5f;
        }
    }
    public void pasif3SkillEtkileriniUygula()
    {
        if (yetenekAgaclari.pasifYetenekler[2].yetenekSeviyesi == 1 && !yetenekAgaclari.pasifYetenekler[2].oyunaUygulandi)
        {
            yetenekAgaclari.pasifYetenekler[2].oyunaUygulandi = true;
            envanterKontrol.ejderhaPuaniArtmaMiktari = 75f;
        }
    }

    public void yakinSkillEtkileriniUygula()
    {
        if (yetenekAgaclari.yakinYetenekler[0].yetenekSeviyesi == 1 && !yetenekAgaclari.yakinYetenekler[0].oyunaUygulandi)
        {
            yetenekAgaclari.yakinYetenekler[0].oyunaUygulandi = true;
            butunYakinSilahlar[0].silahSaldiriHasari += butunYakinSilahlar[0].silahSaldiriHasari * 0.05f;
            butunYakinSilahlar[1].silahSaldiriHasari += butunYakinSilahlar[1].silahSaldiriHasari * 0.05f;
        }
        if (yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi == 1 && !yetenekAgaclari.yakinYetenekler[1].oyunaUygulandi)
        {
            yetenekAgaclari.yakinYetenekler[1].oyunaUygulandi = true;
            butunYakinSilahlar[0].silahSaldiriHasari += butunYakinSilahlar[0].silahSaldiriHasari * 0.5f;
            butunYakinSilahlar[1].silahSaldiriHasari += butunYakinSilahlar[1].silahSaldiriHasari * 0.5f;
        }
        if (yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi == 1 && !yetenekAgaclari.yakinYetenekler[2].oyunaUygulandi)
        {
            yetenekAgaclari.yakinYetenekler[2].oyunaUygulandi = true;
            oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari -= 5;
        }
    }
}
