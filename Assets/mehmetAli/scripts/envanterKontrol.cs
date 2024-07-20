using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderhaPuani, anilar, ejderhaPuaniArtmaMiktari, olunceAniMiktariAzalmaYuzdesi;

    public TextMeshProUGUI ejderhaPuaniMiktar, aniPuani;

    yetenekKontrol yetenekKontrol;

    void Start()
    {
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();

        anilar = 250f;

        ejderhaPuaniArtmaMiktari = 50f;
        olunceAniMiktariAzalmaYuzdesi = 2;
        if (PlayerPrefs.HasKey("anilarKayit"))
        {
            anilar = PlayerPrefs.GetFloat("anilarKayit");
            aniPuani.text = anilar.ToString("F0");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            aniKaydet();
        }
    }

    public void ejderhaPuaniArttir(float gelenEjderhaPuani)
    {
        ejderhaPuani += gelenEjderhaPuani;
        ejderhaPuaniMiktar.text = ejderhaPuani.ToString("F0");
    }

    public void aniArttir(float gelenAnilar)
    {
        anilar += gelenAnilar;
        aniPuani.text = anilar.ToString("F0");
    }

    public void aniKaydet()
    {
        yetenekKontrol.pasif1SkillEtkileriniUygula();
        PlayerPrefs.SetFloat("anilarKayit", anilar / olunceAniMiktariAzalmaYuzdesi);
        PlayerPrefs.Save();
    }
}