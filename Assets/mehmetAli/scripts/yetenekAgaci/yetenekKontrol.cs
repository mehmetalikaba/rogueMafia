using System.Collections.Generic;
using UnityEngine;

public class yetenekKontrol : MonoBehaviour
{
    public yetenekAgaclari yetenekAgaclari;
    public silahOzellikleri[] butunMenzilliSilahlar, butunYakinSilahlar;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public envanterKontrol envanterKontrol;

    void Start()
    {
        skilleriUygulama();
    }

    void Update()
    {

    }

    public void skilleriUygulama()
    {
        menzilliSkillEtkileriniUygula();
        pasif1SkillEtkileriniUygula();
        pasif2SkillEtkileriniUygula();
        pasif3SkillEtkileriniUygula();
        yakinSkillEtkileriniUygula();
    }

    /*public void normalleriGetirme()
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
    }*/

    public void menzilliSkillEtkileriniUygula()
    {
        if (yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi == 1 && !yetenekAgaclari.menzilliYetenekler[0].oyunaUygulandi)
        {
            yetenekAgaclari.menzilliYetenekler[0].oyunaUygulandi = true;
            float etkiDegeri = 0.05f;
            butunMenzilliSilahlar[0].silahSaldiriHasari *= 1 + etkiDegeri;
            butunMenzilliSilahlar[1].silahSaldiriHasari *= 1 + etkiDegeri;
        }

        if (yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi == 1 && !yetenekAgaclari.menzilliYetenekler[1].oyunaUygulandi)
        {
            yetenekAgaclari.menzilliYetenekler[1].oyunaUygulandi = true;
            float etkiDegeri = 0.5f;
            butunMenzilliSilahlar[0].silahSaldiriHasari *= 1 + etkiDegeri;
            butunMenzilliSilahlar[1].silahSaldiriHasari *= 1 + etkiDegeri;
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
            float etkiDegeri = 0.05f;
            butunYakinSilahlar[0].silahSaldiriHasari *= 1 + etkiDegeri;
            butunYakinSilahlar[1].silahSaldiriHasari *= 1 + etkiDegeri;
        }

        if (yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi == 1 && !yetenekAgaclari.yakinYetenekler[1].oyunaUygulandi)
        {
            yetenekAgaclari.yakinYetenekler[1].oyunaUygulandi = true;
            float etkiDegeri = 0.5f;
            butunYakinSilahlar[0].silahSaldiriHasari *= 1 + etkiDegeri;
            butunYakinSilahlar[1].silahSaldiriHasari *= 1 + etkiDegeri;
        }

        if (yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi == 1 && !yetenekAgaclari.yakinYetenekler[2].oyunaUygulandi)
        {
            yetenekAgaclari.yakinYetenekler[2].oyunaUygulandi = true;
            oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari -= 5;
        }
    }
}
