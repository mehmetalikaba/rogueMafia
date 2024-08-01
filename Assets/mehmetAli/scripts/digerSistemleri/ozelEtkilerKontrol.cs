using UnityEngine;

public class ozelEtkilerKontrol : MonoBehaviour
{
    public GameObject ozelGuc1, ozelGuc2, toplanabilir;
    public kaydetKontrol kaydetKontrol;
    public canKontrol canKontrol;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public envanterKontrol envanterKontrol;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;


    public bool sushi, sashimi, tempura, ramen, udon, yakitori, donburi, miso, takoyaki, okonomiyaki;
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
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
    }
    public void yemekEtkileriniKaydet()
    {
        yemekEtkileri[0] = sushi;
        yemekEtkileri[1] = sashimi;
        yemekEtkileri[2] = tempura;
        yemekEtkileri[3] = ramen;
        yemekEtkileri[4] = udon;
        yemekEtkileri[5] = yakitori;
        yemekEtkileri[6] = donburi;
        yemekEtkileri[7] = miso;
        yemekEtkileri[8] = takoyaki;
        yemekEtkileri[9] = okonomiyaki;

        yemekEtkileriniUygula();
        kaydetKontrol.kaydetKontrolOzelEtkiler.jsonOzelEtkilerKaydet();
    }

    public void yemekEtkileriniYukle()
    {
        sushi = yemekEtkileri[0];
        sashimi = yemekEtkileri[1];
        tempura = yemekEtkileri[2];
        ramen = yemekEtkileri[3];
        udon = yemekEtkileri[4];
        yakitori = yemekEtkileri[5];
        donburi = yemekEtkileri[6];
        miso = yemekEtkileri[7];
        takoyaki = yemekEtkileri[8];
        okonomiyaki = yemekEtkileri[9];
    }

    public void yemekEtkileriniUygula()
    {
        if (sushi)
        {
            canKontrol.baslangicCani += 50;
            if (canKontrol.can < canKontrol.baslangicCani)
                canKontrol.can = canKontrol.baslangicCani;
        }
        if (sashimi)
        {
            oyuncuSaldiriTest.bonusHasarlarYakin *= 1.5f;
            oyuncuSaldiriTest.bonusHasarlarMenzilli *= 1.5f;
        }
        if (tempura)
        {
            oyuncuSaldiriTest.kritikIhtimali += 25;
        }
        if (ramen)
        {
            canKontrol.canAzalmaAzalisi += 5;
        }
        if (udon)
        {
            ozelGuc1KullanmaScripti.ozelGuc1ToplamSure = ozelGuc1KullanmaScripti.ozelGuc1ToplamSure * 0.8f;
            ozelGuc2KullanmaScripti.ozelGuc2ToplamSure = ozelGuc2KullanmaScripti.ozelGuc2ToplamSure * 0.8f;
        }
        if (yakitori)
        {
            oyuncuHareket.hareketHizi += oyuncuHareket.hareketHizi * 0.2f;
        }
        if (donburi)
        {
            canKontrol.olmemeSansiVar = true;
        }
        if (miso)
        {
            oyuncuSaldiriTest.silah1DayanikliligiBonus = oyuncuSaldiriTest.silah2DayanikliligiBonus * 1.5f;
            oyuncuSaldiriTest.silah2DayanikliligiBonus = oyuncuSaldiriTest.silah2DayanikliligiBonus * 1.5f;
        }
        if (takoyaki)
        {
            envanterKontrol.aniArttirmaMiktari += 1;
        }
        if (okonomiyaki)
        {
            canKontrol.iskaSansi += 15;
        }
    }
}


/*


    public void yemekEtkileriniGeriAl()
    {
        /*for (int i = 0; i < kaydetKontrol.yemekEtkileri.Length; i++)
        {
            kaydetKontrol.yemekEtkileri[i] = false;
        }

if (sushi)
{
    sushi = false;
    scriptKontrol.canKontrol.can = 0f;
}
if (sashimi)
{
    sashimi = false;
    scriptKontrol.oyuncuSaldiriTest.sonHasar = 0f;
}
if (tempura)
{
    tempura = false;
    scriptKontrol.oyuncuSaldiriTest.kritikIhtimali = 0f;

}
if (ramen)
{
    ramen = false;
    scriptKontrol.canKontrol.canAzalmaAzalisi = 0f;
}
if (udon)
{
    udon = false;
    scriptKontrol.ozelGuc1KullanmaScripti.ozelGuc1ToplamSure /= 0.8f;
    scriptKontrol.ozelGuc1KullanmaScripti.ozelGuc2ToplamSure /= 0.8f;
}
if (yakitori)
{
    yakitori = false;
    scriptKontrol.oyuncuHareket.hareketHizi /= 1.2f;
}
if (donburi)
{
    donburi = false;
    scriptKontrol.canKontrol.olmemeSansi = false;
}
if (miso)
{
    miso = false;
    for (int i = 0; scriptKontrol.tumSilahlar.Length > 0; i++)
    {
        scriptKontrol.tumSilahlar[i].silahDayanikliligi = 100f;
    }
}
if (takoyaki)
{
    takoyaki = false;
    scriptKontrol.envanterKontrol.aniArttirmaMiktari = 0f;
}
if (okonomiyaki)
{
    okonomiyaki = false;
    scriptKontrol.canKontrol.iskaSansi = 0f;
}
    }
}
        */