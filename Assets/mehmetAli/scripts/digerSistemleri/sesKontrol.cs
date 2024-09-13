using UnityEngine;
using UnityEngine.UI;

public class sesKontrol : MonoBehaviour
{
    public AudioSource[] oyunMuzikleri, ortamSesleri, menuMuzigi, sesEfektleri;
    public Slider[] sesSeviyeleriSlider;
    public float[] sesSeviyeleri = new float[4];
    public kaydetKontrol kaydetKontrol;
    public bool menude, oyunda;

    private float[] oyunMuzikleriBaslangic;
    private float[] ortamSesleriBaslangic;
    private float[] menuMuzigiBaslangic;
    private float[] sesEfektleriBaslangic;

    void Start()
    {
        oyunMuzikleriBaslangic = new float[oyunMuzikleri.Length];
        for (int i = 0; i < oyunMuzikleri.Length; i++)
        {
            oyunMuzikleriBaslangic[i] = oyunMuzikleri[i].volume;
        }

        ortamSesleriBaslangic = new float[ortamSesleri.Length];
        for (int i = 0; i < ortamSesleri.Length; i++)
        {
            ortamSesleriBaslangic[i] = ortamSesleri[i].volume;
        }

        if (menude)
        {
            menuMuzigiBaslangic = new float[menuMuzigi.Length];
            for (int i = 0; i < menuMuzigi.Length; i++)
            {
                menuMuzigiBaslangic[i] = menuMuzigi[i].volume;
            }
        }

        float[] sesEfektleriBaslangic = new float[sesEfektleri.Length];
        for (int i = 0; i < sesEfektleri.Length; i++)
        {
            if (sesEfektleri[i] != null)  // sesEfektleri dizisinin elemanlarının null olup olmadığını kontrol et
            {
                sesEfektleriBaslangic[i] = sesEfektleri[i].volume;  // volume özelliğini al
            }
            else
            {
                sesEfektleriBaslangic[i] = 0f;  // Eleman null ise varsayılan bir değer belirle
            }
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
            sesSeviyeleriSlider[0].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.kaydetKontrolSes.jsonSesKaydet(); });
            sesSeviyeleriSlider[1].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.kaydetKontrolSes.jsonSesKaydet(); });
            sesSeviyeleriSlider[2].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.kaydetKontrolSes.jsonSesKaydet(); });
            sesSeviyeleriSlider[3].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.kaydetKontrolSes.jsonSesKaydet(); });
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
            oyunMuzikleri[i].volume = oyunMuzikleriBaslangic[i] * sesSeviyeleri[0];
        }
        for (int i = 0; i < ortamSesleri.Length; i++)
        {
            ortamSesleri[i].volume = ortamSesleriBaslangic[i] * sesSeviyeleri[1];
        }
        if (menude)
        {
            for (int i = 0; i < menuMuzigi.Length; i++)
            {
                menuMuzigi[i].volume = menuMuzigiBaslangic[i] * sesSeviyeleri[2];
            }
        }
        for (int i = 0; i < sesEfektleri.Length; i++)
        {
            // sesEfektleri[i] null değilse ve sesEfektleriBaslangic[i] ve sesSeviyeleri[3] geçerli bir değer içeriyorsa
            if (sesEfektleri[i] != null && sesEfektleriBaslangic != null && sesSeviyeleri != null)
            {
                sesEfektleri[i].volume = sesEfektleriBaslangic[i] * sesSeviyeleri[3];
            }
        }
    }
}
