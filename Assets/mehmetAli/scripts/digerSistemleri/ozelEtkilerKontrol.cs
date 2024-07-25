using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ozelEtkilerKontrol : MonoBehaviour
{

    public scriptKontrol scriptKontrol;


    public bool sushi, sashimi, tempura, ramen, udon, yakitori, donburi, miso, takoyaki, okonomiyaki;

    void Start()
    {
        scriptKontrol = FindObjectOfType<scriptKontrol>();
    }

    void Update()
    {
        if (sushi)
        {
            scriptKontrol.canKontrol.baslangicCani += 10;
        }
        if (sashimi)
        {
            scriptKontrol.oyuncuSaldiriTest.sonHasar += 5;
        }
        if (tempura)
        {
            scriptKontrol.oyuncuSaldiriTest.kritikIhtimali += 5;
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

        }
        if (miso)
        {
            for (int i = 0; scriptKontrol.tumSilahlar.Length > 0; i++)
            {
                scriptKontrol.tumSilahlar[i].silahDayanikliligi += 25;
            }
        }
        if (takoyaki)
        {
            scriptKontrol.envanterKontrol.aniArttirmaMiktari += 1;
        }
        if (okonomiyaki)
        {

        }
    }

    void yemekEtkileriniKapat()
    {
        sushi = false;
        sashimi = false;
        tempura = false;
        ramen = false;
        udon = false;
        yakitori = false;
        donburi = false;
        miso = false;
        takoyaki = false;
        okonomiyaki = false;
    }
}
