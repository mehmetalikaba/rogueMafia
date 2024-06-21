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

    public Button rastgeleSilah1Buton;
    public Button rastgeleSilah2Buton;
    public Button rastgeleSilah3Buton;

    public Image rastgeleSilah1Image;
    public Image rastgeleSilah2Image;
    public Image rastgeleSilah3Image;

    private int silahSlotIndex = 0;

    public bool silahlarSecildi, silah1Secildi, silah2Secildi;

    public bool buton1, buton2, buton3;

    public void Awake()
    {
    }

    public void Start()
    {

        silah1Test = silah1.GetComponent<silahTest>();
        silah2Test = silah2.GetComponent<silahTest>();

    }

    public void Update()
    {
        if (silahSecimPaneli.activeSelf && !silahlarSecildi)
        {
            silahlarSecildi = true;
            SilahlariSecmek();
        }
    }

    public void SilahlariSecmek()
    {
        List<silahlarTest> silahListesi = new List<silahlarTest>(butunSilahlar);
        List<silahlarTest> secilenSilahlar = new List<silahlarTest>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, silahListesi.Count);
            secilenSilahlar.Add(silahListesi[randomIndex]);
            silahListesi.RemoveAt(randomIndex);
        }

        for (int i = 0; i < rastgeleSilahlar.Length; i++)
        {
            rastgeleSilahlar[i] = secilenSilahlar[i];
        }

        string[] silahIsimleri = rastgeleSilahlar.Select(silah => silah.silahAdi).ToArray();
        Debug.Log("Rastgele Seçilen Silahlar: " + string.Join(", ", silahIsimleri));

    }

    public void silahSecimi1()
    {
        if (!buton1)
        {
            buton1 = true;
            if (!silah1Secildi)
            {
                silah1Test.seciliSilah = rastgeleSilahlar[0];
            }
            else
            {
                silah2Test.seciliSilah = rastgeleSilahlar[0];
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
                silah1Test.seciliSilah = rastgeleSilahlar[1];
            }
            else
            {
                silah2Test.seciliSilah = rastgeleSilahlar[1];
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
                silah1Test.seciliSilah = rastgeleSilahlar[2];
            }
            else
            {
                silah2Test.seciliSilah = rastgeleSilahlar[2];
            }
        }
    }


}