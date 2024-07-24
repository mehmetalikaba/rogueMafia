using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "kaydedileceklerKontrol", menuName = "Scriptable Objects/kaydedileceklerKontrol")]
public class kaydedilecekler : ScriptableObject
{
    public bool oyunaBasladi;
    public int hangiSahnede;
    public float oyuncuCan, aniPuani, ejderParasi, silah1Dayaniklilik, silah2Dayaniklilik;
    public GameObject toplanabilirObje, ozelGuc1Obje, ozelGuc2Obje;
    public silahSecimi silah1Secimi, silah2Secimi;
    public float[] sesSeviyeleri;

    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
