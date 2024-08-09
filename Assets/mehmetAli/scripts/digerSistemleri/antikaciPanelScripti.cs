using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class antikaciPanelScripti : MonoBehaviour
{
    public bool oyuncuYakin, randomAntikaGeldi, etkilesimKilitli, antikaSecildi;
    public List<int> secilenAntikalar = new List<int>();
    public antikaYadigarOzellikleri[] antikaObjeleri;
    public antikaYadigarOzellikleri[] yadigarObjeleri;
    public int secilenAntika1, secilenAntika2, secilenAntika3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, antikaciPanel;
    public Text aciklamaText, antikaAdi1, antikaAdi2, antikaAdi3, antikaciDiyalog;
    public araBaseKontrol araBaseKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public DuraklatmaMenusu duraklatmaMenusu;
    public oyuncuHareket oyuncuHareket;

    public void Start()
    {
        araBaseKontrol = FindObjectOfType<araBaseKontrol>();
        aciklamaText.GetComponent<localizedText>().key = "secim1_key";
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !antikaciPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && antikaciPanel.activeSelf)
                devamEt();
        }
    }

    public void randomAntikaGetir()
    {
        randomAntikaGeldi = true;
        while (secilenAntikalar.Count < 3)
        {
            int rastgeleSayi = Random.Range(0, antikaObjeleri.Length);
            if (!secilenAntikalar.Contains(rastgeleSayi))
            {
                secilenAntikalar.Add(rastgeleSayi);
            }
        }

        secilenAntika1 = secilenAntikalar[0];
        secilenAntika2 = secilenAntikalar[1];
        secilenAntika3 = secilenAntikalar[2];

        antikaAdi1.GetComponent<localizedText>().key = antikaObjeleri[secilenAntika1].antikaAdi;
        antikaAdi2.GetComponent<localizedText>().key = antikaObjeleri[secilenAntika2].antikaAdi;
        antikaAdi3.GetComponent<localizedText>().key = antikaObjeleri[secilenAntika3].antikaAdi;

        buton1.GetComponent<Image>().sprite = antikaObjeleri[secilenAntika1].antikaIcon;
        buton2.GetComponent<Image>().sprite = antikaObjeleri[secilenAntika2].antikaIcon;
        buton3.GetComponent<Image>().sprite = antikaObjeleri[secilenAntika3].antikaIcon;
    }
    public void antikaSecimButonu1()
    {
        antikaSecimIslemi(secilenAntika1, buton1);
    }
    public void antikaSecimButonu2()
    {
        antikaSecimIslemi(secilenAntika2, buton2);
    }
    public void antikaSecimButonu3()
    {
        antikaSecimIslemi(secilenAntika3, buton3);
    }
    public void antikaSecimIslemi(int secilenOzelGuc, Button buton)
    {
        if (!antikaSecildi)
        {
            aciklamaText.GetComponent<localizedText>().key = "secim2_key";
        }
        else
        {
            aciklamaText.GetComponent<localizedText>().key = "secim2_key";
        }
        buton.interactable = false;

        if (antikaSecildi && antikaSecildi)
            devamEt();
    }
    public void durdur()
    {
        duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
        if (!randomAntikaGeldi)
            randomAntikaGetir();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        antikaciPanel.SetActive(true);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        antikaciPanel.SetActive(false);
        oyunPaneli.SetActive(true);
        duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
        if (antikaSecildi && antikaSecildi)
        {
            if (araBaseKontrol != null)
                araBaseKontrol.alfredKonustu = true;

            antikaciDiyalog.GetComponent<localizedText>().key = "alfred_bitti";
            this.enabled = false;
        }
    }
}
