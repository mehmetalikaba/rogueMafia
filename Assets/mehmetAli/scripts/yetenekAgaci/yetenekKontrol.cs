using System.Collections;
using System.Collections.Generic;
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


    void Start()
    {
        menzilliYeteneklerListesi = yetenekAgaclari.menzilliYetenekler;
        pasifYeteneklerListesi = yetenekAgaclari.pasifYetenekler;
        yakinYeteneklerListesi = yetenekAgaclari.yakinYetenekler;
    }

    void Update()
    {

    }
}
