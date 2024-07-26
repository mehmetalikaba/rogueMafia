using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ozelEtkilerKontrol : MonoBehaviour
{

    public scriptKontrol scriptKontrol;
    public kaydetKontrol kaydetKontrol;
    public GameObject silah1;

    public bool sushi, sashimi, tempura, ramen, udon, yakitori, donburi, miso, takoyaki, okonomiyaki;

    void Start()
    {
        scriptKontrol = FindObjectOfType<scriptKontrol>();
        silah1 = GameObject.Find("silah1");
    }
    public void yemekEtkileriniKaydet()
    {
        kaydetKontrol.yemekEtkileri[0] = sushi;
        kaydetKontrol.yemekEtkileri[1] = sashimi;
        kaydetKontrol.yemekEtkileri[2] = tempura;
        kaydetKontrol.yemekEtkileri[3] = ramen;
        kaydetKontrol.yemekEtkileri[4] = udon;
        kaydetKontrol.yemekEtkileri[5] = yakitori;
        kaydetKontrol.yemekEtkileri[6] = donburi;
        kaydetKontrol.yemekEtkileri[7] = miso;
        kaydetKontrol.yemekEtkileri[8] = takoyaki;
        kaydetKontrol.yemekEtkileri[9] = okonomiyaki;
    }
    public void yemekEtkileriniYukle()
    {
        sushi = kaydetKontrol.yemekEtkileri[0];
        sashimi = kaydetKontrol.yemekEtkileri[1];
        tempura = kaydetKontrol.yemekEtkileri[2];
        ramen = kaydetKontrol.yemekEtkileri[3];
        udon = kaydetKontrol.yemekEtkileri[4];
        yakitori = kaydetKontrol.yemekEtkileri[5];
        donburi = kaydetKontrol.yemekEtkileri[6];
        miso = kaydetKontrol.yemekEtkileri[7];
        takoyaki = kaydetKontrol.yemekEtkileri[8];
        okonomiyaki = kaydetKontrol.yemekEtkileri[9];
    }
    public void yemekEtkileriniUygula()
    {
        if (sushi)
        {
            scriptKontrol.canKontrol.can += 10;
        }
        if (sashimi)
        {
            scriptKontrol.oyuncuSaldiriTest.sonHasar += 5;
        }
        if (tempura)
        {
            scriptKontrol.oyuncuSaldiriTest.kritikIhtimali += 25;
        }
        if (ramen)
        {
            scriptKontrol.canKontrol.canAzalmaAzalisi += 5;
        }
        if (udon)
        {
            scriptKontrol.ozelGuc1KullanmaScripti.ozelGuc1ToplamSure -= scriptKontrol.ozelGuc1KullanmaScripti.ozelGuc1ToplamSure * 0.2f;
            scriptKontrol.ozelGuc1KullanmaScripti.ozelGuc2ToplamSure -= scriptKontrol.ozelGuc1KullanmaScripti.ozelGuc2ToplamSure * 0.2f;
        }
        if (yakitori)
        {
            scriptKontrol.oyuncuHareket.hareketHizi += scriptKontrol.oyuncuHareket.hareketHizi * 0.2f;
        }
        if (donburi)
        {
            scriptKontrol.canKontrol.olmemeSansi = true;
        }
        if (miso)
        {
            for (int i = 0; scriptKontrol.tumSilahlar.Length > 0; i++)
            {
                silah1.GetComponent<silahOzellikleriniGetir>().butunSilahlarDizisi[i].silahDayanikliligi += 25;
            }
        }
        if (takoyaki)
        {
            scriptKontrol.envanterKontrol.aniArttirmaMiktari += 1;
        }
        if (okonomiyaki)
        {
            scriptKontrol.canKontrol.iskaSansi += 15;
        }
    }

    public void yemekEtkileriniGeriAl()
    {
        for (int i = 0; i < kaydetKontrol.yemekEtkileri.Length; i++)
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
