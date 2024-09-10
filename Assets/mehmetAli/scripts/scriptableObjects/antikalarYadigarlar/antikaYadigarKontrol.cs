using UnityEngine;
using UnityEngine.UI;

public class antikaYadigarKontrol : MonoBehaviour
{
    public antikaYadigarOzellikleri[] elindekiAntikalar, elindekiYadigarlar, tumAntikalar, tumYadigarlar;
    public Image[] antikalarImage, yadigarlarImage;
    public bool[] antikaSlotBos = new bool[3], yadigarSlotBos = new bool[3];
    public bool[] hangiAntikaAktif, hangiYadigarAktif;
    public bool yadigarDusebilir;
    public Sprite yumrukSprite;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    oyuncuHareket oyuncuHareket;
    envanterKontrol envanterKontrol;
    canKontrol canKontrol;

    void Start()
    {
        for (int i = 0; i < antikaSlotBos.Length; i++)
            antikaSlotBos[i] = true;
        for (int i = 0; i < yadigarSlotBos.Length; i++)
            yadigarSlotBos[i] = true;

        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        canKontrol = FindObjectOfType<canKontrol>();

        yumrukSprite = oyuncuSaldiriTest.yumrukSprite;
    }
    void Update()
    {
        yadigarGuncelle();
        antikaEtkisi();
        yadigarEtkisi();

        if (!hangiYadigarAktif[1])
            oyuncuSaldiriTest.hannyaEtkisi = 1f;

        if (!yadigarSlotBos[0] && !yadigarSlotBos[1])
        {
            yadigarDusebilir = false;
        }
    }
    public void antikaEtkisi()
    {
        if (elindekiAntikalar[0] != null)
            hangiAntikaVar(0);
        if (elindekiAntikalar[1] != null)
            hangiAntikaVar(1);
    }
    public void yadigarEtkisi()
    {
        if (elindekiYadigarlar[0] != null)
            hangiYadigarVar(0);
        if (elindekiYadigarlar[1] != null)
            hangiYadigarVar(1);
    }
    public void hangiAntikaVar(int hangiAntika)
    {
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[0]) // Tengu Kanatları
        {
            if (!hangiAntikaAktif[0])
            {
                Debug.Log("Tengu Kanatları");
                hangiAntikaAktif[0] = true;
                oyuncuHareket.ziplamaSayisi = 2;
            }
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[1]) // Phoenix Tüyü
        {
            if (!hangiAntikaAktif[1])
            {
                Debug.Log("Phoenix Tüyü");
                hangiAntikaAktif[1] = true;
                canKontrol.olmemeSansiVar = true;
                canKontrol.kacOlmemeSansi++;
            }
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[2]) // Tori Tılsımı
        {
            if (!hangiAntikaAktif[2])
            {
                Debug.Log("Tori Tılsımı");
                // gerekenler canKontrolde yapildi - (toriVar bool)
                hangiAntikaAktif[2] = true;
            }
        }
    }
    public void hangiYadigarVar(int hangiYadigar)
    {
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[0]) // Hannya Maskesi
        {
            if (!hangiYadigarAktif[0])
            {
                Debug.Log("Hannya Maskesi");
                oyuncuSaldiriTest.hannyaEtkisi = 0.5f;
                hangiYadigarAktif[0] = true;
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[1]) // Barut Fıçısı
        {
            if (!hangiYadigarAktif[1])
            {
                Debug.Log("Barut Fıçısı");
                // gerekenler dusmanHasarda barutFicisi gameObject yapildi
                hangiYadigarAktif[1] = true;
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[2]) // Zehirli Tüy
        {
            if (!hangiYadigarAktif[2])
            {
                Debug.Log("Zehirli Tüy");
                // can kontrolde yapildi "firlatilan" ile vurulma
                hangiYadigarAktif[2] = true;
            }
        }
    }
    public void yadigarGuncelle()
    {
        for (int i = 0; i < elindekiAntikalar.Length; i++)
        {
            if (elindekiAntikalar[i] == null)
            {
                antikaSlotBos[i] = true;
                antikalarImage[i].sprite = yumrukSprite;
            }
            else if (elindekiAntikalar[i] != null)
            {
                antikaSlotBos[i] = false;
                antikalarImage[i].sprite = elindekiAntikalar[i].antikaIcon;
            }
        }
        for (int i = 0; i < elindekiYadigarlar.Length; i++)
        {
            if (elindekiYadigarlar[i] == null)
            {
                yadigarSlotBos[i] = true;
                yadigarlarImage[i].sprite = yumrukSprite;
            }
            else if (elindekiYadigarlar[i] != null)
            {
                yadigarSlotBos[i] = false;
                yadigarlarImage[i].sprite = elindekiYadigarlar[i].yadigarIcon;
            }
        }
    }
}
