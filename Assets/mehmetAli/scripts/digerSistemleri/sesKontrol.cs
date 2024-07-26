using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class sesKontrol : MonoBehaviour
{
    public AudioSource[] oyunMuzikleri, ortamSesleri, menuMuzigi, sesEfektleri;
    public Slider[] sesSeviyeleriSlider;
    public float ses0, ses1, ses2, ses3;
    public kaydetKontrol kaydetKontrol;
    public bool menude, oyunda;
    public envanterVerileri envanterVerileri;

    void Awake()
    {
        jsonSesGetir();
    }

    void Start()
    {
        if (menude)
        {
            ses0 = 0.25f;
            ses1 = 0.25f;
            ses2 = 0.25f;
            ses3 = 0.25f;

            sesSeviyeleriSlider[0].value = ses0;
            sesSeviyeleriSlider[1].value = ses1;
            sesSeviyeleriSlider[2].value = ses2;
            sesSeviyeleriSlider[3].value = ses3;
        }

        sesSeviyesiniAyarla();

        if (menude)
        {
            sesSeviyeleriSlider[0].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.jsonKaydet(); });
            sesSeviyeleriSlider[1].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.jsonKaydet(); });
            sesSeviyeleriSlider[2].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.jsonKaydet(); });
            sesSeviyeleriSlider[3].onValueChanged.AddListener(delegate { sesSeviyesiniAyarla(); kaydetKontrol.jsonKaydet(); });
        }
    }
    void sesSeviyesiniAyarla()
    {
        if (menude)
        {
            ses0 = sesSeviyeleriSlider[0].value;
            ses1 = sesSeviyeleriSlider[1].value;
            ses2 = sesSeviyeleriSlider[2].value;
            ses3 = sesSeviyeleriSlider[3].value;
        }

        for (int i = 0; i < oyunMuzikleri.Length; i++)
        {
            oyunMuzikleri[i].volume = ses0;
        }
        for (int i = 0; i < ortamSesleri.Length; i++)
        {
            ortamSesleri[i].volume = ses1;
        }
        if (menude)
        {
            for (int i = 0; i < menuMuzigi.Length; i++)
            {
                menuMuzigi[i].volume = ses2;
            }
        }
        for (int i = 0; i < sesEfektleri.Length; i++)
        {
            sesEfektleri[i].volume = ses3;
        }
    }


    public void jsonSesGetir()
    {
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            envanterVerileri data = JsonUtility.FromJson<envanterVerileri>(json);

            Debug.Log("SES SEVIYELERI GELDI <==> " + path);

            ses0 = data.envanterSes0;
            ses1 = data.envanterSes1;
            ses2 = data.envanterSes2;
            ses3 = data.envanterSes3;
        }
        else
        {
            Debug.LogWarning("JSON dosyasý bulunamadý: " + path);
        }
    }

}