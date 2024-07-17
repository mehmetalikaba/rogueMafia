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

    public silahSecimi.silahlar oncekiSilah;
    public silahOzellikleri seciliSilah = null;

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
                seciliSilah = butunSilahlarDizisi[0];
                break;
            case silahSecimi.silahlar.kunai:
                seciliSilah = butunSilahlarDizisi[1];
                break;
            case silahSecimi.silahlar.kusarigama:
                seciliSilah = butunSilahlarDizisi[2];
                break;
            case silahSecimi.silahlar.nunchaku:
                seciliSilah = butunSilahlarDizisi[3];
                break;
            case silahSecimi.silahlar.tekagiShuko:
                seciliSilah = butunSilahlarDizisi[4];
                break;
            case silahSecimi.silahlar.tessen:
                seciliSilah = butunSilahlarDizisi[5];
                break;
            case silahSecimi.silahlar.yumi:
                seciliSilah = butunSilahlarDizisi[6];
                break;
            case silahSecimi.silahlar.yumruk:
                seciliSilah = butunSilahlarDizisi[7];
                break;
        }

        if (seciliSilah != null)
        {
            silahTuru = seciliSilah.silahTuru;
            silahAdi = seciliSilah.silahAdi;
            silahSaldiriHasari = seciliSilah.silahSaldiriHasari;
            silahSaldiriMenzili = seciliSilah.silahSaldiriMenzili;
            silahDayanikliligi = seciliSilah.silahDayanikliligi;
            silahDayanikliligiImage.fillAmount = seciliSilah.silahDayanikliligi;
            karakterAnimator = seciliSilah.karakterAnimator;
            silahImage.sprite = seciliSilah.silahIcon;
            aciklamaKeyi = seciliSilah.aciklamaKeyi;
            if (seciliSilah.silahAdi != "YUMRUK")
            {
                for (int i = 0; i < seciliSilah.animasyonClipleri.Length; i++)
                {
                    animasyonClipleri[i] = seciliSilah.animasyonClipleri[i];
                }
            }
        }
    }

    public void seciliSilahinBilgileriniGetir()
    {
        silahTuru = seciliSilah.silahTuru;
        silahAdi = seciliSilah.silahAdi;
        silahSaldiriHasari = seciliSilah.silahSaldiriHasari;
        silahSaldiriMenzili = seciliSilah.silahSaldiriMenzili;
        silahDayanikliligi = seciliSilah.silahDayanikliligi;
        silahDayanikliligiImage.fillAmount = seciliSilah.silahDayanikliligi;
        karakterAnimator = seciliSilah.karakterAnimator;
        silahImage.sprite = seciliSilah.silahIcon;
        aciklamaKeyi = seciliSilah.aciklamaKeyi;
        if (seciliSilah.silahAdi != "YUMRUK")
        {
            for (int i = 0; i < seciliSilah.animasyonClipleri.Length; i++)
            {
                animasyonClipleri[i] = seciliSilah.animasyonClipleri[i];
            }
        }
    }
}