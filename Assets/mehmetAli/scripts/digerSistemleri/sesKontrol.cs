using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class sesKontrol : MonoBehaviour
{
    public AudioSource[] oyunMuzikleri, ortamSesleri, menuMuzigi, sesEfektleri;
    public Slider[] sesSeviyeleriSlider;
    public float[] sesSeviyeleri;
    public bool menude, oyunda;

    public AudioSource[] sahnedekiSesler;

    void Start()
    {
        sesDegerleriniGetir();
        sesDegerleriniAyarla();

        if (menude)
        {
            sesSeviyeleriSlider[0].onValueChanged.AddListener(delegate { sesDegerleriniAyarla(); });
            sesSeviyeleriSlider[1].onValueChanged.AddListener(delegate { sesDegerleriniAyarla(); });
            sesSeviyeleriSlider[2].onValueChanged.AddListener(delegate { sesDegerleriniAyarla(); });
            sesSeviyeleriSlider[3].onValueChanged.AddListener(delegate { sesDegerleriniAyarla(); });
        }

        if (oyunda)
            sahnedekiSesler = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        if (menude)
            sesDegerleriniKaydet();
    }

    void sesDegerleriniAyarla()
    {
        if (menude)
        {
            sesSeviyeleri[0] = sesSeviyeleriSlider[0].value;
            sesSeviyeleri[1] = sesSeviyeleriSlider[1].value;
            sesSeviyeleri[2] = sesSeviyeleriSlider[2].value;
            sesSeviyeleri[3] = sesSeviyeleriSlider[3].value;
        }

        if (oyunda)
        {
            for (int i = 0; i < oyunMuzikleri.Length; i++)
            {
                oyunMuzikleri[i].volume = sesSeviyeleri[0];
            }
            for (int i = 0; i < ortamSesleri.Length; i++)
            {
                ortamSesleri[i].volume = sesSeviyeleri[1];
            }
            for (int i = 0; i < sesEfektleri.Length; i++)
            {
                sesEfektleri[i].volume = sesSeviyeleri[3];
            }
        }
        if (menude)
        {
            for (int i = 0; i < menuMuzigi.Length; i++)
            {
                menuMuzigi[i].volume = sesSeviyeleri[2];
            }
            for (int i = 0; i < sesEfektleri.Length; i++)
            {
                sesEfektleri[i].volume = sesSeviyeleri[3];
            }
        }
    }

    public void sesDegerleriniKaydet()
    {
        PlayerPrefs.SetFloat("sesler0", sesSeviyeleri[0]);
        PlayerPrefs.SetFloat("sesler1", sesSeviyeleri[1]);
        PlayerPrefs.SetFloat("sesler2", sesSeviyeleri[2]);
        PlayerPrefs.SetFloat("sesler3", sesSeviyeleri[2]);
        PlayerPrefs.Save();
    }

    public void sesDegerleriniGetir()
    {
        if (PlayerPrefs.HasKey("sesler0")) sesSeviyeleri[0] = PlayerPrefs.GetFloat("sesler0");
        if (PlayerPrefs.HasKey("sesler1")) sesSeviyeleri[1] = PlayerPrefs.GetFloat("sesler1");
        if (PlayerPrefs.HasKey("sesler2")) sesSeviyeleri[2] = PlayerPrefs.GetFloat("sesler2");
        if (PlayerPrefs.HasKey("sesler3")) sesSeviyeleri[3] = PlayerPrefs.GetFloat("sesler3");
    }
}
