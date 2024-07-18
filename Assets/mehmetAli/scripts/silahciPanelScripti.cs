using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class silahciPanelScripti : MonoBehaviour
{
    public GameObject silahciPaneli, silah1, silah2;
    List<int> secilenSilahlarListesi = new List<int>();
    public int secilenSilah1, secilenSilah2, secilenSilah3;

    public silahSecimi silahSecimi;
    public silahOzellikleriniGetir silah1Test, silah2Test;
    public silahOzellikleri[] butunSilahlar;
    public silahOzellikleri[] rastgeleSilahlar = new silahOzellikleri[3];
    public Button rastgeleSilah1Buton, rastgeleSilah2Buton, rastgeleSilah3Buton;
    public Image rastgeleSilah1Image, rastgeleSilah2Image, rastgeleSilah3Image;
    public bool silahPaneliAcildi, silah1Secildi, silah2Secildi;



    public void Start()
    {
        silah1Test = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Test = silah2.GetComponent<silahOzellikleriniGetir>();

        silahSecimi = new silahSecimi();
    }

    public void Update()
    {
        if (silahciPaneli.activeSelf && !silahPaneliAcildi)
        {
            silahPaneliAcildi = true;
            SilahlariSecmek();
        }

        if (silah1Secildi && silah2Secildi)
        {
            silahciPaneli.SetActive(false);
            silah1Test.seciliSilahinBilgileriniGetir();
            silah2Test.seciliSilahinBilgileriniGetir();
        }
    }

    public void SilahlariSecmek()
    {
        List<silahOzellikleri> silahListesi = new List<silahOzellikleri>(butunSilahlar);
        List<silahOzellikleri> secilenSilahlar = new List<silahOzellikleri>();

        while (secilenSilahlar.Count < 3)
        {
            int randomIndex = Random.Range(0, silahListesi.Count);

            if (!secilenSilahlarListesi.Contains(butunSilahlar.ToList().IndexOf(silahListesi[randomIndex])))
            {
                secilenSilahlar.Add(silahListesi[randomIndex]);
                secilenSilahlarListesi.Add(butunSilahlar.ToList().IndexOf(silahListesi[randomIndex]));
            }
        }

        for (int i = 0; i < rastgeleSilahlar.Length; i++)
        {
            rastgeleSilahlar[i] = secilenSilahlar[i];
        }

        string[] silahIsimleri = rastgeleSilahlar.Select(silah => silah.silahAdi).ToArray();

        rastgeleSilah1Image.sprite = rastgeleSilahlar[0].silahIcon;
        rastgeleSilah2Image.sprite = rastgeleSilahlar[1].silahIcon;
        rastgeleSilah3Image.sprite = rastgeleSilahlar[2].silahIcon;

        secilenSilah1 = secilenSilahlarListesi[0];
        secilenSilah2 = secilenSilahlarListesi[1];
        secilenSilah3 = secilenSilahlarListesi[2];
    }


    public void silahSecimi1()
    {
        rastgeleSilah1Buton.interactable = false;
        //ozelGucSecimIslemi(secilenOzelGuc1, rastgeleSilah1Buton);
    }
    public void silahSecimi2()
    {

    }
    public void silahSecimi3()
    {

    }

    public void silahSecimIslemi(int secilenOzelGuc, Button buton)
    {
        if (!silah1Secildi)
        {
            silah1Secildi = true;
            silah1Test.silahOzellikleriniGetirSilahOzellikleri = rastgeleSilahlar[2];
            silah1Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah3];
        }
        else
        {
            silah2Secildi = true;
            silah2Test.silahOzellikleriniGetirSilahOzellikleri = rastgeleSilahlar[2];
            silah2Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah3];
        }
    }
}