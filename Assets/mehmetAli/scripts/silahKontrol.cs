using UnityEngine;
using UnityEngine.UI;

public class silahKontrol : MonoBehaviour
{
    public silahOzellikleriniGetir silah1Ozellikleri;
    public silahOzellikleriniGetir silah2Ozellikleri;

    public GameObject silah1, silah2;

    public silahSecimi.silahlar geciciSilah;

    void Start()
    {
        silah1Ozellikleri = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = silah2.GetComponent<silahOzellikleriniGetir>();

        silah1Getir(silah1Ozellikleri.silahSecimi.tumSilahlar);
        silah2Getir(silah2Ozellikleri.silahSecimi.tumSilahlar);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            silahlarDegistir();
        }
    }

    public void silah1Getir(silahSecimi.silahlar silahAdi)
    {
        silah1Ozellikleri.silahSecimi.tumSilahlar = silahAdi;
        silah1Ozellikleri.UpdateWeapon();
    }

    public void silah2Getir(silahSecimi.silahlar silahAdi)
    {
        silah2Ozellikleri.silahSecimi.tumSilahlar = silahAdi;
        silah2Ozellikleri.UpdateWeapon();
    }

    public void silahlarDegistir()
    {
        geciciSilah = silah1Ozellikleri.silahSecimi.tumSilahlar;
        silah1Getir(silah2Ozellikleri.silahSecimi.tumSilahlar);
        silah2Getir(geciciSilah);
    }
}