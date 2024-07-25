using System;
using UnityEngine;
using UnityEngine.UI;

public class silahOzellikleriniGetir : MonoBehaviour
{
    public silahSecimi silahSecimi;
    public silahOzellikleri[] butunSilahlarDizisi;

    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriMenzili;
    public float silahDayanikliligi, silahDayanikliligiAzalmaMiktari;
    public RuntimeAnimatorController karakterAnimator;
    public Image silahImage;
    public string aciklamaKeyi;
    public AnimationClip[] animasyonClipleri;
    public GameObject solMenzilli, sagMenzilli;
    public AudioClip[] saldiriSesi;

    public silahSecimi.silahlar oncekiSilah;
    public silahOzellikleri secilenSilahOzellikleri = null;

    public Image silahDayanikliligiImage;


    void Awake()
    {
        silahOzellikleriniGuncelle();
    }

    public void Update()
    {
        if (silahSecimi.tumSilahlar != oncekiSilah)
        {
            silahOzellikleriniGuncelle();
        }
    }

    public void silahOzellikleriniGuncelle()
    {
        oncekiSilah = silahSecimi.tumSilahlar;

        switch (silahSecimi.tumSilahlar)
        {
            case silahSecimi.silahlar.katana:
                secilenSilahOzellikleri = butunSilahlarDizisi[0];
                break;
            case silahSecimi.silahlar.shuriken:
                secilenSilahOzellikleri = butunSilahlarDizisi[1];
                break;
            case silahSecimi.silahlar.tekagishuko:
                secilenSilahOzellikleri = butunSilahlarDizisi[2];
                break;
            case silahSecimi.silahlar.yumi:
                secilenSilahOzellikleri = butunSilahlarDizisi[3];
                break;
            case silahSecimi.silahlar.yumruk:
                secilenSilahOzellikleri = butunSilahlarDizisi[4];
                break;

        }

        if (secilenSilahOzellikleri != null)
        {
            seciliSilahinBilgileriniGetir();
        }
    }

    public void seciliSilahinBilgileriniGetir()
    {
        silahTuru = secilenSilahOzellikleri.silahTuru;
        silahAdi = secilenSilahOzellikleri.silahAdi;
        silahSaldiriHasari = secilenSilahOzellikleri.silahSaldiriHasari;
        silahSaldiriMenzili = secilenSilahOzellikleri.silahSaldiriMenzili;
        silahDayanikliligi = secilenSilahOzellikleri.silahDayanikliligi;
        silahDayanikliligiAzalmaMiktari = secilenSilahOzellikleri.silahDayanikliligiAzalmaMiktari;
        silahDayanikliligiImage.fillAmount = secilenSilahOzellikleri.silahDayanikliligi;
        karakterAnimator = secilenSilahOzellikleri.karakterAnimator;
        silahImage.sprite = secilenSilahOzellikleri.silahIcon;
        aciklamaKeyi = secilenSilahOzellikleri.aciklamaKeyi;
        if (secilenSilahOzellikleri.silahAdi != "YUMRUK")
        {
            for (int i = 0; i < secilenSilahOzellikleri.saldiriSesleri.Length; i++)
            {
                saldiriSesi[i] = secilenSilahOzellikleri.saldiriSesleri[i];
            }
            for (int i = 0; i < secilenSilahOzellikleri.animasyonClipleri.Length; i++)
            {
                animasyonClipleri[i] = secilenSilahOzellikleri.animasyonClipleri[i];
            }
        }
        if (secilenSilahOzellikleri.silahTuru == "menzilli")
        {
            solMenzilli = secilenSilahOzellikleri.solMenzilli;
            sagMenzilli = secilenSilahOzellikleri.sagMenzilli;
        }
    }
}