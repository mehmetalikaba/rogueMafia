using Unity.VisualScripting;
using UnityEngine;

public class araBirimKontrol : MonoBehaviour
{
    public bool heykel, kapuson, sandik;
    public bool aldiMi, iksirAldi;
    public asamaKontrol[] kontrol;
    public asamaKontrol asamaKontrol;
    public GameObject maviFx, yesilFx, alamadiFx, iksir, silah;
    public silahOzellikleri[] silahlar;
    public canKontrol canKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public envanterKontrol envanterKontrol;
    public rastgeleDusenIksir rastgeleDusenIksir;

    void Start()
    {
        asamaKontrol = GetComponent<asamaKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        if (asamaKontrol.oyuncuGeldi)
            oyuncuHareket.yavaslat = true;
        else
            oyuncuHareket.yavaslat = false;

        rastgeleDusenIksir = FindObjectOfType<rastgeleDusenIksir>();

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
        {
            if (!aldiMi)
            {
                aldiMi = true;
                if (sandik)
                    sandikBirim();
                else if (heykel)
                    heykelBirim();
                if (kapuson)
                    kapusonBirim();
            }
            if (kapuson && !iksirAldi && rastgeleDusenIksir == null)
                kapusonBirimSatici();
        }
    }

    public void sandikBirim()
    {
        int randomSayi = Random.Range(1, 3);
        if (randomSayi == 1)
        {
            Instantiate(iksir, kontrol[2].transform.position, Quaternion.identity);
        }
        if (randomSayi == 2)
        {
            int ranSayi = Random.Range(1, silahlar.Length);
            silah.GetComponent<rastgeleDusenSilah>().dusenSilah = silahlar[ranSayi];
            Vector3 yeniPozisyon = kontrol[2].transform.position + new Vector3(2f, 0f, 0f);
            Instantiate(silah, yeniPozisyon, Quaternion.identity);
        }
    }
    public void kapusonBirim()
    {
        if (kontrol[0].oyuncuGeldi)
        {
            Instantiate(maviFx, kontrol[0].transform.position, Quaternion.identity);
            oyuncuHareket.hareketHiziBonus += 1.5f;
        }
        if (kontrol[1].oyuncuGeldi)
        {
            Instantiate(yesilFx, kontrol[1].transform.position, Quaternion.identity);
            oyuncuHareket.ziplamaSayisi += 1;
        }

    }
    public void kapusonBirimSatici()
    {
        iksirAldi = true;
        if (kontrol[2].oyuncuGeldi)
        {
            envanterKontrol = FindObjectOfType<envanterKontrol>();
            if (envanterKontrol.ejderParasi > 250)
            {
                envanterKontrol.ejderParasi -= 250;
                Instantiate(iksir, kontrol[2].transform.position, Quaternion.identity);
            }
            else
                Instantiate(alamadiFx, kontrol[2].transform.position, Quaternion.identity);
        }
        iksirAldi = false;
    }
    public void heykelBirim()
    {
        if (kontrol[0].oyuncuGeldi)
        {
            Instantiate(maviFx, kontrol[0].transform.position, Quaternion.identity);
            oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
            oyuncuSaldiriTest.silah1Script.silahDayanikliligi = 100f;
            oyuncuSaldiriTest.silah2Script.silahDayanikliligi = 100f;
            oyuncuSaldiriTest.bonusHasarlarMenzilli += 1.5f;
            oyuncuSaldiriTest.bonusHasarlarYakin += 1.5f;
        }
        else if (kontrol[1].oyuncuGeldi)
        {
            Instantiate(yesilFx, kontrol[1].transform.position, Quaternion.identity);
            canKontrol = FindObjectOfType<canKontrol>();
            canKontrol.baslangicCani += 25f;
            canKontrol.can = canKontrol.baslangicCani;
        }
    }
}
