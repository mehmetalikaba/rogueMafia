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
    public float silahSaldiriHizi;
    public Animator karakterAnimator;
    public Sprite silahIcon;

    public SpriteRenderer spriteRenderer;

    public silahSecimi.silahlar oncekiSilah;

    public silahlarTest selectedWeapon = null;



    void Awake()
    {
        UpdateWeapon();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = silahIcon;

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
                selectedWeapon = scriptableObjectler[0];
                break;
            case silahSecimi.silahlar.kunai:
                selectedWeapon = scriptableObjectler[1];
                break;
            case silahSecimi.silahlar.kusarigama:
                selectedWeapon = scriptableObjectler[2];
                break;
            case silahSecimi.silahlar.nunchaku:
                selectedWeapon = scriptableObjectler[3];
                break;
            case silahSecimi.silahlar.ryuPistol:
                selectedWeapon = scriptableObjectler[4];
                break;
            case silahSecimi.silahlar.ryuUzi:
                selectedWeapon = scriptableObjectler[5];
                break;
            case silahSecimi.silahlar.tekagiShuko:
                selectedWeapon = scriptableObjectler[6];
                break;
            case silahSecimi.silahlar.tessen:
                selectedWeapon = scriptableObjectler[7];
                break;
            case silahSecimi.silahlar.yumi:
                selectedWeapon = scriptableObjectler[8];
                break;
        }

        if (selectedWeapon != null)
        {
            silahTuru = selectedWeapon.silahTuru;
            silahAdi = selectedWeapon.silahAdi;
            silahSaldiriHasari = selectedWeapon.silahSaldiriHasari;
            silahSaldiriHizi = selectedWeapon.silahSaldiriHizi;
            karakterAnimator = selectedWeapon.karakterAnimator;
            silahIcon = selectedWeapon.silahIcon;

        }
    }

    public void katanayiSec()
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
    }
}