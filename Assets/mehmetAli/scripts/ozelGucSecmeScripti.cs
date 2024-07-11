using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ozelGucSecmeScripti : MonoBehaviour
{
    public Sprite[] ozelGucIkonlari;
    public GameObject[] ozelGucObjeleri;
    public bool ozelGuc1Secildi, ozelGuc2Secildi;

    public ozelGucKullanmaScripti ozelGuc1KullanmaScript, ozelGuc2KullanmaScript;
    public Button buton1, buton2;

    private int secilenOzelGuc1, secilenOzelGuc2;

    public etkilesimKontrol etkilesimKontrol;

    void Start()
    {
        etkilesimKontrol = FindObjectOfType<etkilesimKontrol>();

        RastgeleOzelGucBelirle();

        buton1.onClick.AddListener(ozelGuc1Secimi);
        buton2.onClick.AddListener(ozelGuc2Secimi);
    }

    void RastgeleOzelGucBelirle()
    {
        secilenOzelGuc1 = Random.Range(0, ozelGucObjeleri.Length);

        do
        {
            secilenOzelGuc2 = Random.Range(0, ozelGucObjeleri.Length);
        }
        while (secilenOzelGuc1 == secilenOzelGuc2);

        buton1.GetComponent<Image>().sprite = ozelGucIkonlari[secilenOzelGuc1];
        buton2.GetComponent<Image>().sprite = ozelGucIkonlari[secilenOzelGuc2];
    }

    void ozelGuc1Secimi()
    {
        if (!ozelGuc1Secildi)
        {
            ozelGuc1KullanmaScript.ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc1];
            ozelGuc1KullanmaScript.ozelGuc1Image.sprite = ozelGucIkonlari[secilenOzelGuc1];
            ozelGuc1Secildi = true;
            buton1.interactable = false;
        }

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }

    void ozelGuc2Secimi()
    {
        if (!ozelGuc2Secildi)
        {
            ozelGuc2KullanmaScript.ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc2];
            ozelGuc2KullanmaScript.ozelGuc2Image.sprite = ozelGucIkonlari[secilenOzelGuc2];
            ozelGuc2Secildi = true;
            buton2.interactable = false;
        }

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }

    public void devamEt()
    {
        etkilesimKontrol.alfredPanel.SetActive(false);
        etkilesimKontrol.oyunDevamEt();
    }
}
