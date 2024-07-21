using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SesKontrol : MonoBehaviour
{
    public AudioSource[] oyunMuzikleri, menuMuzigi, sesEfektleri, ortamSesleri;
    public Slider[] sesSeviyeleriSlider;
    public float[] sesSeviyeleri;

    void Start()
    {
        SesSeviyesiniAyarla();

        sesSeviyeleriSlider[0].onValueChanged.AddListener(delegate { SesSeviyesiniAyarla(); });
        sesSeviyeleriSlider[1].onValueChanged.AddListener(delegate { SesSeviyesiniAyarla(); });
        sesSeviyeleriSlider[2].onValueChanged.AddListener(delegate { SesSeviyesiniAyarla(); });
        sesSeviyeleriSlider[3].onValueChanged.AddListener(delegate { SesSeviyesiniAyarla(); });
    }

    void SesSeviyesiniAyarla()
    {

        sesSeviyeleri[0] = sesSeviyeleriSlider[0].value;
        sesSeviyeleri[1] = sesSeviyeleriSlider[1].value;
        sesSeviyeleri[2] = sesSeviyeleriSlider[2].value;
        sesSeviyeleri[3] = sesSeviyeleriSlider[3].value;


        for (int i = 0; i < oyunMuzikleri.Length; i++)
        {
            oyunMuzikleri[i].volume = sesSeviyeleri[0];
        }
        for (int i = 0; i < menuMuzigi.Length; i++)
        {
            menuMuzigi[i].volume = sesSeviyeleri[1];
        }
        for (int i = 0; i < sesEfektleri.Length; i++)
        {
            sesEfektleri[i].volume = sesSeviyeleri[2];
        }
        for (int i = 0; i < ortamSesleri.Length; i++)
        {
            ortamSesleri[i].volume = sesSeviyeleri[3];
        }

    }
}
