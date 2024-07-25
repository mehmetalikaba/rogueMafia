using UnityEngine;
using UnityEngine.UI;

public class sesKontrol : MonoBehaviour
{
    public AudioSource[] oyunMuzikleri, ortamSesleri, menuMuzigi, sesEfektleri;
    public Slider[] sesSeviyeleriSlider;
    public float[] sesSeviyeleri, bastakiSesSeviyesi;
    public kaydedilecekler kaydedilecekler;
    public bool menude, oyunda;

    void Start()
    {
        sesDegerleriniGetir();

        for (int i = 0; i < sesSeviyeleri.Length; i++)
        {
            bastakiSesSeviyesi[i] = sesSeviyeleri[i];
        }

        if (menude)
        {
            sesSeviyeleriSlider[0].value = sesSeviyeleri[0];
            sesSeviyeleriSlider[1].value = sesSeviyeleri[1];
            sesSeviyeleriSlider[2].value = sesSeviyeleri[2];
            sesSeviyeleriSlider[3].value = sesSeviyeleri[3];
        }

        sesSeviyesiniAyarla();

        if (menude)
        {
            sesSeviyeleriSlider[0].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); sesDegerleriniKaydet(); });
            sesSeviyeleriSlider[1].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); sesDegerleriniKaydet(); });
            sesSeviyeleriSlider[2].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); sesDegerleriniKaydet(); });
            sesSeviyeleriSlider[3].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); sesDegerleriniKaydet(); });
        }

    }

    void Update()
    {
        for (int i = 0; i < sesSeviyeleri.Length; i++)
        {
            if (sesSeviyeleri[i] != bastakiSesSeviyesi[i])
            {
                sesSeviyesiniAyarla();
            }
        }
    }

    void sesSeviyesiniAyarla()
    {
        if (menude)
        {
            sesSeviyeleri[0] = sesSeviyeleriSlider[0].value;
            sesSeviyeleri[1] = sesSeviyeleriSlider[1].value;
            sesSeviyeleri[2] = sesSeviyeleriSlider[2].value;
            sesSeviyeleri[3] = sesSeviyeleriSlider[3].value;
        }

        for (int i = 0; i < oyunMuzikleri.Length; i++)
        {
            oyunMuzikleri[i].volume += oyunMuzikleri[i].volume * (sesSeviyeleri[0] - bastakiSesSeviyesi[0]);
        }
        for (int i = 0; i < ortamSesleri.Length; i++)
        {
            ortamSesleri[i].volume += ortamSesleri[i].volume * (sesSeviyeleri[1] - bastakiSesSeviyesi[1]);
        }
        if (menude)
        {
            for (int i = 0; i < menuMuzigi.Length; i++)
            {
                menuMuzigi[i].volume += menuMuzigi[i].volume * (sesSeviyeleri[2] - bastakiSesSeviyesi[2]);
            }
        }
        for (int i = 0; i < sesEfektleri.Length; i++)
        {
            sesEfektleri[i].volume += sesEfektleri[i].volume * (sesSeviyeleri[3] - bastakiSesSeviyesi[3]);
        }

        for (int i = 0; i < sesSeviyeleri.Length; i++)
        {
            bastakiSesSeviyesi[i] = sesSeviyeleri[i];
        }

    }
    public void sesDegerleriniKaydet()
    {
        kaydedilecekler.sesSeviyeleri[0] = sesSeviyeleri[0];
        kaydedilecekler.sesSeviyeleri[1] = sesSeviyeleri[1];
        kaydedilecekler.sesSeviyeleri[2] = sesSeviyeleri[2];
        kaydedilecekler.sesSeviyeleri[3] = sesSeviyeleri[3];
    }
    public void sesDegerleriniGetir()
    {
        sesSeviyeleri[0] = kaydedilecekler.sesSeviyeleri[0];
        sesSeviyeleri[1] = kaydedilecekler.sesSeviyeleri[1];
        sesSeviyeleri[2] = kaydedilecekler.sesSeviyeleri[2];
        sesSeviyeleri[3] = kaydedilecekler.sesSeviyeleri[3];
    }
}


/*
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class sesKontrol : MonoBehaviour
{
    public AudioSource[] oyunMuzikleri, ortamSesleri, menuMuzigi, sesEfektleri;
    public Slider[] sesSeviyeleriSlider;
    public float[] sesSeviyeleri;
    public bool menude, oyunda;
    public kaydedilecekler kaydedilecekler;
    public AudioSource[] sahnedekiSesler;
    public GameObject sesPaneli;

    void Start()
    {
        sesDegerleriniAyarla();

        if (menude && sesPaneli.activeSelf)
        {
            //sesDegerleriniGetir();

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
        if (menude && sesPaneli.activeSelf)
            sesDegerleriniKaydet();
    }

    void sesDegerleriniAyarla()
    {
        sesSeviyeleri[0] = sesSeviyeleriSlider[0].value;
        sesSeviyeleri[1] = sesSeviyeleriSlider[1].value;
        sesSeviyeleri[2] = sesSeviyeleriSlider[2].value;
        sesSeviyeleri[3] = sesSeviyeleriSlider[3].value;

        for (int i = 0; i < oyunMuzikleri.Length; i++)
        {
            oyunMuzikleri[i].volume = sesSeviyeleri[0];
        }
        for (int i = 0; i < ortamSesleri.Length; i++)
        {
            ortamSesleri[i].volume = sesSeviyeleri[1];
        }
        for (int i = 0; i < menuMuzigi.Length; i++)
        {
            menuMuzigi[i].volume = sesSeviyeleri[2];
        }
        for (int i = 0; i < sesEfektleri.Length; i++)
        {
            sesEfektleri[i].volume = sesSeviyeleri[3];
        }
    }

    public void sesDegerleriniKaydet()
    {
        kaydedilecekler.sesSeviyeleri[0] = sesSeviyeleri[0];
        kaydedilecekler.sesSeviyeleri[1] = sesSeviyeleri[1];
        kaydedilecekler.sesSeviyeleri[2] = sesSeviyeleri[2];
        kaydedilecekler.sesSeviyeleri[3] = sesSeviyeleri[3];
    }

    public void sesDegerleriniGetir()
    {
        sesSeviyeleri[0] = kaydedilecekler.sesSeviyeleri[0];
        sesSeviyeleri[1] = kaydedilecekler.sesSeviyeleri[1];
        sesSeviyeleri[2] = kaydedilecekler.sesSeviyeleri[2];
        sesSeviyeleri[3] = kaydedilecekler.sesSeviyeleri[3];
    }
}
 
 */