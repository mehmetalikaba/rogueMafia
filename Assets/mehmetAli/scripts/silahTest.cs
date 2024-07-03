using System;
using UnityEngine;
using UnityEngine.UI;

public class silahTest : MonoBehaviour
{
    public silahSecimi silahSecimi;
    public silahlarTest[] scriptableObjectler;

    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriMenzili;
    public RuntimeAnimatorController karakterAnimator;
    public Sprite silahIcon;

    public SpriteRenderer spriteRenderer;

    public silahSecimi.silahlar oncekiSilah;

    public silahlarTest seciliSilah = null;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

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
            case silahSecimi.silahlar.ryuPistol:
                seciliSilah = scriptableObjectler[4];
                break;
            case silahSecimi.silahlar.ryuUzi:
                seciliSilah = scriptableObjectler[5];
                break;
            case silahSecimi.silahlar.tekagiShuko:
                seciliSilah = scriptableObjectler[6];
                break;
            case silahSecimi.silahlar.tessen:
                seciliSilah = scriptableObjectler[7];
                break;
            case silahSecimi.silahlar.yumi:
                seciliSilah = scriptableObjectler[8];
                break;
        }

        if (seciliSilah != null)
        {
            silahTuru = seciliSilah.silahTuru;
            silahAdi = seciliSilah.silahAdi;
            silahSaldiriHasari = seciliSilah.silahSaldiriHasari;
            silahSaldiriMenzili = seciliSilah.silahSaldiriMenzili;
            karakterAnimator = seciliSilah.karakterAnimator;
            spriteRenderer.sprite = seciliSilah.silahIcon;
        }
    }

    public void seciliSilahinBilgileriniGetir()
    {
        silahTuru = seciliSilah.silahTuru;
        silahAdi = seciliSilah.silahAdi;
        silahSaldiriHasari = seciliSilah.silahSaldiriHasari;
        silahSaldiriMenzili = seciliSilah.silahSaldiriMenzili;
        karakterAnimator = seciliSilah.karakterAnimator;
        spriteRenderer.sprite = seciliSilah.silahIcon;
    }
}
/*public void katanayiSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.katana;
}
public void kunaiyiSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.kunai;
}
public void kusarigamayiSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.kusarigama;
}
public void nunchakuyuSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.nunchaku;
}
public void ryuPistoluSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.ryuPistol;
}
public void ryuUziyiSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.ryuUzi;
}
public void tekagiShukoyuSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.tekagiShuko;
}
public void tesseniSec()
{
    silahSecimi.tumSilahlar = silahSecimi.silahlar.tessen;
}
public void yumiyiSec()
{
    Debug.Log("secti");
    silahSecimi.tumSilahlar = silahSecimi.silahlar.yumi;
}*/