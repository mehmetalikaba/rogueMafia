using UnityEngine;

[CreateAssetMenu(fileName = "YeniYetenek", menuName = "Yetenek Aðacý/Yetenek")]
public class Yetenek : ScriptableObject
{
    public bool yakin, menzilli, ozelGuc;
    public string yetenekAdi, aciklama;
    public int baslangicSeviyesi, seviye, maxSeviye, gerekliEjderhaPuani;
    public Sprite ikon;
    public float[] hasarNeKadarArtacak, menzilNeKadarArtacak, beklemeSuresiNeKadarAzalacak, silahDayanikliligiNeKadarArtacak;
    public Yetenek[] gerekliYetenekler;
    public silahOzellikleri[] silahOzellikleri;
    public ozelGucKullanmaScripti[] ozelGucKullanmaScripti;


    public void yetenekEtkisi()
    {
        if (yakin)
            silahOzellikleri[0].silahSaldiriHasari += hasarNeKadarArtacak[seviye];
        if (menzilli)
            silahOzellikleri[0].silahSaldiriHasari += hasarNeKadarArtacak[seviye];
        if (ozelGuc)
            ozelGucKullanmaScripti[0].ozelGuc2ToplamSure -= beklemeSuresiNeKadarAzalacak[seviye];

    }
}
