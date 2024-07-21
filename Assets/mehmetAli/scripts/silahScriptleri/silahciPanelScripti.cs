using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class silahciPanelScripti : MonoBehaviour
{
    public bool oyuncuYakin, menzilliSecildi, yakinSecildi, randomSilahlarGeldi;
    public int secilenSilah1, secilenSilah2, secilenSilah3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, silahciPaneli, silah1, silah2;
    public TextMeshProUGUI aciklamaText, silah1Adi, silah2Adi, silah3Adi;
    public silahSecimi silahSecimi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public silahOzellikleri[] butunSilahlar;
    public List<int> secilenSilahlar = new List<int>();

    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !silahciPaneli.activeSelf)
            durdur();
        else if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && silahciPaneli.activeSelf)
            devamEt();
    }

    public void randomSilahGetir()
    {
        randomSilahlarGeldi = true;
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

        silah1Adi.text = butunSilahlar[secilenSilah1].silahAdi;
        silah2Adi.text = butunSilahlar[secilenSilah2].silahAdi;
        silah3Adi.text = butunSilahlar[secilenSilah3].silahAdi;
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
                aciklamaText.text = "Ikinci secimini yap";
                silah2.GetComponent<silahOzellikleriniGetir>().secilenSilahOzellikleri = butunSilahlar[secilenSilah];
                silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah];
                oyuncuSaldiriTest.yumruk2 = false;
                menzilliSecildi = true;
                silah2.GetComponent<silahOzellikleriniGetir>().seciliSilahinBilgileriniGetir();
            }
            else
                aciklamaText.text = "Menzilli silahini zaten sectin";
        }
        else if (butunSilahlar[secilenSilah].silahTuru == "yakin")
        {
            if (!yakinSecildi)
            {
                aciklamaText.text = "Ikinci secimini yap";
                silah1.GetComponent<silahOzellikleriniGetir>().secilenSilahOzellikleri = butunSilahlar[secilenSilah];
                silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah];
                oyuncuSaldiriTest.yumruk1 = false;
                yakinSecildi = true;
                silah1.GetComponent<silahOzellikleriniGetir>().seciliSilahinBilgileriniGetir();
            }
            else
                aciklamaText.text = "Yakin silahini zaten sectin";
        }

        buton.interactable = false;
        if (menzilliSecildi && yakinSecildi)
            devamEt();
    }
    public void durdur()
    {
        if (!randomSilahlarGeldi)
            randomSilahGetir();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        silahciPaneli.SetActive(true);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        silahciPaneli.SetActive(false);
        oyunPaneli.SetActive(true);
        if (menzilliSecildi && yakinSecildi)
        {
            gameObject.SetActive(false);
            this.enabled = false;
        }
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