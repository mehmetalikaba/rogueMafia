using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sefPanelScripti : MonoBehaviour
{
    public Text fiyat1, fiyat2,fiyat3;
    public bool oyuncuYakin, yemekSecti, yemekUcretsiz, etkilesimKilitli;
    public int secilenYemek1, secilenYemek2, secilenYemek3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, sefPaneli;
    public Text ejderParasi, welcomeText, aciklamaText, yemek1Adi, yemek2Adi, yemek3Adi, sefDiyalog;
    public yemekOzellikleri[] yemekler;
    public scriptKontrol scriptKontrol;
    public Image[] yemekGorselleri;

    public string eksikMetni;

    public List<int> secilenYemekler = new List<int>();

    LocalizationManager localizationManager;

    public localizedText[] yemekAciklamalari;

    public void Start()
    {
        localizationManager = FindObjectOfType<LocalizationManager>();
        eksikMetni = localizationManager.GetLocalizedValue("eksik_key");
        ejderParasi.text = scriptKontrol.envanterKontrol.ejderParasi.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !sefPaneli.activeSelf)
                durdur();
            else if (oyuncuYakin && sefPaneli.activeSelf)
                devamEt();
        }
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

        yemekAciklamalari[0].key = yemekler[secilenYemek1].yemekAciklamaKeyi;
        yemekAciklamalari[1].key = yemekler[secilenYemek2].yemekAciklamaKeyi;
        yemekAciklamalari[2].key = yemekler[secilenYemek3].yemekAciklamaKeyi;

    }
    public void yemekSecimi1()
    {
        if (scriptKontrol.envanterKontrol.ejderParasi > yemekler[secilenYemek1].yemekFiyati)
            yemekSecimIslemi(secilenYemek1, buton1);
        else if (yemekUcretsiz)
            yemekSecimIslemi(secilenYemek1, buton1);
        else
            aciklamaText.text = yemekler[secilenYemek1].yemekFiyati - scriptKontrol.envanterKontrol.ejderParasi + eksikMetni;
    }
    public void yemekSecimi2()
    {
        if (scriptKontrol.envanterKontrol.ejderParasi > yemekler[secilenYemek2].yemekFiyati)
            yemekSecimIslemi(secilenYemek2, buton2);
        else if (yemekUcretsiz)
            yemekSecimIslemi(secilenYemek2, buton2);
        else
            aciklamaText.text = yemekler[secilenYemek2].yemekFiyati - scriptKontrol.envanterKontrol.ejderParasi + eksikMetni;
    }
    public void yemekSecimi3()
    {
        if (scriptKontrol.envanterKontrol.ejderParasi > yemekler[secilenYemek3].yemekFiyati)
            yemekSecimIslemi(secilenYemek3, buton3);
        else if (yemekUcretsiz)
            yemekSecimIslemi(secilenYemek3, buton3);
        else
            aciklamaText.text = yemekler[secilenYemek3].yemekFiyati - scriptKontrol.envanterKontrol.ejderParasi + eksikMetni;
    }
    public void yemekSecimIslemi(int secilenYemek, Button buton)
    {
        if (secilenYemek == 0)
            scriptKontrol.ozelEtkilerKontrol.sushi = true;
        if (secilenYemek == 1)
            scriptKontrol.ozelEtkilerKontrol.sashimi = true;
        if (secilenYemek == 2)
            scriptKontrol.ozelEtkilerKontrol.tempura = true;
        if (secilenYemek == 3)
            scriptKontrol.ozelEtkilerKontrol.ramen = true;
        if (secilenYemek == 4)
            scriptKontrol.ozelEtkilerKontrol.udon = true;
        if (secilenYemek == 5)
            scriptKontrol.ozelEtkilerKontrol.yakitori = true;
        if (secilenYemek == 6)
            scriptKontrol.ozelEtkilerKontrol.donburi = true;
        if (secilenYemek == 7)
            scriptKontrol.ozelEtkilerKontrol.miso = true;
        if (secilenYemek == 8)
            scriptKontrol.ozelEtkilerKontrol.takoyaki = true;
        if (secilenYemek == 9)
            scriptKontrol.ozelEtkilerKontrol.okonomiyaki = true;

        yemekSecti = true;
        buton.interactable = false;
        if (!yemekUcretsiz)
            scriptKontrol.envanterKontrol.ejderParasi -= yemekler[secilenYemek].yemekFiyati;
        devamEt();
    }
    public void durdur()
    {
        if (!yemekSecti)
            randomYemekGetir();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sefPaneli.SetActive(true);
        oyunPaneli.SetActive(false);
        welcomeText.GetComponent<localizedText>().key = "selamlama";
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        sefPaneli.SetActive(false);
        oyunPaneli.SetActive(true);
        if (yemekSecti)
        {
            sefDiyalog.GetComponent<localizedText>().key = "sef_bitti";
            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniUygula();
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
