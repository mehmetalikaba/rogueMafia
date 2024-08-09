using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour
{
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public kaydetKontrol kaydetKontrol;
    public yetenekKontrol yetenekKontrol;

    private void Awake()
    {
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
        kaydetKontrol = FindObjectOfType<kaydetKontrol>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
    }

    void Start()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterYukle();
        kaydetKontrol.kaydetKontrolOzelEtkiler.jsonOzelEtkilerYukle();
        kaydetKontrol.kaydetKontrolSes.jsonSesYukle();
        kaydetKontrol.kaydetKontrolYetenek.jsonYetenekYukle();

        ozelEtkilerKontrol.yemekEtkileriniUygula();
        yetenekKontrol.yetenekleriUygula();
    }

    void Update()
    {

    }
}
