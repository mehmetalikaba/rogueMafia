using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using JetBrains.Annotations;

public class alfredPanelScripti : MonoBehaviour
{
    public GameObject alfredPanel, ozelGuc1, ozelGuc2;
    public List<int> secilenOzelGucler = new List<int>();
    public int secilenOzelGuc1, secilenOzelGuc2, secilenOzelGuc3;
    public bool oyuncuYakin, ozelGuc1Secildi, ozelGuc2Secildi;
    public Button buton1, buton2, buton3;
    public GameObject[] ozelGucObjeleri;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScript, ozelGuc2KullanmaScript;

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
        {
            durdur();
            alfredPanel.SetActive(true);
        }

        if (alfredPanel.activeSelf)
            randomOzelGucGetir();

        if (ozelGuc1Secildi && ozelGuc2Secildi)
        {
            devamEt();
            alfredPanel.SetActive(false);
        }
    }

    public void randomOzelGucGetir()
    {
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

        buton1.GetComponent<Image>().sprite = ozelGucObjeleri[secilenOzelGuc1].GetComponent<SpriteRenderer>().sprite;
        buton2.GetComponent<Image>().sprite = ozelGucObjeleri[secilenOzelGuc2].GetComponent<SpriteRenderer>().sprite;
        buton3.GetComponent<Image>().sprite = ozelGucObjeleri[secilenOzelGuc3].GetComponent<SpriteRenderer>().sprite;
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
            ozelGuc1KullanmaScript.ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc1KullanmaScript.ozelGuc1Image.sprite = ozelGucObjeleri[secilenOzelGuc].GetComponent<SpriteRenderer>().sprite;
            ozelGuc1Secildi = true;
        }
        else
        {
            ozelGuc2KullanmaScript.ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc2KullanmaScript.ozelGuc2Image.sprite = ozelGucObjeleri[secilenOzelGuc].GetComponent<SpriteRenderer>().sprite;
            ozelGuc2Secildi = true;
        }
        buton.interactable = false;
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
