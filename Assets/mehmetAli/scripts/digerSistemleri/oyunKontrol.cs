using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour
{
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public kaydetKontrol kaydetKontrol;

    private void Awake()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterYukle();
        kaydetKontrol.kaydetKontrolOzelEtkiler.jsonOzelEtkilerYukle();
        kaydetKontrol.kaydetKontrolSes.jsonSesYukle();
        kaydetKontrol.kaydetKontrolYetenek.jsonYetenekYukle();
    }

    void Start()
    {
        ozelEtkilerKontrol.yemekEtkileriniYukle();
        ozelEtkilerKontrol.yemekEtkileriniUygula();
    }

    void Update()
    {

    }
}
