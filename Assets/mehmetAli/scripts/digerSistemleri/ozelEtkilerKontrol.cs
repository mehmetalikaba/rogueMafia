using UnityEngine;

public class ozelEtkilerKontrol : MonoBehaviour
{
    public GameObject ozelGuc1, ozelGuc2;
    public kaydetKontrol kaydetKontrol;
    public canKontrol canKontrol;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public envanterKontrol envanterKontrol;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public iksirKullanmaScripti iksirKullanmaScripti;
    public bool[] yemekEtkileri = new bool[10];

    void Start()
    {
        kaydetKontrol = FindObjectOfType<kaydetKontrol>();
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        ozelGuc1KullanmaScripti = ozelGuc1.GetComponent<ozelGucKullanmaScripti>();
        ozelGuc2KullanmaScripti = ozelGuc2.GetComponent<ozelGucKullanmaScripti>();
        iksirKullanmaScripti = FindObjectOfType<iksirKullanmaScripti>();
    }

    public void yemekEtkileriniUygula()
    {
        if (yemekEtkileri[0]) // sushi
        {
            canKontrol.baslangicCani += 50;
            canKontrol.can += 50;
        }
        if (yemekEtkileri[1]) // sashimi
        {
            oyuncuSaldiriTest.bonusHasarlarYakin = 1.5f;
            oyuncuSaldiriTest.bonusHasarlarMenzilli = 1.5f;
            oyuncuSaldiriTest.sashimiYedi = true;
        }
        if (yemekEtkileri[2]) // tempura
        {
            oyuncuSaldiriTest.kritikHasari = 1.5f;
            oyuncuSaldiriTest.kritikIhtimali += 25;
            oyuncuSaldiriTest.tempuraYedi = true;
        }
        if (yemekEtkileri[3]) // ramen
        {
            canKontrol.canAzalmaAzalisi += 5;
        }
        if (yemekEtkileri[4]) // udon
        {
            ozelGuc1KullanmaScripti.ozelGuc1ToplamSure = ozelGuc1KullanmaScripti.ozelGuc1ToplamSure * 0.8f;
            ozelGuc2KullanmaScripti.ozelGuc2ToplamSure = ozelGuc2KullanmaScripti.ozelGuc2ToplamSure * 0.8f;
        }
        if (yemekEtkileri[5]) // yakitori
        {
            oyuncuHareket.hareketHiziBonus = 1.25f;
        }
        if (yemekEtkileri[6]) // donburi
        {
            canKontrol.olmemeSansiVar = true;
            canKontrol.kacOlmemeSansi++;
        }
        if (yemekEtkileri[7]) // miso
        {
            oyuncuSaldiriTest.silah1DayanikliligiBonus = oyuncuSaldiriTest.silah2DayanikliligiBonus * 1.5f;
            oyuncuSaldiriTest.silah2DayanikliligiBonus = oyuncuSaldiriTest.silah2DayanikliligiBonus * 1.5f;
        }
        if (yemekEtkileri[8]) // takoyaki
        {
            envanterKontrol.aniArttirmaMiktari += 1;
        }
        if (yemekEtkileri[9]) // okonomiyaki
        {
            canKontrol.iskaSansi += 15;
        }
        if (yemekEtkileri[10]) //  gyoza
        {
            // gerekli olanlar dusman hasarda
        }
        if (yemekEtkileri[11]) //  onigiri
        {
            // gerekli olanlar dusman hasarda
        }
        if (yemekEtkileri[12]) //  soba
        {
            // gerekli olanlar dusman hasarda
        }
        if (yemekEtkileri[13]) //  kasutera
        {
            // gerekli olanlar dusman hasarda
        }
        if (yemekEtkileri[14]) //  nigiri
        {
            // gerekli olanlar dusman hasarda
        }


        kaydetKontrol.kaydetKontrolOzelEtkiler.jsonOzelEtkilerKaydet();
    }
}