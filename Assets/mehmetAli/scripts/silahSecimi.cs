using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class silahSecimi
{
    public enum silahlar { katana, kunai, kusarigama, nunchaku, tekagiShuko, tessen, yumi, yumruk,shuriken };
    public silahlar tumSilahlar;
    public List<silahlar> tumSilahlarListesi = new List<silahlar>();

    public silahSecimi()
    {
        foreach (silahlar silah in System.Enum.GetValues(typeof(silahlar)))
        {
            tumSilahlarListesi.Add(silah);
        }
    }

    public void silahSec(string silahAdi)
    {
        if (System.Enum.TryParse(silahAdi, out silahlar secilenSilah))
        {
            tumSilahlar = secilenSilah;
        }
        else
        {
            Debug.LogError("Gecersiz silah adi: " + silahAdi);
        }
    }
}