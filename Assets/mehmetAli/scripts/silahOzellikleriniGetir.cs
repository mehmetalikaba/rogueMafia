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
    public float silahDayanikliligi;
    public RuntimeAnimatorController karakterAnimator;
    public Image silahImage;
    public string aciklamaKeyi;
    public AnimationClip[] animasyonClipleri;
    public GameObject solMenzilli, sagMenzilli;

    public silahSecimi.silahlar oncekiSilah;
    public silahOzellikleri silahOzellikleriniGetirSilahOzellikleri = null;

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
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[0];
                break;
            case silahSecimi.silahlar.kunai:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[1];
                break;
            case silahSecimi.silahlar.kusarigama:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[2];
                break;
            case silahSecimi.silahlar.nunchaku:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[3];
                break;
            case silahSecimi.silahlar.tekagiShuko:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[4];
                break;
            case silahSecimi.silahlar.tessen:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[5];
                break;
            case silahSecimi.silahlar.yumi:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[6];
                break;
            case silahSecimi.silahlar.yumruk:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[7];
                break;
            case silahSecimi.silahlar.shuriken:
                silahOzellikleriniGetirSilahOzellikleri = butunSilahlarDizisi[8];
                break;
        }

        if (silahOzellikleriniGetirSilahOzellikleri != null)
        {
            silahTuru = silahOzellikleriniGetirSilahOzellikleri.silahTuru;
            silahAdi = silahOzellikleriniGetirSilahOzellikleri.silahAdi;
            silahSaldiriHasari = silahOzellikleriniGetirSilahOzellikleri.silahSaldiriHasari;
            silahSaldiriMenzili = silahOzellikleriniGetirSilahOzellikleri.silahSaldiriMenzili;
            silahDayanikliligi = silahOzellikleriniGetirSilahOzellikleri.silahDayanikliligi;
            silahDayanikliligiImage.fillAmount = silahOzellikleriniGetirSilahOzellikleri.silahDayanikliligi;
            karakterAnimator = silahOzellikleriniGetirSilahOzellikleri.karakterAnimator;
            silahImage.sprite = silahOzellikleriniGetirSilahOzellikleri.silahIcon;
            aciklamaKeyi = silahOzellikleriniGetirSilahOzellikleri.aciklamaKeyi;
            if (silahOzellikleriniGetirSilahOzellikleri.silahAdi != "YUMRUK")
            {
                for (int i = 0; i < silahOzellikleriniGetirSilahOzellikleri.animasyonClipleri.Length; i++)
                {
                    animasyonClipleri[i] = silahOzellikleriniGetirSilahOzellikleri.animasyonClipleri[i];
                }
            }
            if (silahOzellikleriniGetirSilahOzellikleri.silahTuru == "menzilli")
            {
                solMenzilli = silahOzellikleriniGetirSilahOzellikleri.solMenzilli;
                sagMenzilli = silahOzellikleriniGetirSilahOzellikleri.sagMenzilli;
            }
        }
    }

    public void seciliSilahinBilgileriniGetir()
    {
        silahTuru = silahOzellikleriniGetirSilahOzellikleri.silahTuru;
        silahAdi = silahOzellikleriniGetirSilahOzellikleri.silahAdi;
        silahSaldiriHasari = silahOzellikleriniGetirSilahOzellikleri.silahSaldiriHasari;
        silahSaldiriMenzili = silahOzellikleriniGetirSilahOzellikleri.silahSaldiriMenzili;
        silahDayanikliligi = silahOzellikleriniGetirSilahOzellikleri.silahDayanikliligi;
        silahDayanikliligiImage.fillAmount = silahOzellikleriniGetirSilahOzellikleri.silahDayanikliligi;
        karakterAnimator = silahOzellikleriniGetirSilahOzellikleri.karakterAnimator;
        silahImage.sprite = silahOzellikleriniGetirSilahOzellikleri.silahIcon;
        aciklamaKeyi = silahOzellikleriniGetirSilahOzellikleri.aciklamaKeyi;
        if (silahOzellikleriniGetirSilahOzellikleri.silahAdi != "YUMRUK")
        {
            for (int i = 0; i < silahOzellikleriniGetirSilahOzellikleri.animasyonClipleri.Length; i++)
            {
                animasyonClipleri[i] = silahOzellikleriniGetirSilahOzellikleri.animasyonClipleri[i];
            }
        }
        if (silahOzellikleriniGetirSilahOzellikleri.silahTuru == "menzilli")
        {
            solMenzilli = silahOzellikleriniGetirSilahOzellikleri.solMenzilli;
            sagMenzilli = silahOzellikleriniGetirSilahOzellikleri.sagMenzilli;
        }
    }
}