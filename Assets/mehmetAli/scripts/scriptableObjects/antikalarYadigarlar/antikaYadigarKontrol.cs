using UnityEngine;
using UnityEngine.UI;

public class antikaYadigarKontrol : MonoBehaviour
{
    public antikaYadigarOzellikleri[] elindekiAntikalar, elindekiYadigarlar, tumAntikalar, tumYadigarlar;
    public Image[] antikalarImage, yadigarlarImage;
    public bool[] antikaSlotBos = new bool[3], yadigarSlotBos = new bool[3];
    public bool[] antikaAktifMi, yadigarAktifMi;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    oyuncuHareket oyuncuHareket;
    envanterKontrol envanterKontrol;

    void Start()
    {
        for (int i = 0; i < antikaSlotBos.Length; i++)
            antikaSlotBos[i] = true;
        for (int i = 0; i < yadigarSlotBos.Length; i++)
            yadigarSlotBos[i] = true;

        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void Update()
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



    public void antikaNeYapacak()
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

    public void yadigarNeYapacak()
    {
        if (elindekiYadigarlar[0] != null)
        {

        }
        if (elindekiYadigarlar[1] != null)
        {

        }
        if (elindekiYadigarlar[2] != null)
        {

        }
    }

    public void hangiAntikaVar(int hangiAntika)
    {
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[0])
        {
            oyuncuHareket.ziplamaSayisi = 2;
            antikaAktifMi[0] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[1])
        {
            // gerekenler dusman hasarda yapildi - (antika3 bool)
            antikaAktifMi[1] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[2])
        {
            // gerekenler dusman hasarda yapildi - (antika6 bool)
            antikaAktifMi[2] = true;
        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[3])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[4])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[5])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[6])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[7])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[8])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[9])
        {

        }
        if (elindekiAntikalar[hangiAntika] == tumAntikalar[10])
        {

        }
    }

    public void hangiYadigarVar()
    {

    }
}
