using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "yetenekObjesi", menuName = "Scriptable Objects/yetenekObjesi")]
public class yetenekObjesi : ScriptableObject
{
    public bool baslangic, menzilli, pasif, yakin, oyunaUygulandi, gelistirilebilir;
    public string yetenekAdi, aciklama;
    public int yetenekSeviyesi, maxSeviye, gerekliAniPuani;
    public List<yetenekObjesi> gerekliYetenekler;
}
