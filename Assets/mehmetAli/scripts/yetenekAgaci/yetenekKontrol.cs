using System.Collections.Generic;
using UnityEngine;

public class yetenekKontrol : MonoBehaviour
{
    public yetenekAgaclari yetenekAgaclari;
    public List<yetenekObjesi> menzilliYeteneklerListesi, pasifYeteneklerListesi, yakinYeteneklerListesi;

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

        silah1Ozellikleri = silah1OzellikleriGetir.silahOzellikleriniGetirSilahOzellikleri;
        silah2Ozellikleri = silah1OzellikleriGetir.silahOzellikleriniGetirSilahOzellikleri;
        pasifSkillEtkileriniUygula();
    }

    void Update()
    {
    }



    public void menzilliSkillEtkileriniUygula()
    {
        if (yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi == 1)
        {
            Debug.Log(oyuncuSaldiriTest.sonHasar);
            Debug.Log(oyuncuSaldiriTest.sonHasar = oyuncuSaldiriTest.silah1Script.silahSaldiriHasari * 0.05f);
        }
        if (yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi > 0)
        {
            Debug.Log(oyuncuSaldiriTest.sonHasar);
            Debug.Log(oyuncuSaldiriTest.sonHasar = yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi / 10);
        }
        if (yetenekAgaclari.menzilliYetenekler[2].yetenekSeviyesi > 0)
        {
            Debug.Log(oyuncuSaldiriTest.silahDayanikliligiAzalmaMiktari);
            Debug.Log(oyuncuSaldiriTest.silahDayanikliligiAzalmaMiktari -= yetenekAgaclari.menzilliYetenekler[2].yetenekSeviyesi);
        }
    }

    public void pasifSkillEtkileriniUygula()
    {

        if (yetenekAgaclari.pasifYetenekler[0].yetenekSeviyesi == 1)
        {

        }
        if (yetenekAgaclari.pasifYetenekler[1].yetenekSeviyesi > 0)
        {

        }
        if (yetenekAgaclari.pasifYetenekler[2].yetenekSeviyesi > 0)
        {

        }
    }

    public void yakinSkillEtkileriniUygula()
    {
        if (yetenekAgaclari.yakinYetenekler[0].yetenekSeviyesi == 1)
        {
            Debug.Log(oyuncuSaldiriTest.sonHasar);
            Debug.Log(oyuncuSaldiriTest.sonHasar = oyuncuSaldiriTest.silah1Script.silahSaldiriHasari * 0.05f);
        }
        if (yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi > 0)
        {
            Debug.Log(oyuncuSaldiriTest.sonHasar);
            Debug.Log(oyuncuSaldiriTest.sonHasar = yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi / 10);
        }
        if (yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi > 0)
        {
            Debug.Log(oyuncuSaldiriTest.silahDayanikliligiAzalmaMiktari);
            Debug.Log(oyuncuSaldiriTest.silahDayanikliligiAzalmaMiktari -= yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi);
        }
    }
}
