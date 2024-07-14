using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rastgeleDusenSilah : MonoBehaviour
{
    public silahOzellikleri[] butunSilahlar;

    public GameObject silah1, silah2;

    public silahOzellikleri dusenSilah;

    public silahOzellikleriniGetir dusenSilahOzellikleriniGetir, silah1Ozellikleri, silah2Ozellikleri;

    public bool oyuncuYakin;

    public SpriteRenderer spriteRenderer;

    public silahSecimi silahSecimi;

    public int randomIndex;

    void Start()
    {
        List<silahOzellikleri> silahListesi = new List<silahOzellikleri>(butunSilahlar);

        spriteRenderer = GetComponent<SpriteRenderer>();

        randomIndex = Random.Range(0, silahListesi.Count);

        dusenSilah = butunSilahlar[randomIndex];

        spriteRenderer.sprite = butunSilahlar[randomIndex].silahIcon;
    }

    void Update()
    {
        if (oyuncuYakin)
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
            {
                if (dusenSilah.silahTuru == "yakin")
                {
                    Debug.Log("dusen silah yakin");
                    silah1Ozellikleri = silah1.GetComponent<silahOzellikleriniGetir>();
                    silah1Getir();
                }
                else if (dusenSilah.silahTuru == "menzilli")
                {
                    Debug.Log("dusen silah menzilli");
                    silah2Ozellikleri = silah2.GetComponent<silahOzellikleriniGetir>();
                    silah2Getir();
                }
                else
                    Debug.Log("bir hata oldu");
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = false;
    }

    public void silah1Getir()
    {
        Debug.Log("silah1Getir");
        silah1Ozellikleri.seciliSilah = dusenSilah;
        silah1Ozellikleri.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[randomIndex];
        silah1Ozellikleri.silahOzellikleriniGuncelle();

        Destroy(gameObject);
    }

    public void silah2Getir()
    {
        Debug.Log("silah2Getir");
        silah2Ozellikleri.seciliSilah = dusenSilah;
        silah2Ozellikleri.silahSecimi.tumSilahlar = silahSecimi.tumSilahlarListesi[randomIndex];
        silah2Ozellikleri.silahOzellikleriniGuncelle();

        Destroy(gameObject);
    }
}
