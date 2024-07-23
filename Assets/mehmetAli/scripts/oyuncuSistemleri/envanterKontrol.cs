using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderParasi, aniPuani, ejderhaPuaniArtmaMiktari, olunceAniMiktariAzalmaYuzdesi;
    public TextMeshProUGUI ejderParasiText, aniPuaniText;
    public kaydedilecekler kaydedilecekler;

    void Start()
    {

        ejderhaPuaniArtmaMiktari = 50f;
        olunceAniMiktariAzalmaYuzdesi = 2;
        aniPuani = kaydedilecekler.aniPuani;
        aniPuaniText.text = aniPuani.ToString("F0");
    }

    public void ejderhaPuaniArttir(float gelenEjderhaPuani)
    {
        ejderParasi += gelenEjderhaPuani;
        ejderParasiText.text = ejderParasi.ToString("F0");
    }

    public void aniArttir(float gelenAnilar)
    {
        aniPuani += gelenAnilar;
        aniPuaniText.text = aniPuani.ToString("F0");
    }
}