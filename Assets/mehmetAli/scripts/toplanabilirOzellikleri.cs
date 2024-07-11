using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toplanabilirOzellikleri : MonoBehaviour
{
    public string toplanabilirAdi, toplanabilirAciklamaKeyi;

    public Sprite toplanabilirIcon;



    public canKontrol canKontrol;

    public oyuncuSaldiriTest oyuncuSaldiriTest;

    public oyuncuHareket oyuncuHareket;

    public float toplanabilirEtkiSuresi;


    void Start()
    {

    }

    void Update()
    {

    }


    public void canObjesiOzelligi()
    {
        canKontrol = FindObjectOfType<canKontrol>();

        if (canKontrol.can < 75)
        {
            canKontrol.can += ((canKontrol.can / 100) * 25);
        }
        else if (canKontrol.can > 75)
        {
            float fazlaOlanCanMiktari = 100 - canKontrol.can;

            canKontrol.can = 75;

            canKontrol.can += ((canKontrol.can / 100) * 25);
        }


        toplanabilirEtkiSuresi = 30f;
    }
    public void dayaniklilikObjesiOzelligi()
    {
        canKontrol = FindObjectOfType<canKontrol>();

        toplanabilirEtkiSuresi = 30f;
    }
    public void hasarObjesiOzelligi()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        toplanabilirEtkiSuresi = 30f;
    }
    public void hareketHiziObjesiOzelligi()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        toplanabilirEtkiSuresi = 30f;
    }




}
