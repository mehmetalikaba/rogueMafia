using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RastgeleSilah : MonoBehaviour
{
    public GameObject silahSecimPaneli;

    public GameObject silah1, silah2;

    public silahKontrol silahKontrol;
    public silahSecimi silahSecimi;
    public silahTest silahTest, silah1Test, silah2Test;

    public silahlarTest[] butunSilahlar;
    public silahlarTest[] rastgeleSilahlar = new silahlarTest[3];
    public silahlarTest[] seciliSilahlar = new silahlarTest[2];

    public Button rastgeleSilah1Buton, rastgeleSilah2Buton, rastgeleSilah3Buton;

    public Image rastgeleSilah1Image, rastgeleSilah2Image, rastgeleSilah3Image;

    public bool silahPaneliAcildi, silah1Secildi, silah2Secildi, buton1, buton2, buton3;

    public int secilenSilah1, secilenSilah2, secilenSilah3;

    List<int> secilenSilahIndeksleri = new List<int>();

    public void Start()
    {
        silah1Test = silah1.GetComponent<silahTest>();
        silah2Test = silah2.GetComponent<silahTest>();

        silahSecimi = new silahSecimi();
    }

    public void Update()
    {
        if (silahSecimPaneli.activeSelf && !silahPaneliAcildi)
        {
            silahPaneliAcildi = true;
            SilahlariSecmek();
        }

        if (silah1Secildi && silah2Secildi)
        {
            silahSecimPaneli.SetActive(false);
            silah1Test.seciliSilahinBilgileriniGetir();
            silah2Test.seciliSilahinBilgileriniGetir();
        }
    }

    public void SilahlariSecmek()
    {
        List<silahlarTest> silahListesi = new List<silahlarTest>(butunSilahlar);
        List<silahlarTest> secilenSilahlar = new List<silahlarTest>();

        while (secilenSilahlar.Count < 3)
        {
            int randomIndex = Random.Range(0, silahListesi.Count);

            if (!secilenSilahIndeksleri.Contains(butunSilahlar.ToList().IndexOf(silahListesi[randomIndex])))
            {
                secilenSilahlar.Add(silahListesi[randomIndex]);
                secilenSilahIndeksleri.Add(butunSilahlar.ToList().IndexOf(silahListesi[randomIndex]));
            }
        }

        for (int i = 0; i < rastgeleSilahlar.Length; i++)
        {
            rastgeleSilahlar[i] = secilenSilahlar[i];
        }

        string[] silahIsimleri = rastgeleSilahlar.Select(silah => silah.silahAdi).ToArray();
        Debug.Log("gelen silahlar: " + string.Join(", ", silahIsimleri));
        Debug.Log("1. silah " + secilenSilahIndeksleri[0]);
        Debug.Log("2. silah " + secilenSilahIndeksleri[1]);
        Debug.Log("3. silah " + secilenSilahIndeksleri[2]);

        rastgeleSilah1Image.sprite = rastgeleSilahlar[0].silahIcon;
        rastgeleSilah2Image.sprite = rastgeleSilahlar[1].silahIcon;
        rastgeleSilah3Image.sprite = rastgeleSilahlar[2].silahIcon;

        secilenSilah1 = secilenSilahIndeksleri[0];
        secilenSilah2 = secilenSilahIndeksleri[1];
        secilenSilah3 = secilenSilahIndeksleri[2];
    }


    public void silahSecimi1()
    {
        if (!buton1)
        {
            buton1 = true;
            if (!silah1Secildi)
            {
                silah1Secildi = true;
                silah1Test.seciliSilah = rastgeleSilahlar[0];
                silah1Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah1];
            }
            else
            {
                silah2Secildi = true;
                silah2Test.seciliSilah = rastgeleSilahlar[0];
                silah2Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah1];
            }
        }
    }
    public void silahSecimi2()
    {
        if (!buton2)
        {
            buton2 = true;
            if (!silah1Secildi)
            {
                silah1Secildi = true;
                silah1Test.seciliSilah = rastgeleSilahlar[1];
                silah1Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah2];
            }
            else
            {
                silah2Secildi = true;
                silah2Test.seciliSilah = rastgeleSilahlar[1];
                silah2Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah2];
            }
        }
    }
    public void silahSecimi3()
    {
        if (!buton3)
        {
            buton3 = true;
            if (!silah1Secildi)
            {
                silah1Secildi = true;
                silah1Test.seciliSilah = rastgeleSilahlar[2];
                silah1Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah3];
            }
            else
            {
                silah2Secildi = true;
                silah2Test.seciliSilah = rastgeleSilahlar[2];
                silah2Test.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[secilenSilah3];
            }
        }
    }
}