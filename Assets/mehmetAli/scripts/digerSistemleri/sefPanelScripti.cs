using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sefPanelScripti : MonoBehaviour
{
    public bool oyuncuYakin;
    public int secilenYemek1, secilenYemek2, secilenYemek3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, sefPaneli;
    public TextMeshProUGUI aciklamaText, yemek1Adi, yemek2Adi, yemek3Adi;
    public yemekOzellikleri[] yemekler;
    public scriptKontrol scriptKontrol;

    public List<int> secilenYemekler = new List<int>();

    public void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
            durdur();

        if (sefPaneli.activeSelf)
            randomYemekGetir();
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

        buton1.GetComponent<Image>().sprite = yemekler[secilenYemek1].yemekSprite;
        buton2.GetComponent<Image>().sprite = yemekler[secilenYemek2].yemekSprite;
        buton3.GetComponent<Image>().sprite = yemekler[secilenYemek3].yemekSprite;

        yemek1Adi.text = yemekler[secilenYemek1].yemekAdi;
        yemek2Adi.text = yemekler[secilenYemek2].yemekAdi;
        yemek3Adi.text = yemekler[secilenYemek3].yemekAdi;
    }
    public void yemekSecimi1()
    {
        if (scriptKontrol.envanterKontrol.ejderhaPuani > yemekler[secilenYemek1].yemekFiyati)
            yemekSecimIslemi(secilenYemek1, buton1);
        else
            aciklamaText.text = "Paran yetersiz: " + scriptKontrol.envanterKontrol.ejderhaPuani;
    }
    public void yemekSecimi2()
    {
        if (scriptKontrol.envanterKontrol.ejderhaPuani > yemekler[secilenYemek2].yemekFiyati)
            yemekSecimIslemi(secilenYemek2, buton2);
        else
            aciklamaText.text = "Paran yetersiz: " + scriptKontrol.envanterKontrol.ejderhaPuani;
    }
    public void yemekSecimi3()
    {
        if (scriptKontrol.envanterKontrol.ejderhaPuani > yemekler[secilenYemek3].yemekFiyati)
            yemekSecimIslemi(secilenYemek3, buton3);
        else
            aciklamaText.text = "Paran yetersiz: " + scriptKontrol.envanterKontrol.ejderhaPuani;
    }
    public void yemekSecimIslemi(int secilenYemek, Button buton)
    {
        if (secilenYemek == 0)
            scriptKontrol.ozelEtkilerKontrol.corek = true;
        if (secilenYemek == 1)
            scriptKontrol.ozelEtkilerKontrol.ekmek = true;
        if (secilenYemek == 2)
            scriptKontrol.ozelEtkilerKontrol.et = true;
        if (secilenYemek == 3)
            scriptKontrol.ozelEtkilerKontrol.somon = true;
        if (secilenYemek == 4)
            scriptKontrol.ozelEtkilerKontrol.sushi = true;

        buton.interactable = false;
        devamEt();
    }
    public void durdur()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sefPaneli.SetActive(true);
        oyunPaneli.SetActive(false);
        aciklamaText.text = "Paran: " + scriptKontrol.envanterKontrol.ejderhaPuani;
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        sefPaneli.SetActive(false);
        oyunPaneli.SetActive(true);
        gameObject.SetActive(false);
        this.enabled = false;
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
