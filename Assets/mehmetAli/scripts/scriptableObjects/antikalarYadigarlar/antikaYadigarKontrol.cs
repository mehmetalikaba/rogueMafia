using UnityEngine;
using UnityEngine.UI;

public class antikaYadigarKontrol : MonoBehaviour
{
    public antikaYadigarOzellikleri[] elindekiAntikalar, elindekiYadigarlar, tumAntikalar, tumYadigarlar;
    public Image[] antikalarImage, yadigarlarImage;
    public bool[] antikaSlotBos = new bool[3], yadigarSlotBos = new bool[3];
    public bool[] hangiAntikaAktif, hangiYadigarAktif;
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
    }
    void Update()
    {
        yadigarGuncelle();
        antikaEtkisi();
        yadigarEtkisi();

        if (!hangiYadigarAktif[1])
            oyuncuSaldiriTest.hannyaEtkisi = 1f;
    }
    public void antikaEtkisi()
    {
        if (elindekiAntikalar[0] != null)
        {
            hangiAntikaVar(0);
        }
        if (elindekiAntikalar[1] != null)
        {
            hangiAntikaVar(1);
        }
        if (elindekiAntikalar[2] != null)
        {
            hangiAntikaVar(2);
        }
    }
    public void yadigarEtkisi()
    {
        if (elindekiYadigarlar[0] != null)
        {
            hangiYadigarVar(0);
        }
        if (elindekiYadigarlar[1] != null)
        {
            hangiYadigarVar(1);
        }
        if (elindekiYadigarlar[2] != null)
        {
            hangiYadigarVar(2);
        }
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
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[1]) // Yıldırım Yayı
        {
            if (!hangiAntikaAktif[1])
            {
                Debug.Log("Yıldırım Yayı");
                hangiAntikaAktif[1] = true; // HENUZ YAPILMADI
            }
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[2]) // Phoenix Tüyü
        {
            if (!hangiAntikaAktif[2])
            {
                Debug.Log("Phoenix Tüyü");
                hangiAntikaAktif[2] = true;
                canKontrol.olmemeSansiVar = true;
                canKontrol.kacOlmemeSansi++;
            }
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[3]) // İblis Pençesi
        {
            if (!hangiAntikaAktif[3])
            {
                Debug.Log("İblis Pençesi");
                hangiAntikaAktif[3] = true; // HENUZ YAPILMADI
            }
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[4]) // Tütsü Çanağı
        {
            if (!hangiAntikaAktif[4])
            {
                Debug.Log("Tütsü Çanağı");
                // gerekenler oyuncuSaldiriTestte yapildi - (alanHasariVer())
                hangiAntikaAktif[4] = true;
            }
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[5]) // Tori Tılsımı
        {
            if (!hangiAntikaAktif[5])
            {
                Debug.Log("Tori Tılsımı");
                // gerekenler canKontrolde yapildi - (toriVar bool)
                hangiAntikaAktif[5] = true;
            }
        }
        /*
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[0]) // Tengu Kanatları
        {
            hangiAntikaAktif[0] = true;
            oyuncuHareket.ziplamaSayisi = 2;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[1]) // Buz Ejderhası Gözyaşı
        {
            hangiAntikaAktif[1] = true;
            // gerekenler dusman hasarda yapildi - (antika3 bool)
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[2]) // Yıldırım Yayı
        {
            hangiAntikaAktif[2] = true;
            // gerekenler dusman hasarda yapildi - (antika6 bool)
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[3]) // Phoenix Tüyü
        {
            hangiAntikaAktif[3] = true;
            canKontrol.olmemeSansiVar = true;
            canKontrol.kacOlmemeSansi++;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[4]) // Kintsugi Kalkanı
        {
            hangiAntikaAktif[4] = true;
            oyuncuSaldiriTest.silah1DayanikliligiBonus = 1000f;
            oyuncuSaldiriTest.silah2DayanikliligiBonus = 1000f;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[5]) // Kintaro'nun Cevheri
        {
            hangiAntikaAktif[5] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[6]) // Shinigami'nin Dokunuşu
        {
            hangiAntikaAktif[6] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[7]) // Amaterasu'nun Aynası
        {
            hangiAntikaAktif[7] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[8]) // Kayıp Ruh Parşömeni
        {
            hangiAntikaAktif[8] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[9]) // İblis Pençesi
        {
            hangiAntikaAktif[9] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[10]) // Kirinin Koruması
        {
            hangiAntikaAktif[10] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[11]) // Hayalet Prizma
        {
            hangiAntikaAktif[11] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[12]) // Ahşap Lotus Oyması
        {
            hangiAntikaAktif[12] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[13]) // Yeşim Ejderha Saati
        {
            hangiAntikaAktif[13] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[14]) // Dönüşüm Sandığı
        {
            hangiAntikaAktif[14] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[15]) // Öfke Küresi
        {
            hangiAntikaAktif[15] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[16]) // Tütsü Çanağı
        {
            hangiAntikaAktif[16] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[17]) // Sakura Heykeli
        {
            hangiAntikaAktif[17] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[18]) // Buz Totemi
        {
            hangiAntikaAktif[18] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[19]) // Tori Tılsımı
        {
            hangiAntikaAktif[19] = true;
        }*/
    }

    public void hangiYadigarVar(int hangiYadigar)
    {
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[0]) // Gölgelerin Mızrağı
        {
            if (!hangiYadigarAktif[0])
            {
                Debug.Log("Gölgelerin Mızrağı");
                canKontrol.sonHasarAl((canKontrol.can / 2), "golgelerinMizragi");
                hangiYadigarAktif[0] = true;
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[1]) // Hannya Maskesi
        {
            if (!hangiYadigarAktif[1])
            {
                Debug.Log("Hannya Maskesi");
                oyuncuSaldiriTest.hannyaEtkisi = 0.5f;
                hangiYadigarAktif[1] = true;
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[2]) // Ateş Mührü
        {
            if (!hangiYadigarAktif[2])
            {
                Debug.Log("Ateş Mührü");
                // gerekenler dusecek olan objelerde yapildi
                hangiYadigarAktif[2] = true;
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[3]) // Barut Fıçısı
        {
            if (!hangiYadigarAktif[3])
            {
                Debug.Log("Barut Fıçısı");
                // gerekenler dusmanHasarda barutFicisi gameObject yapildi
                hangiYadigarAktif[3] = true;
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[4]) // Karanlık Çeleng
        {
            if (!hangiYadigarAktif[4])
            {
                Debug.Log("Karanlık Çeleng");
                hangiYadigarAktif[4] = true; // HENUZ YAPILMADI
            }
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[5]) // Zehirli Tüy
        {
            if (!hangiYadigarAktif[5])
            {
                Debug.Log("Zehirli Tüy");
                // can kontrolde yapildi "firlatilan" ile vurulma
                hangiYadigarAktif[5] = true; 
            }
        }

        /*
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[0]) // Gölgelerin Mızrağı
        {
            hangiYadigarAktif[0] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[1]) // Kara Parşömen
        {
            hangiYadigarAktif[1] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[2]) // Yorgunluk İksiri
        {
            hangiYadigarAktif[2] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[3]) // Köz Bileklik
        {
            hangiYadigarAktif[3] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[4]) // Buz Tacı
        {
            hangiYadigarAktif[4] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[5]) // Hannya Maskesi
        {
            hangiYadigarAktif[5] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[6]) // Felç Getiren Eldiven
        {
            hangiYadigarAktif[6] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[7]) // Kara Sandogasa
        {
            hangiYadigarAktif[7] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[8]) // Yırtık Çuval
        {
            hangiYadigarAktif[8] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[9]) // Kötü Şans Bilekliği
        {
            hangiYadigarAktif[9] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[10]) // Tengu’nun Gagası
        {
            hangiYadigarAktif[10] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[11]) // Çöküş Yüzüğü
        {
            hangiYadigarAktif[11] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[12]) // Kara Büyü Parşömeni
        {
            hangiYadigarAktif[12] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[13]) // Kanlı Maske
        {
            hangiYadigarAktif[13] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[14]) // Sönmeyen Mumluk
        {
            hangiYadigarAktif[14] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[15]) // Ateş Mührü
        {
            hangiYadigarAktif[15] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[16]) // Tutsak Yüzükleri
        {
            hangiYadigarAktif[16] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[17]) // Barut Fıçısı
        {
            hangiYadigarAktif[17] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[18]) // Göz Kamaştıran Taş
        {
            hangiYadigarAktif[18] = true;
        }
        if (elindekiYadigarlar[hangiYadigar] == tumYadigarlar[19]) // Değersiz Miğfer
        {
            hangiYadigarAktif[19] = true;
        }*/
    }

    public void yadigarGuncelle()
    {
        if (elindekiAntikalar[0] != null)
        {
            antikaSlotBos[0] = false;
            antikalarImage[0].sprite = elindekiAntikalar[0].antikaIcon;
        }
        if (elindekiAntikalar[1] != null)
        {
            antikaSlotBos[1] = false;
            antikalarImage[1].sprite = elindekiAntikalar[1].antikaIcon;
        }
        if (elindekiAntikalar[2] != null)
        {
            antikaSlotBos[2] = false;
            antikalarImage[2].sprite = elindekiAntikalar[2].antikaIcon;
        }
        if (elindekiYadigarlar[0] != null)
        {
            yadigarSlotBos[0] = false;
            yadigarlarImage[0].sprite = elindekiYadigarlar[0].yadigarIcon;
        }
        if (elindekiYadigarlar[1] != null)
        {
            yadigarSlotBos[1] = false;
            yadigarlarImage[1].sprite = elindekiYadigarlar[1].yadigarIcon;
        }
        if (elindekiYadigarlar[2] != null)
        {
            yadigarSlotBos[2] = false;
            yadigarlarImage[2].sprite = elindekiYadigarlar[2].yadigarIcon;
        }
    }
}
