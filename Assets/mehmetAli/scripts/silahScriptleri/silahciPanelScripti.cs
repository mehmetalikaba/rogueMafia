using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class silahciPanelScripti : MonoBehaviour
{
    public GameObject oyunPaneli, silahciPaneli, silah1, silah2;
    public List<int> secilenSilahlar = new List<int>();
    public int secilenSilah1, secilenSilah2, secilenSilah3;
    public bool oyuncuYakin, menzilliSecildi, yakinSecildi;
    public Button buton1, buton2, buton3;

    public silahOzellikleri[] butunSilahlar;

    public silahSecimi silahSecimi;
    public silahOzellikleriniGetir silah1OzellikleriniGetir, silah2OzellikleriniGetir;

    oyuncuSaldiriTest oyuncuSaldiriTest;

    public void Start()
    {
        silah1OzellikleriniGetir = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2OzellikleriniGetir = silah2.GetComponent<silahOzellikleriniGetir>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        silahSecimi = new silahSecimi();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
        {
            durdur();
            silahciPaneli.SetActive(true);
            oyunPaneli.SetActive(false);
        }

        if (silahciPaneli.activeSelf)
            randomSilahGetir();

        if (menzilliSecildi && yakinSecildi)
        {
            devamEt();
            silahciPaneli.SetActive(false);
            oyunPaneli.SetActive(true);
            silah1OzellikleriniGetir.seciliSilahinBilgileriniGetir();
            silah2OzellikleriniGetir.seciliSilahinBilgileriniGetir();

            gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    public void randomSilahGetir()
    {
        while (secilenSilahlar.Count < 3)
        {
            int rastgeleSayi = Random.Range(0, butunSilahlar.Length);
            if (!secilenSilahlar.Contains(rastgeleSayi))
            {
                secilenSilahlar.Add(rastgeleSayi);
            }
        }

        secilenSilah1 = secilenSilahlar[0];
        secilenSilah2 = secilenSilahlar[1];
        secilenSilah3 = secilenSilahlar[2];

        buton1.GetComponent<Image>().sprite = butunSilahlar[secilenSilah1].silahIcon;
        buton2.GetComponent<Image>().sprite = butunSilahlar[secilenSilah2].silahIcon;
        buton3.GetComponent<Image>().sprite = butunSilahlar[secilenSilah3].silahIcon;
    }
    public void silahSecimi1()
    {
        silahSecimIslemi(secilenSilah1, buton1);
    }
    public void silahSecimi2()
    {
        silahSecimIslemi(secilenSilah2, buton2);
    }
    public void silahSecimi3()
    {
        silahSecimIslemi(secilenSilah3, buton3);
    }
    public void silahSecimIslemi(int secilenSilah, Button buton)
    {
        if (butunSilahlar[secilenSilah].silahTuru == "menzilli")
        {
            if (!menzilliSecildi)
            {
                silah2OzellikleriniGetir.secilenSilahOzellikleri = butunSilahlar[secilenSilah];
                silah2OzellikleriniGetir.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah];
                oyuncuSaldiriTest.yumruk2 = false;
                menzilliSecildi = true;
                buton.interactable = false;
            }
        }
        else if (butunSilahlar[secilenSilah].silahTuru == "yakin")
        {
            if (!yakinSecildi)
            {
                silah1OzellikleriniGetir.secilenSilahOzellikleri = butunSilahlar[secilenSilah];
                silah1OzellikleriniGetir.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah];
                oyuncuSaldiriTest.yumruk1 = false;
                yakinSecildi = true;
                buton.interactable = false;
            }
        }

    }
    public void durdur()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = false;
    }
}