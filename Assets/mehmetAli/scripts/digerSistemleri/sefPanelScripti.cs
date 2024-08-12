using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class sefPanelScripti : MonoBehaviour
{
    public Text fiyat1, fiyat2, fiyat3;
    public bool oyuncuYakin, yemekSecti, yemekUcretsiz, etkilesimKilitli;
    public int secilenYemek1, secilenYemek2, secilenYemek3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, sefPaneli;
    public Text ejderParasi, aciklamaText, yemek1Adi, yemek2Adi, yemek3Adi, sefDiyalog;
    public yemekOzellikleri[] yemekler;
    public Image[] yemekGorselleri;
    public mouseUzerindeMi buton1Kontrol, buton2Kontrol, buton3Kontrol;
    public envanterKontrol envanterKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public DuraklatmaMenusu duraklatmaMenusu;
    public oyuncuHareket oyuncuHareket;
    public string eksikMetni;
    public List<int> secilenYemekler = new List<int>();
    LocalizationManager localizationManager;
    public AudioSource yemekAlma;


    public void Start()
    {
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        localizationManager = FindObjectOfType<LocalizationManager>();
        eksikMetni = localizationManager.GetLocalizedValue("eksik_key");
        ejderParasi.text = envanterKontrol.ejderParasi.ToString();
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !sefPaneli.activeSelf)
                durdur();
            else if (oyuncuYakin && sefPaneli.activeSelf)
                devamEt();
        }

        if (buton1Kontrol.mouseUzerinde)
            aciklamaText.GetComponent<localizedText>().key = yemekler[secilenYemek1].yemekAciklamaKeyi;
        if (buton2Kontrol.mouseUzerinde)
            aciklamaText.GetComponent<localizedText>().key = yemekler[secilenYemek2].yemekAciklamaKeyi;
        if (buton3Kontrol.mouseUzerinde)
            aciklamaText.GetComponent<localizedText>().key = yemekler[secilenYemek3].yemekAciklamaKeyi;

    }

    public void randomYemekGetir()
    {
        while (secilenYemekler.Count < 3)
        {
            int rastgeleSayi = Random.Range(0, yemekler.Length);
            if (!secilenYemekler.Contains(rastgeleSayi))
            {
                secilenYemekler.Add(rastgeleSayi);
            }
        }

        secilenYemek1 = secilenYemekler[0];
        secilenYemek2 = secilenYemekler[1];
        secilenYemek3 = secilenYemekler[2];

        fiyat1.text = yemekler[secilenYemek1].yemekFiyati.ToString();
        fiyat2.text = yemekler[secilenYemek2].yemekFiyati.ToString();
        fiyat3.text = yemekler[secilenYemek3].yemekFiyati.ToString();

        yemekGorselleri[0].sprite = yemekler[secilenYemek1].yemekSprite;
        yemekGorselleri[1].sprite = yemekler[secilenYemek2].yemekSprite;
        yemekGorselleri[2].sprite = yemekler[secilenYemek3].yemekSprite;

        yemek1Adi.text = yemekler[secilenYemek1].yemekAdi;
        yemek2Adi.text = yemekler[secilenYemek2].yemekAdi;
        yemek3Adi.text = yemekler[secilenYemek3].yemekAdi;
    }

    public void yemekSecimi1()
    {
        if (envanterKontrol.ejderParasi >= yemekler[secilenYemek1].yemekFiyati || yemekUcretsiz)
        {
            yemekGorselleri[0].enabled = false;
            fiyat1.text = "";
            yemek1Adi.text = "";
            yemekSecimIslemi(secilenYemek1, buton1);
        }
        else
            aciklamaText.text = yemekler[secilenYemek1].yemekFiyati - envanterKontrol.ejderParasi + eksikMetni;
    }
    public void yemekSecimi2()
    {
        if (envanterKontrol.ejderParasi >= yemekler[secilenYemek2].yemekFiyati || yemekUcretsiz)
        {
            yemekGorselleri[1].enabled = false;
            fiyat2.text = "";
            yemek2Adi.text = "";
            yemekSecimIslemi(secilenYemek2, buton2);
        }
        else
            aciklamaText.text = yemekler[secilenYemek2].yemekFiyati - envanterKontrol.ejderParasi + eksikMetni;
    }
    public void yemekSecimi3()
    {
        if (envanterKontrol.ejderParasi >= yemekler[secilenYemek3].yemekFiyati || yemekUcretsiz)
        {
            yemekGorselleri[2].enabled = false;
            fiyat3.text = "";
            yemek3Adi.text = "";
            yemekSecimIslemi(secilenYemek3, buton3);
        }
        else
            aciklamaText.text = yemekler[secilenYemek3].yemekFiyati - envanterKontrol.ejderParasi + eksikMetni;
    }
    public void yemekSecimIslemi(int secilenYemek, Button buton)
    {
        ozelEtkilerKontrol.yemekEtkileri[secilenYemek] = true;

        yemekSecti = true;
        Destroy(buton.GetComponent<mouseUzerindeMi>());
        if (!yemekUcretsiz)
            envanterKontrol.ejderParasi -= yemekler[secilenYemek].yemekFiyati;
        yemekAlma.Play();
        aciklamaText.GetComponent<localizedText>().key = "";
        aciklamaText.text = "";
        ejderParasi.text = envanterKontrol.ejderParasi.ToString();
    }
    public void durdur()
    {
        if (!yemekSecti)
            randomYemekGetir();

        ejderParasi.text = envanterKontrol.ejderParasi.ToString();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sefPaneli.SetActive(true);
        oyunPaneli.SetActive(false);
        duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        sefPaneli.SetActive(false);
        oyunPaneli.SetActive(true);
        duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
    }
}
