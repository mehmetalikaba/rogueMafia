using System;
using UnityEngine;
using UnityEngine.UI;

public class silahOzellikleriniGetir : MonoBehaviour
{
    public silahOzellikleri[] butunSilahlarDizisi;
    public silahOzellikleri elindekiSilah;
    public string simdikiSilah;

    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriMenzili;
    public float silahDayanikliligi, silahDayanikliligiAzalmaMiktari;
    public RuntimeAnimatorController karakterAnimator;
    public Image silahImage;
    public string aciklamaKeyi;
    public AnimationClip[] animasyonClipleri;
    public GameObject solMenzilli, sagMenzilli, solZehirli, sagZehirli;
    public AudioClip[] saldiriSesi;
    public Image silahDayanikliligiImage;

    void Start()
    {
        if (elindekiSilah == null || simdikiSilah == null)
            Debug.Log("Elinde silah yok");
        else
            if (elindekiSilah != null && simdikiSilah != elindekiSilah.aciklamaKeyi)
            seciliSilahinBilgileriniGetir();
    }
    public void Update()
    {
        if (elindekiSilah != null && simdikiSilah != elindekiSilah.aciklamaKeyi)
        {
            simdikiSilah = elindekiSilah.aciklamaKeyi;
            seciliSilahinBilgileriniGetir();

        }
    }

    public void seciliSilahinBilgileriniGetir()
    {
        for (int i = 0; i < butunSilahlarDizisi.Length; i++)
            if (butunSilahlarDizisi[i].aciklamaKeyi == simdikiSilah)
                elindekiSilah = butunSilahlarDizisi[i];

        silahTuru = elindekiSilah.silahTuru;
        silahAdi = elindekiSilah.silahAdi;
        silahSaldiriHasari = elindekiSilah.silahSaldiriHasari;
        silahSaldiriMenzili = elindekiSilah.silahSaldiriMenzili;
        silahDayanikliligi = elindekiSilah.silahDayanikliligi;
        silahDayanikliligiAzalmaMiktari = elindekiSilah.silahDayanikliligiAzalmaMiktari;
        silahDayanikliligiImage.fillAmount = elindekiSilah.silahDayanikliligi;
        karakterAnimator = elindekiSilah.karakterAnimator;
        silahImage.sprite = elindekiSilah.silahIcon;
        aciklamaKeyi = elindekiSilah.aciklamaKeyi;
        if (elindekiSilah.silahAdi != "YUMRUK")
        {
            for (int i = 0; i < elindekiSilah.saldiriSesleri.Length; i++)
                saldiriSesi[i] = elindekiSilah.saldiriSesleri[i];
            for (int i = 0; i < elindekiSilah.animasyonClipleri.Length; i++)
                animasyonClipleri[i] = elindekiSilah.animasyonClipleri[i];
        }
        if (elindekiSilah.silahTuru == "menzilli")
        {
            solMenzilli = elindekiSilah.solMenzilli;
            sagMenzilli = elindekiSilah.sagMenzilli;
            solZehirli = elindekiSilah.solZehirli;
            sagZehirli = elindekiSilah.sagZehirli;
        }
    }
}