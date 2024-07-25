using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderParasi, aniPuani, ejderhaPuaniArtmaMiktari, olunceAniMiktariAzalmaYuzdesi, aniArttirmaMiktari;
    public TextMeshProUGUI ejderParasiText, aniPuaniText;
    public kaydedilecekler kaydedilecekler;
    public AudioSource topla;

    void Start()
    {

        ejderhaPuaniArtmaMiktari = 50f;
        olunceAniMiktariAzalmaYuzdesi = 2;
        aniPuani = kaydedilecekler.aniPuani;
        aniPuaniText.text = aniPuani.ToString("F0");
        ejderParasiText.text = aniPuani.ToString("F0");
    }

    public void ejderParasiArttir(float gelenEjderhaPuani)
    {
        topla.Play();
        ejderParasi += gelenEjderhaPuani;
        ejderParasiText.text = ejderParasi.ToString("F0");
    }

    public void aniArttir(float gelenAnilar)
    {
        gelenAnilar += aniArttirmaMiktari;
        aniPuani += gelenAnilar;
        aniPuaniText.text = aniPuani.ToString("F0");
    }
}