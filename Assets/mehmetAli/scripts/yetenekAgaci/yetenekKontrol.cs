using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class yetenekKontrol : MonoBehaviour
{
    public yetenekAgaclari yetenekAgaclari;
    public List<yetenekObjesi> menzilliYeteneklerListesi, pasifYeteneklerListesi, yakinYeteneklerListesi;

    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public envanterKontrol envanterKontrol;
    public canKontrol canKontrol;

    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, toplanabilir;

    public silahOzellikleri silah1Ozellikleri, silah2Ozellikleri;

    public silahOzellikleriniGetir silah1OzellikleriGetir, silah2OzellikleriGetir;


    void Start()
    {
        menzilliYeteneklerListesi = yetenekAgaclari.menzilliYetenekler;
        pasifYeteneklerListesi = yetenekAgaclari.pasifYetenekler;
        yakinYeteneklerListesi = yetenekAgaclari.yakinYetenekler;

        silah1OzellikleriGetir = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2OzellikleriGetir = silah2.GetComponent<silahOzellikleriniGetir>();

        silah1Ozellikleri = silah1OzellikleriGetir.silahOzellikleriniGetirSilahOzellikleri;
        silah2Ozellikleri = silah1OzellikleriGetir.silahOzellikleriniGetirSilahOzellikleri;
    }

    void Update()
    {

    }
}
