using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class alfredPanelScripti : MonoBehaviour
{
    public bool oyuncuYakin, ozelGuc1Secildi, ozelGuc2Secildi, randomOzelGuclerGeldi;
    public int secilenOzelGuc1, secilenOzelGuc2, secilenOzelGuc3;
    public Button buton1, buton2, buton3;
    public GameObject oyunPaneli, alfredPanel, ozelGuc1, ozelGuc2;
    public TextMeshProUGUI aciklamaText, ozelGuc1Adi, ozelGuc2Adi, ozelGuc3Adi;
    public GameObject[] ozelGucObjeleri;
    public List<int> secilenOzelGucler = new List<int>();

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !alfredPanel.activeSelf)
            durdur();
        else if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && alfredPanel.activeSelf)
            devamEt();
    }

    public void randomOzelGucGetir()
    {
        randomOzelGuclerGeldi = true;
        while (secilenOzelGucler.Count < 3)
        {
            int rastgeleSayi = Random.Range(0, ozelGucObjeleri.Length);
            if (!secilenOzelGucler.Contains(rastgeleSayi))
            {
                secilenOzelGucler.Add(rastgeleSayi);
            }
        }

        secilenOzelGuc1 = secilenOzelGucler[0];
        secilenOzelGuc2 = secilenOzelGucler[1];
        secilenOzelGuc3 = secilenOzelGucler[2];

        ozelGuc1Adi.text = ozelGucObjeleri[secilenOzelGuc1].GetComponent<ozelGucOzellikleri>().ozelGucAd;
        ozelGuc2Adi.text = ozelGucObjeleri[secilenOzelGuc2].GetComponent<ozelGucOzellikleri>().ozelGucAd;
        ozelGuc3Adi.text = ozelGucObjeleri[secilenOzelGuc3].GetComponent<ozelGucOzellikleri>().ozelGucAd;

        buton1.GetComponent<Image>().sprite = ozelGucObjeleri[secilenOzelGuc1].GetComponent<ozelGucOzellikleri>().ozelGucIconu;
        buton2.GetComponent<Image>().sprite = ozelGucObjeleri[secilenOzelGuc2].GetComponent<ozelGucOzellikleri>().ozelGucIconu;
        buton3.GetComponent<Image>().sprite = ozelGucObjeleri[secilenOzelGuc3].GetComponent<ozelGucOzellikleri>().ozelGucIconu;
    }
    public void ozelGucSecimButonu1()
    {
        ozelGucSecimIslemi(secilenOzelGuc1, buton1);
    }
    public void ozelGucSecimButonu2()
    {
        ozelGucSecimIslemi(secilenOzelGuc2, buton2);
    }
    public void ozelGucSecimButonu3()
    {
        ozelGucSecimIslemi(secilenOzelGuc3, buton3);
    }
    public void ozelGucSecimIslemi(int secilenOzelGuc, Button buton)
    {
        if (!ozelGuc1Secildi)
        {
            aciklamaText.text = "Ikinci secimini yap";
            ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuc1Image.sprite = ozelGucObjeleri[secilenOzelGuc].GetComponent<SpriteRenderer>().sprite;
            ozelGuc1Secildi = true;
        }
        else
        {
            aciklamaText.text = "Ikinci secimini yap";
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuc2Image.sprite = ozelGucObjeleri[secilenOzelGuc].GetComponent<SpriteRenderer>().sprite;
            ozelGuc2Secildi = true;
        }
        buton.interactable = false;

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }
    public void durdur()
    {
        if (!randomOzelGuclerGeldi)
            randomOzelGucGetir();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        alfredPanel.SetActive(true);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        alfredPanel.SetActive(false);
        oyunPaneli.SetActive(true);
        if (ozelGuc1Secildi && ozelGuc2Secildi)
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
