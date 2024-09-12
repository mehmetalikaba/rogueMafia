using System.IO;
using UnityEngine;

public class yetenekKontrol : MonoBehaviour
{
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public envanterKontrol envanterKontrol;
    public kaydetKontrolYetenek kaydetKontrolYetenek;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public int[] menzilliSeviyeler = new int[3], pasifSeviyeler = new int[3], yakinSeviyeler = new int[3];
    public yetenekObjesi[] menzilliYetenekler = new yetenekObjesi[3], pasifYetenekler = new yetenekObjesi[3], yakinYetenekler = new yetenekObjesi[3];

    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        kaydetKontrolYetenek = FindObjectOfType<kaydetKontrolYetenek>();

        if (File.Exists(kaydetKontrolYetenek.path))
            kaydetKontrolYetenek.jsonYetenekYukle();
        else
        {
            kaydetKontrolYetenek.jsonYetenekKaydet();
            kaydetKontrolYetenek.jsonYetenekYukle();
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num9Tusu")))
        {
            kaydetKontrolYetenek.jsonYetenekSifirla();
            yetenekleriUygula();
        }
    }
    public void yetenekButonunaBasti(string hangiYetenek, int kacinciYetenek)
    {
        Debug.Log("YETENEK GELISTIRILDI: " + hangiYetenek + " <==> " + kacinciYetenek);

        if (hangiYetenek == "menzilli")
        {
            menzilliSeviyeler[kacinciYetenek]++;
            menzilliYetenekler[kacinciYetenek].yetenekSeviyesi++;
        }

        if (hangiYetenek == "pasif")
        {
            pasifSeviyeler[kacinciYetenek]++;
            pasifYetenekler[kacinciYetenek].yetenekSeviyesi++;
        }

        if (hangiYetenek == "yakin")
        {
            yakinSeviyeler[kacinciYetenek]++;
            yakinYetenekler[kacinciYetenek].yetenekSeviyesi++;
        }

        yetenekleriUygula();
        kaydetKontrolYetenek.jsonYetenekKaydet();
    }

    public void yetenekleriUygula()
    {
        if (menzilliSeviyeler[0] > 0)
        {
            oyuncuSaldiriTest.bonusHasarlarMenzilli += 5f;
        }

        if (menzilliSeviyeler[1] > 0)
        {
            oyuncuSaldiriTest.bonusHasarlarMenzilli += 10f;
        }

        if (menzilliSeviyeler[2] > 0)
        {
            oyuncuSaldiriTest.silah2DayanikliligiBonus = oyuncuSaldiriTest.silah2DayanikliligiBonus * 1.5f;
        }

        if (pasifSeviyeler[0] > 0)
        {
            envanterKontrol.olunceAniMiktariAzalmaYuzdesi = 1.5f;
        }

        if (pasifSeviyeler[1] > 0)
        {
            ozelGuc2KullanmaScripti.ozelGuc2ToplamSure = 7.5f; ozelGuc1KullanmaScripti.ozelGuc1ToplamSure = 7.5f;
        }

        if (pasifSeviyeler[2] > 0)
        {
            envanterKontrol.ejderhaPuaniArtmaMiktari = 75f;
        }

        if (yakinSeviyeler[0] > 0)
        {
            oyuncuSaldiriTest.bonusHasarlarYakin += 5f;
        }

        if (yakinSeviyeler[1] > 0)
        {
            oyuncuSaldiriTest.bonusHasarlarYakin += 10f;
        }

        if (yakinSeviyeler[2] > 0)
        {
            oyuncuSaldiriTest.silah1DayanikliligiBonus = oyuncuSaldiriTest.silah1DayanikliligiBonus * 1.5f;
        }
    }
}
