using System;
using UnityEngine;
using UnityEngine.UI;

public class silahOzellikleriniGetir : MonoBehaviour
{
    public silahSecimi silahSecimi;
    public silahOzellikleri[] scriptableObjectler;

    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriMenzili;
    public RuntimeAnimatorController karakterAnimator;
    public Image silahImage;

    public silahSecimi.silahlar oncekiSilah;

    public silahOzellikleri seciliSilah = null;

    void Awake()
    {

        UpdateWeapon();
    }

    public void Update()
    {
        if (silahSecimi.tumSilahlar != oncekiSilah)
        {
            UpdateWeapon();
        }
    }

    public void UpdateWeapon()
    {
        oncekiSilah = silahSecimi.tumSilahlar;

        switch (silahSecimi.tumSilahlar)
        {
            case silahSecimi.silahlar.katana:
                seciliSilah = scriptableObjectler[0];
                break;
            case silahSecimi.silahlar.kunai:
                seciliSilah = scriptableObjectler[1];
                break;
            case silahSecimi.silahlar.kusarigama:
                seciliSilah = scriptableObjectler[2];
                break;
            case silahSecimi.silahlar.nunchaku:
                seciliSilah = scriptableObjectler[3];
                break;
            case silahSecimi.silahlar.tekagiShuko:
                seciliSilah = scriptableObjectler[4];
                break;
            case silahSecimi.silahlar.tessen:
                seciliSilah = scriptableObjectler[5];
                break;
            case silahSecimi.silahlar.yumi:
                seciliSilah = scriptableObjectler[6];
                break;
        }

        if (seciliSilah != null)
        {
            silahTuru = seciliSilah.silahTuru;
            silahAdi = seciliSilah.silahAdi;
            silahSaldiriHasari = seciliSilah.silahSaldiriHasari;
            silahSaldiriMenzili = seciliSilah.silahSaldiriMenzili;
            karakterAnimator = seciliSilah.karakterAnimator;
            silahImage.sprite = seciliSilah.silahIcon;
        }
    }

    public void seciliSilahinBilgileriniGetir()
    {
        silahTuru = seciliSilah.silahTuru;
        silahAdi = seciliSilah.silahAdi;
        silahSaldiriHasari = seciliSilah.silahSaldiriHasari;
        silahSaldiriMenzili = seciliSilah.silahSaldiriMenzili;
        karakterAnimator = seciliSilah.karakterAnimator;
        silahImage.sprite = seciliSilah.silahIcon;
    }
}