using UnityEngine;
using UnityEngine.UI;

public class antikaYadigarKontrol : MonoBehaviour
{
    public string[] dusenYadigarlar = new string[3];
    public string[] antikaAdi = new string[3], yadigarAdi = new string[3];
    public antikaYadigarOzellikleri[] elindekiAntikalar, elindekiYadigarlar, tumAntikalar, tumYadigarlar;
    public Image[] antikalarImage, yadigarlarImage;
    public bool[] antikaSlotBos = new bool[3], yadigarSlotBos = new bool[3];
    public bool[] hangiAntikaAktif, hangiYadigarAktif;
    public bool yadigarDusebilir, kontrollerAcik;
    public Sprite yumrukSprite;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    oyuncuHareket oyuncuHareket;
    envanterKontrol envanterKontrol;
    canKontrol canKontrol;

    void Start()
    {
        kontrollerAcik = true;
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
        if (kontrollerAcik)
        {
            yadigarGuncelle();
            antikaEtkisi();
            yadigarEtkisi();
        }

        if (!hangiYadigarAktif[1])
            oyuncuSaldiriTest.hannyaEtkisi = 1f;

        if (!yadigarSlotBos[0] && !yadigarSlotBos[1])
        {
            yadigarDusebilir = false;
        }
    }
    public void antikaEtkisi()
    {
        if (elindekiAntikalar[0] != null || antikaAdi[0] != "")
            hangiAntikaVar(0);
        if (elindekiAntikalar[1] != null || antikaAdi[1] != "")
            hangiAntikaVar(1);
    }
    public void yadigarEtkisi()
    {
        if (elindekiYadigarlar[0] != null || yadigarAdi[0] != "")
            hangiYadigarVar(0);
        if (elindekiYadigarlar[1] != null || yadigarAdi[1] != "")
            hangiYadigarVar(1);
    }
    public void hangiAntikaVar(int hangiAntika)
    {
        if (antikaAdi[hangiAntika] == tumAntikalar[0].antikaAdi) // Tengu Kanatları
        {
            if (!hangiAntikaAktif[0])
            {
                Debug.Log("Tengu Kanatları");
                antikaAdi[hangiAntika] = tumAntikalar[0].antikaAdi;
                hangiAntikaAktif[0] = true;
                oyuncuHareket.ziplamaSayisi = 2;
            }
        }
        if (antikaAdi[hangiAntika] == tumAntikalar[1].antikaAdi) // Phoenix Tüyü
        {
            if (!hangiAntikaAktif[1])
            {
                Debug.Log("Phoenix Tüyü");
                antikaAdi[hangiAntika] = tumAntikalar[1].antikaAdi;
                hangiAntikaAktif[1] = true;
                canKontrol.olmemeSansiVar = true;
                canKontrol.kacOlmemeSansi++;
            }
        }
        if (antikaAdi[hangiAntika] == tumAntikalar[2].antikaAdi) // Tori Tılsımı
        {
            if (!hangiAntikaAktif[2])
            {
                Debug.Log("Tori Tılsımı");
                antikaAdi[hangiAntika] = tumAntikalar[2].antikaAdi;
                // gerekenler canKontrolde yapildi - (toriVar bool)
                hangiAntikaAktif[2] = true;
            }
        }
    }
    public void hangiYadigarVar(int hangiYadigar)
    {
        if (yadigarAdi[hangiYadigar] == tumYadigarlar[0].yadigarAdi) // Hannya Maskesi
        {
            if (!hangiYadigarAktif[0])
            {
                Debug.Log("Hannya Maskesi");
                yadigarAdi[hangiYadigar] = tumYadigarlar[0].yadigarAdi;
                oyuncuSaldiriTest.hannyaEtkisi = 0.5f;
                hangiYadigarAktif[0] = true;
            }
        }
        if (yadigarAdi[hangiYadigar] == tumYadigarlar[1].yadigarAdi) // Barut Fıçısı
        {
            if (!hangiYadigarAktif[1])
            {
                Debug.Log("Barut Fıçısı");
                yadigarAdi[hangiYadigar] = tumYadigarlar[1].yadigarAdi;
                // gerekenler dusmanHasarda barutFicisi gameObject yapildi
                hangiYadigarAktif[1] = true;
            }
        }
        if (yadigarAdi[hangiYadigar] == tumYadigarlar[2].yadigarAdi) // Zehirli Tüy
        {
            if (!hangiYadigarAktif[2])
            {
                Debug.Log("Zehirli Tüy");
                yadigarAdi[hangiYadigar] = tumYadigarlar[2].yadigarAdi;
                // can kontrolde yapildi "firlatilan" ile vurulma
                hangiYadigarAktif[2] = true;
            }
        }
    }
    public void yadigarGuncelle()
    {
        for (int i = 0; i < elindekiAntikalar.Length; i++)
        {
            if (antikaAdi[i] == "")
            {
                if (!antikaSlotBos[i])
                {
                    elindekiAntikalar[i] = null;
                    antikaSlotBos[i] = true;
                    antikalarImage[i].sprite = yumrukSprite;
                }
            }
            else
            {
                antikaSlotBos[i] = false;
                if (elindekiAntikalar[i] == null)
                {
                    for (int b = 0; b < tumAntikalar.Length; b++)
                    {
                        if (tumAntikalar[b].antikaAdi == antikaAdi[i])
                        {
                            elindekiAntikalar[i] = tumAntikalar[b];
                            antikalarImage[i].sprite = elindekiAntikalar[i].antikaIcon;
                            break;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < elindekiYadigarlar.Length; i++)
        {
            if (yadigarAdi[i] == "")
            {
                if (elindekiYadigarlar[i] != null || !yadigarSlotBos[i])
                {
                    elindekiYadigarlar[i] = null;
                    yadigarSlotBos[i] = true;
                    yadigarlarImage[i].sprite = yumrukSprite;
                }
            }
            else
            {
                yadigarSlotBos[i] = false;

                if (elindekiYadigarlar[i] == null)
                {
                    for (int b = 0; b < tumYadigarlar.Length; b++)
                    {
                        if (tumYadigarlar[b].yadigarAdi == yadigarAdi[i])
                        {
                            elindekiYadigarlar[i] = tumYadigarlar[b];
                            yadigarlarImage[i].sprite = elindekiYadigarlar[i].yadigarIcon;
                            break;
                        }
                    }
                }
            }
        }
    }
}
