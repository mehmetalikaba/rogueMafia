using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaydetKontrol : MonoBehaviour
{
    public GameObject silah1, silah2, toplanabilirObje, ozelGuc1, ozelGuc2;
    public silahOzellikleri silah1Kaydet, silah2Kaydet;
    public kaydedilecekler kaydedilecekler;

    void Start()
    {
        silah1.GetComponent<silahOzellikleriniGetir>();
        silah2.GetComponent<silahOzellikleriniGetir>();
        toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>();
        ozelGuc1.GetComponent<ozelGucKullanmaScripti>();
        ozelGuc2.GetComponent<ozelGucKullanmaScripti>();
    }

    void Update()
    {
        
    }
}
