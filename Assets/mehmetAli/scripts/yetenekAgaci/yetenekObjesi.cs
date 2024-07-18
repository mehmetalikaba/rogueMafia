using UnityEngine;

[CreateAssetMenu(fileName = "yetenekObjesi", menuName = "Scriptable Objects/yetenekObjesi")]
public class yetenekObjesi : ScriptableObject
{
    public bool menzilli, pasif, yakin;
    public string yetenekAdi, aciklama;
    public float yetenekSeviyesi, maxSeviye, gerekliAniPuani;
    public Sprite yetenekIconu;
}
