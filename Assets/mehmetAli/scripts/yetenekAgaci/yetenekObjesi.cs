using UnityEngine;

[CreateAssetMenu(fileName = "yetenekObjesi", menuName = "Scriptable Objects/yetenekObjesi")]
public class yetenekObjesi : ScriptableObject
{
    public bool yakin, menzilli, ozelGuc;
    public string yetenekAdi, aciklama;
    public int baslangicSeviyesi, seviye, maxSeviye, gerekliAniPuani;
    public Sprite ikon;
    public float[] hasarNeKadarArtacak, menzilNeKadarArtacak, beklemeSuresiNeKadarAzalacak, silahDayanikliligiNeKadarArtacak;
    public yetenekObjesi[] gerekliYetenekler;
    public silahOzellikleri[] silahOzellikleri;
    public ozelGucKullanmaScripti[] ozelGucKullanmaScripti;


    public void yetenekEtkisi()
    {
        if (yakin)
        {
            silahOzellikleri[0].silahSaldiriHasari += hasarNeKadarArtacak[seviye];
            silahOzellikleri[0].silahSaldiriHasari += silahDayanikliligiNeKadarArtacak[seviye];
        }
        if (menzilli)
        {
            silahOzellikleri[0].silahSaldiriHasari += hasarNeKadarArtacak[seviye];
            silahOzellikleri[0].silahSaldiriHasari += hasarNeKadarArtacak[seviye];
            silahOzellikleri[0].silahSaldiriHasari += silahDayanikliligiNeKadarArtacak[seviye];
        }
        if (ozelGuc)
        {
            ozelGucKullanmaScripti[0].ozelGuc2ToplamSure -= beklemeSuresiNeKadarAzalacak[seviye];
        }
    }
}
