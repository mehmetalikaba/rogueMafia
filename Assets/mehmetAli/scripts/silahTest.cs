using System;
using UnityEngine;
using UnityEngine.UI;

public class silahTest : MonoBehaviour
{
    public silahSecimi silahSecimi;
    public silahlarTest[] scriptableObjects;

    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriHizi;
    public Animator karakterAnimator;
    public Sprite silahIcon;

    private silahSecimi.silahlar oncekiSilah;

    void Start()
    {
        UpdateWeapon();
    }

    void Update()
    {
        if (silahSecimi.tumSilahlar != oncekiSilah)
        {
            UpdateWeapon();
        }
    }

    void UpdateWeapon()
    {
        oncekiSilah = silahSecimi.tumSilahlar;

        silahlarTest selectedWeapon = null;
        switch (silahSecimi.tumSilahlar)
        {
            case silahSecimi.silahlar.katana:
                selectedWeapon = scriptableObjects[0];
                break;
            case silahSecimi.silahlar.kunai:
                selectedWeapon = scriptableObjects[1];
                break;
            case silahSecimi.silahlar.kusarigama:
                selectedWeapon = scriptableObjects[2];
                break;
            case silahSecimi.silahlar.nunchaku:
                selectedWeapon = scriptableObjects[3];
                break;
            case silahSecimi.silahlar.ryuPistol:
                selectedWeapon = scriptableObjects[4];
                break;
            case silahSecimi.silahlar.ryuUzi:
                selectedWeapon = scriptableObjects[5];
                break;
            case silahSecimi.silahlar.tekagiShuko:
                selectedWeapon = scriptableObjects[6];
                break;
            case silahSecimi.silahlar.tessen:
                selectedWeapon = scriptableObjects[7];
                break;
            case silahSecimi.silahlar.yumi:
                selectedWeapon = scriptableObjects[8];
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

            Debug.Log("Seçilen Silah: " + silahAdi + ", Saldýrý Hýzý: " + silahSaldiriHizi);
        }
        else
        {
            Debug.LogWarning("Seçilen silah türüne uygun Scriptable Object bulunamadý.");
        }
    }
}