using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ozelGucSecmeScripti : MonoBehaviour
{
    public Sprite[] ozelGucIkonlari;
    public GameObject[] ozelGucObjeleri;
    public bool ozelGuc1Secildi, ozelGuc2Secildi;

    public ozelGucKullanmaScripti ozelGuc1KullanmaScript, ozelGuc2KullanmaScript;
    public Button buton1, buton2, buton3;

    private int secilenOzelGuc1, secilenOzelGuc2, secilenOzelGuc3;

    public etkilesimKontrol etkilesimKontrol;

    public List<int> secilenOzelGucler = new List<int>();

    void Start()
    {
        etkilesimKontrol = FindObjectOfType<etkilesimKontrol>();

        RastgeleOzelGucBelirle();
    }

    void RastgeleOzelGucBelirle()
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

        buton1.GetComponent<Image>().sprite = ozelGucIkonlari[secilenOzelGuc1];
        buton2.GetComponent<Image>().sprite = ozelGucIkonlari[secilenOzelGuc2];
        buton3.GetComponent<Image>().sprite = ozelGucIkonlari[secilenOzelGuc3];
    }

    public void ozelGucSecimButonu1()
    {
        buton1.interactable = false;
        ozelGucSecimIslemi(secilenOzelGuc1, buton1);

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }

    public void ozelGucSecimButonu2()
    {
        buton2.interactable = false;
        ozelGucSecimIslemi(secilenOzelGuc2, buton2);

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }

    public void ozelGucSecimButonu3()
    {
        buton3.interactable = false;
        ozelGucSecimIslemi(secilenOzelGuc3, buton3);

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }

    public void ozelGucSecimIslemi(int secilenOzelGuc, Button buton)
    {
        if (!ozelGuc1Secildi)
        {
            ozelGuc1KullanmaScript.ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc1KullanmaScript.ozelGuc1Image.sprite = ozelGucIkonlari[secilenOzelGuc];
            ozelGuc1Secildi = true;
        }
        else
        {
            ozelGuc2KullanmaScript.ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc2KullanmaScript.ozelGuc2Image.sprite = ozelGucIkonlari[secilenOzelGuc];
            ozelGuc2Secildi = true;
        }
        buton.interactable = false;
    }

    public void devamEt()
    {
        etkilesimKontrol.alfredPanel.SetActive(false);
        etkilesimKontrol.oyunDevamEt();
    }
}
