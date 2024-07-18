using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderhaPuani, anilar, kaydedilecekAniMiktari;

    public TextMeshProUGUI ejderhaPuaniMiktar, aniPuani;

    void Start()
    {

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
        kaydedilecekAniMiktari = anilar / 2;
        PlayerPrefs.SetFloat("anilarKayit", anilar);
        PlayerPrefs.Save();
        Debug.Log("anilar kaydedildi");
    }
}