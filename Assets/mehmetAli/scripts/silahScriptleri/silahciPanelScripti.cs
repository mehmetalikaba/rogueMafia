using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class silahciPanelScripti : MonoBehaviour
{
    public GameObject daireButon;
    public bool oyuncuYakin, menzilliSecildi, yakinSecildi, randomSilahlarGeldi, etkilesimKilitli;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, silahciPaneli, silah1, silah2;
    public Text aciklamaText, silah1Adi, silah2Adi, silah3Adi, silah1Ozellik, silah2Ozellik, silah3Ozellik, silahciDiyalog;
    public Image silah1Image, silah2Image, silah3Image;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public silahOzellikleri[] menzilliler, yakinlar;
    public List<silahOzellikleri> secilenSilahlar = new List<silahOzellikleri>();
    public araBaseKontrol araBaseKontrol;
    public anaBaseKontrol anaBaseKontrol;
    public DuraklatmaMenusu duraklatmaMenusu;
    public oyuncuHareket oyuncuHareket;
    public localizedText silah1Aciklama, silah2Aciklama, silah3Aciklama;

    public void Start()
    {
        araBaseKontrol = FindObjectOfType<araBaseKontrol>();
        anaBaseKontrol = FindObjectOfType<anaBaseKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        aciklamaText.GetComponent<localizedText>().key = "secim1_key";
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
            daireButon.SetActive(true);
        else if (!oyuncuYakin)
            daireButon.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !silahciPaneli.activeSelf)
                durdur();
            else if (oyuncuYakin && silahciPaneli.activeSelf)
                devamEt();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !silahciPaneli.activeSelf)
                durdur();
            else if (oyuncuYakin && silahciPaneli.activeSelf)
                devamEt();
        }
    }

    public void randomSilahGetir()
    {
        randomSilahlarGeldi = true;
        secilenSilahlar.Clear();

        int menzilliAdedi = Random.Range(1, 3);
        int yakinAdedi = 3 - menzilliAdedi;

        List<silahOzellikleri> menzilliSecimler = new List<silahOzellikleri>();
        List<silahOzellikleri> yakinSecimler = new List<silahOzellikleri>();

        while (menzilliSecimler.Count < menzilliAdedi)
        {
            int rastgeleSayi = Random.Range(0, menzilliler.Length);
            if (!menzilliSecimler.Contains(menzilliler[rastgeleSayi]))
                menzilliSecimler.Add(menzilliler[rastgeleSayi]);
        }

        while (yakinSecimler.Count < yakinAdedi)
        {
            int rastgeleSayi = Random.Range(0, yakinlar.Length);
            if (!yakinSecimler.Contains(yakinlar[rastgeleSayi]))
                yakinSecimler.Add(yakinlar[rastgeleSayi]);
        }

        secilenSilahlar.AddRange(menzilliSecimler);
        secilenSilahlar.AddRange(yakinSecimler);

        silah1Image.sprite = secilenSilahlar[0].silahIcon;
        silah2Image.sprite = secilenSilahlar[1].silahIcon;
        silah3Image.sprite = secilenSilahlar[2].silahIcon;

        silah1Adi.text = secilenSilahlar[0].silahAdi;
        silah2Adi.text = secilenSilahlar[1].silahAdi;
        silah3Adi.text = secilenSilahlar[2].silahAdi;

        silah1Aciklama.key = secilenSilahlar[0].aciklamaKeyi;
        silah2Aciklama.key = secilenSilahlar[1].aciklamaKeyi;
        silah3Aciklama.key = secilenSilahlar[2].aciklamaKeyi;

        silah1Ozellik.text = secilenSilahlar[0].silahSaldiriHasari.ToString() + "\n~Hasar~";
        silah2Ozellik.text = secilenSilahlar[1].silahSaldiriHasari.ToString() + "\n~Hasar~";
        silah3Ozellik.text = secilenSilahlar[2].silahSaldiriHasari.ToString() + "\n~Hasar~";
    }
    public void silahSecimi1()
    {
        silahSecimIslemi(secilenSilahlar[0], buton1);
    }
    public void silahSecimi2()
    {
        silahSecimIslemi(secilenSilahlar[1], buton2);
    }
    public void silahSecimi3()
    {
        silahSecimIslemi(secilenSilahlar[2], buton3);
    }
    public void silahSecimIslemi(silahOzellikleri secilenSilah, Button buton)
    {
        if (secilenSilah.silahTuru == "menzilli")
        {
            if (!menzilliSecildi)
            {
                aciklamaText.GetComponent<localizedText>().key = "secim2_key";
                silah2.GetComponent<silahOzellikleriniGetir>().elindekiSilah = secilenSilah;
                menzilliSecildi = true;
                buton.interactable = false;
            }
            else
                aciklamaText.GetComponent<localizedText>().key = "menzilliSecili_key";
        }
        else if (secilenSilah.silahTuru == "yakin")
        {
            if (!yakinSecildi)
            {
                aciklamaText.GetComponent<localizedText>().key = "secim2_key";
                silah1.GetComponent<silahOzellikleriniGetir>().elindekiSilah = secilenSilah;
                yakinSecildi = true;
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
        //duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        silahciPaneli.SetActive(false);
        oyunPaneli.SetActive(true);
        //duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
        if (menzilliSecildi && yakinSecildi)
        {
            if (anaBaseKontrol != null)
                anaBaseKontrol.silahciKonustu = true;
            if (araBaseKontrol != null)
                araBaseKontrol.silahciKonustu = true;
            silahciDiyalog.GetComponent<localizedText>().key = "silahci_bitti";
            this.enabled = false;
        }
    }
}