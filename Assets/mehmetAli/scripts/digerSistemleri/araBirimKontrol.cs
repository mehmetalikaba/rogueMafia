using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class araBirimKontrol : MonoBehaviour
{
    public bool heykel, kapuson, sandik;
    public bool aldiMi;
    public asamaKontrol[] kontrol;
    public asamaKontrol asamaKontrol;
    public GameObject maviFx, yesilFx, alamadiFx, antika, iksir, silah, yadigar, yemek, isik, isik2;
    public silahOzellikleri[] silahlar;
    public canKontrol canKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public envanterKontrol envanterKontrol;
    public rastgeleDusenIksir rastgeleDusenIksir;
    public Animator animator;

    void Start()
    {
        asamaKontrol = GetComponent<asamaKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        rastgeleDusenIksir = FindObjectOfType<rastgeleDusenIksir>();

        if (sandik && kontrol[0].oyuncuGeldi && !aldiMi)
            isik.SetActive(true);
        else
        {
            if (isik.activeSelf)
                isik.SetActive(false);
        }
        if (kapuson)
        {
            if (!aldiMi)
            {
                if (kontrol[0].oyuncuGeldi)
                {
                    isik.SetActive(true);
                    isik2.SetActive(false);
                }
                if (kontrol[1].oyuncuGeldi)
                {
                    isik.SetActive(false);
                    isik2.SetActive(true);
                }
                else if (!kontrol[0].oyuncuGeldi && !kontrol[1].oyuncuGeldi)
                {
                    isik.SetActive(false);
                    isik2.SetActive(false);
                }
            }
        }
        if (heykel)
        {
            if (!aldiMi)
            {
                if (kontrol[0].oyuncuGeldi)
                {
                    isik.SetActive(true);
                    isik2.SetActive(false);
                }
                if (kontrol[1].oyuncuGeldi)
                {
                    isik.SetActive(false);
                    isik2.SetActive(true);
                }
                else if (!kontrol[0].oyuncuGeldi && !kontrol[1].oyuncuGeldi)
                {
                    isik.SetActive(false);
                    isik2.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            if (!aldiMi)
            {
                if (sandik)
                {
                    if (kontrol[0].oyuncuGeldi)
                        StartCoroutine(sandikBirim());
                }
                else if (heykel)
                    heykelBirim();
                if (kapuson)
                    kapusonBirim();
            }
            if (kapuson && rastgeleDusenIksir == null)
                kapusonBirimSatici();
        }
    }

    IEnumerator sandikBirim()
    {
        aldiMi = true;
        isik.SetActive(false);
        animator.SetTrigger("acil");
        Instantiate(maviFx, kontrol[0].transform.position, Quaternion.identity);
        for (int i = 0; i < 2; i++)
        {
            int randomSayi = Random.Range(1, 5);
            if (randomSayi == 1)
            {
                Instantiate(antika, kontrol[0].transform.position, Quaternion.identity);
            }
            if (randomSayi == 2)
            {
                Instantiate(iksir, kontrol[0].transform.position, Quaternion.identity);
            }
            if (randomSayi == 3)
            {
                int ranSayi = Random.Range(1, silahlar.Length);
                silah.GetComponent<rastgeleDusenSilah>().dusenSilah = silahlar[ranSayi];
                Vector3 yeniPozisyon = kontrol[0].transform.position + new Vector3(2f, 0f, 0f);
                Instantiate(silah, yeniPozisyon, Quaternion.identity);
            }
            if (randomSayi == 4)
            {
                Instantiate(yemek, kontrol[0].transform.position, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(0.1f);
        isik2.SetActive(true);
    }
    public void kapusonBirim()
    {
        if (kontrol[0].oyuncuGeldi)
        {
            aldiMi = true;
            Instantiate(maviFx, kontrol[0].transform.position, Quaternion.identity);
            oyuncuHareket.hareketHiziBonus += 0.25f;
        }
        if (kontrol[1].oyuncuGeldi)
        {
            aldiMi = true;
            Instantiate(yesilFx, kontrol[1].transform.position, Quaternion.identity);
            oyuncuHareket.ziplamaGucuBonus += 0.25f;
        }

    }
    public void kapusonBirimSatici()
    {
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
    }
    public void heykelBirim()
    {
        if (kontrol[0].oyuncuGeldi)
        {
            aldiMi = true;
            Instantiate(maviFx, kontrol[0].transform.position, Quaternion.identity);
            oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
            oyuncuSaldiriTest.silah1Script.silahDayanikliligi = 100f;
            oyuncuSaldiriTest.silah2Script.silahDayanikliligi = 100f;
            oyuncuSaldiriTest.bonusHasarlarMenzilli += 1.5f;
            oyuncuSaldiriTest.bonusHasarlarYakin += 1.5f;
        }
        else if (kontrol[1].oyuncuGeldi)
        {
            aldiMi = true;
            Instantiate(yesilFx, kontrol[1].transform.position, Quaternion.identity);
            canKontrol = FindObjectOfType<canKontrol>();
            canKontrol.baslangicCani += 25f;
            canKontrol.can = canKontrol.baslangicCani;
        }
    }
}
