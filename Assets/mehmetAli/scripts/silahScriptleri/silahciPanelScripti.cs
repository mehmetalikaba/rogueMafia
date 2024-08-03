using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class silahciPanelScripti : MonoBehaviour
{
    public bool oyuncuYakin, menzilliSecildi, yakinSecildi, randomSilahlarGeldi, etkilesimKilitli;
    public int secilenSilah1, secilenSilah2, secilenSilah3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, silahciPaneli, silah1, silah2;
    public Text aciklamaText, silah1Adi, silah2Adi, silah3Adi, silahciDiyalog;
    public silahSecimi silahSecimi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public silahOzellikleri[] butunSilahlar;
    public List<int> secilenSilahlar = new List<int>();
    public araBaseKontrol araBaseKontrol;
    public DuraklatmaMenusu duraklatmaMenusu;

    public void Start()
    {
        araBaseKontrol = FindObjectOfType<araBaseKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        aciklamaText.GetComponent<localizedText>().key = "secim1_key";
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !silahciPaneli.activeSelf)
                durdur();
            else if (oyuncuYakin && silahciPaneli.activeSelf)
                devamEt();
            else
                silahciDiyalog.GetComponent<localizedText>().key = "silahci_bitti";
        }
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
                aciklamaText.GetComponent<localizedText>().key = "secim2_key";
                silah2.GetComponent<silahOzellikleriniGetir>().secilenSilahOzellikleri = butunSilahlar[secilenSilah];
                silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah];
                oyuncuSaldiriTest.yumruk2 = false;
                menzilliSecildi = true;
                silah2.GetComponent<silahOzellikleriniGetir>().seciliSilahinBilgileriniGetir();
                buton.interactable = false;
            }
            else
                aciklamaText.GetComponent<localizedText>().key = "menzilliSecili_key";
        }
        else if (butunSilahlar[secilenSilah].silahTuru == "yakin")
        {
            if (!yakinSecildi)
            {
                aciklamaText.GetComponent<localizedText>().key = "secim2_key";
                silah1.GetComponent<silahOzellikleriniGetir>().secilenSilahOzellikleri = butunSilahlar[secilenSilah];
                silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah];
                oyuncuSaldiriTest.yumruk1 = false;
                yakinSecildi = true;
                silah1.GetComponent<silahOzellikleriniGetir>().seciliSilahinBilgileriniGetir();
                buton.interactable = false;
            }
            else
                aciklamaText.GetComponent<localizedText>().key = "yakinSecili_key";
        }

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
        duraklatmaMenusu.duraklatmaKilitli = true;
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        silahciPaneli.SetActive(false);
        oyunPaneli.SetActive(true);
        duraklatmaMenusu.duraklatmaKilitli = false;
        if (menzilliSecildi && yakinSecildi)
        {
            if (araBaseKontrol != null)
                araBaseKontrol.silahciKonustu = true;

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