using UnityEngine;
using UnityEngine.UI;

public class antikaciPanelScripti : MonoBehaviour
{
    public bool oyuncuYakin, etkilesimKilitli, antikaSecildi, randomAntika1, randomAntika2, randomAntika3;
    public antikaYadigarOzellikleri[] secilebilecekler, seviye1Antikalar, seviye2Antikalar, seviye3Antikalar, seviye1Yadigarlar, seviye2Yadigarlar, seviye3Yadigarlar;
    public Button[] antikaButonlari, yadigarButonlari;
    public Text[] antikaAdlari, yadigarAdlari;
    public GameObject oyunPaneli, antikaciPanel;
    public Text antikaciDiyalog;
    public Sprite ejderParasi, aniPuani;
    public araBaseKontrol araBaseKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public DuraklatmaMenusu duraklatmaMenusu;
    public oyuncuHareket oyuncuHareket;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public envanterKontrol envanterKontrol;
    public int ranSayi1, ranSayi2, ranSayi3, aniEjder, kacAntika, yadigarDegeri;
    public Animator[] kapakAnimler;

    public void Start()
    {
        araBaseKontrol = FindObjectOfType<araBaseKontrol>();
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !antikaciPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && antikaciPanel.activeSelf)
                devamEt();
        }
    }
    public void yadigarlariGetir()
    {
        for (int i = 0; i < 3; i++)
        {
            if (antikaYadigarKontrol.yadigarObjesi[i] != null)
            {
                yadigarAdlari[i].GetComponent<localizedText>().key = antikaYadigarKontrol.yadigarObjesi[i].yadigarAdi;
                yadigarButonlari[i].GetComponent<Image>().sprite = antikaYadigarKontrol.yadigarObjesi[i].yadigarIcon;
                kapakAnimler[i].SetTrigger("kapakAc");
            }
        }
    }
    public void yadigarSec1()
    {
        if (!antikaYadigarKontrol.yadigarSlotBos[0])
        {
            yadigarDegeri = antikaYadigarKontrol.yadigarObjesi[0].yadigarDegeri;
            kacTaneAntikaOlacak(1);
        }
    }
    public void yadigarSec2()
    {
        if (!antikaYadigarKontrol.yadigarSlotBos[1])
        {
            yadigarDegeri = antikaYadigarKontrol.yadigarObjesi[1].yadigarDegeri;
            kacTaneAntikaOlacak(2);
        }
    }
    public void yadigarSec3()
    {
        if (!antikaYadigarKontrol.yadigarSlotBos[2])
        {
            yadigarDegeri = antikaYadigarKontrol.yadigarObjesi[2].yadigarDegeri;
            kacTaneAntikaOlacak(3);
        }
    }

    public void kacTaneAntikaOlacak(int hangisiniSecti)
    {
        int sayi = Random.Range(0, 100);
        if (sayi <= 5)
        {
            kacAntika = 3;
            rastgeleBelirleme(hangisiniSecti);
        }
        else if (sayi <= 25)
        {
            kacAntika = 2;
            rastgeleBelirleme(hangisiniSecti);
        }
        else if (sayi > 25)
        {
            kacAntika = 1;
            rastgeleBelirleme(hangisiniSecti);
        }
    }

    public void rastgeleBelirleme(int hangisiniSecti)
    {
        if (kacAntika == 1)
        {
            randomAntikaGetir();

            antikaAdlari[1].GetComponent<localizedText>().key = "ani_puani";
            antikaButonlari[1].GetComponent<Image>().sprite = aniPuani;
            antikaAdlari[2].GetComponent<localizedText>().key = "ejder_parasi";
            antikaButonlari[2].GetComponent<Image>().sprite = ejderParasi;
            // 1 antika gelecek
            // 1 ani gelecek
            // 1 ejder gelecek
        }
        else if (kacAntika == 2)
        {
            randomAntikaGetir();

            aniEjder = Random.Range(1, 2);
            if (aniEjder == 1)
            {
                antikaAdlari[2].GetComponent<localizedText>().key = "ani_puani";
                antikaButonlari[2].GetComponent<Image>().sprite = aniPuani;
            }
            else
            {
                antikaAdlari[2].GetComponent<localizedText>().key = "ejder_parasi";
                antikaButonlari[2].GetComponent<Image>().sprite = ejderParasi;
            }
            // 2 antika gelecek
            // ani veya ejder gelecek
        }
        else if (kacAntika == 3)
        {
            randomAntikaGetir();
            // 3 antika gelecek
        }
    }

    public void randomAntikaGetir()
    {

        for (int i = 0; i < kacAntika; i++)
        {
            if (yadigarDegeri == 1)
            {
                int randomSayi = Random.Range(0, 100);
                if (randomSayi <= 5)
                {
                    ranSayi1 = Random.Range(0, seviye3Antikalar.Length);
                    secilebilecekler[i] = seviye3Antikalar[ranSayi1];
                }
                else if (randomSayi <= 25)
                {
                    ranSayi2 = Random.Range(0, seviye2Antikalar.Length);
                    secilebilecekler[i] = seviye2Antikalar[ranSayi2];
                }
                else if (randomSayi > 25)
                {
                    ranSayi3 = Random.Range(0, seviye1Antikalar.Length);
                    secilebilecekler[i] = seviye1Antikalar[ranSayi3];
                }
            }
            if (yadigarDegeri == 2)
            {
                int randomSayi = Random.Range(0, 100);
                if (randomSayi <= 5)
                {
                    ranSayi1 = Random.Range(0, seviye3Antikalar.Length);
                    secilebilecekler[i] = seviye3Antikalar[ranSayi1];
                }
                else if (randomSayi <= 25)
                {
                    ranSayi2 = Random.Range(0, seviye1Antikalar.Length);
                    secilebilecekler[i] = seviye1Antikalar[ranSayi2];
                }
                else if (randomSayi > 25)
                {
                    ranSayi3 = Random.Range(0, seviye2Antikalar.Length);
                    secilebilecekler[i] = seviye2Antikalar[ranSayi3];
                }
            }
            if (yadigarDegeri == 3)
            {
                int randomSayi = Random.Range(0, 100);
                if (randomSayi <= 5)
                {
                    ranSayi1 = Random.Range(0, seviye1Antikalar.Length);
                    secilebilecekler[i] = seviye1Antikalar[ranSayi1];
                }
                else if (randomSayi <= 25)
                {
                    ranSayi2 = Random.Range(0, seviye2Antikalar.Length);
                    secilebilecekler[i] = seviye2Antikalar[ranSayi2];
                }
                else if (randomSayi > 25)
                {
                    ranSayi3 = Random.Range(0, seviye3Antikalar.Length);
                    secilebilecekler[i] = seviye3Antikalar[ranSayi3];
                }
            }
        }
        adlarVeIconlar();
    }

    public void adlarVeIconlar()
    {
        if (kacAntika == 1)
        {
            antikaAdlari[0].GetComponent<localizedText>().key = secilebilecekler[0].antikaAdi;
            antikaButonlari[0].GetComponent<Image>().sprite = secilebilecekler[0].antikaIcon;
        }
        else if (kacAntika == 2)
        {
            antikaAdlari[0].GetComponent<localizedText>().key = secilebilecekler[0].antikaAdi;
            antikaButonlari[0].GetComponent<Image>().sprite = secilebilecekler[0].antikaIcon;
            antikaAdlari[1].GetComponent<localizedText>().key = secilebilecekler[1].antikaAdi;
            antikaButonlari[1].GetComponent<Image>().sprite = secilebilecekler[1].antikaIcon;
        }
        else if (kacAntika == 3)
        {
            antikaAdlari[0].GetComponent<localizedText>().key = secilebilecekler[0].antikaAdi;
            antikaButonlari[0].GetComponent<Image>().sprite = secilebilecekler[0].antikaIcon;
            antikaAdlari[1].GetComponent<localizedText>().key = secilebilecekler[1].antikaAdi;
            antikaButonlari[1].GetComponent<Image>().sprite = secilebilecekler[1].antikaIcon;
            antikaAdlari[2].GetComponent<localizedText>().key = secilebilecekler[2].antikaAdi;
            antikaButonlari[2].GetComponent<Image>().sprite = secilebilecekler[2].antikaIcon;
        }
    }


    public void antikaSecimButonu1()
    {
        int hangiButon = 0;

        if (gameObject.name == "antikaButon1")
            hangiButon = 0;
        if (gameObject.name == "antikaButon2")
            hangiButon = 1;
        if (gameObject.name == "antikaButon3")
            hangiButon = 2;

        if (secilebilecekler[hangiButon] != null)
        {
            if (antikaYadigarKontrol.antikaSlotBos[0])
                antikaYadigarKontrol.antikaObjesi[0] = secilebilecekler[hangiButon];
            else if (antikaYadigarKontrol.antikaSlotBos[1])
                antikaYadigarKontrol.antikaObjesi[1] = secilebilecekler[hangiButon];
            else if (antikaYadigarKontrol.antikaSlotBos[2])
                antikaYadigarKontrol.antikaObjesi[2] = secilebilecekler[hangiButon];
        }
        else
        {
            if (antikaAdlari[0].GetComponent<localizedText>().key == "ani_puani")
            {
                int antikaciAniPuani = 0;

                if (yadigarDegeri == 1)
                    antikaciAniPuani = 3;
                if (yadigarDegeri == 2)
                    antikaciAniPuani = 5;
                if (yadigarDegeri == 3)
                    antikaciAniPuani = 7;

                envanterKontrol.aniPuani += antikaciAniPuani;
            }
            else if (antikaAdlari[0].GetComponent<localizedText>().key == "ejder_parasi")
            {
                int antikaciEjderParasi = 0;

                if (yadigarDegeri == 1)
                    antikaciEjderParasi = 100;
                if (yadigarDegeri == 2)
                    antikaciEjderParasi = 500;
                if (yadigarDegeri == 3)
                    antikaciEjderParasi = 1000;

                envanterKontrol.ejderParasi += antikaciEjderParasi;
            }
        }
    }
    public void antikaSecimButonu2()
    {
        if (secilebilecekler[1] != null)
        {
            if (antikaYadigarKontrol.antikaSlotBos[0])
                antikaYadigarKontrol.antikaObjesi[0] = secilebilecekler[1];
            else if (antikaYadigarKontrol.antikaSlotBos[1])
                antikaYadigarKontrol.antikaObjesi[1] = secilebilecekler[1];
            else if (antikaYadigarKontrol.antikaSlotBos[2])
                antikaYadigarKontrol.antikaObjesi[2] = secilebilecekler[1];
        }
    }
    public void antikaSecimButonu3()
    {
        if (secilebilecekler[2] != null)
        {
            if (antikaYadigarKontrol.antikaSlotBos[0])
                antikaYadigarKontrol.antikaObjesi[0] = secilebilecekler[2];
            else if (antikaYadigarKontrol.antikaSlotBos[1])
                antikaYadigarKontrol.antikaObjesi[1] = secilebilecekler[2];
            else if (antikaYadigarKontrol.antikaSlotBos[2])
                antikaYadigarKontrol.antikaObjesi[2] = secilebilecekler[2];
        }
    }
    public void antikaSecimIslemi(int secilenAntika, Button buton)
    {
        buton.interactable = false;

        if (antikaSecildi && antikaSecildi)
            devamEt();
    }
    public void durdur()
    {
        yadigarlariGetir();
        duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        antikaciPanel.SetActive(true);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        antikaciPanel.SetActive(false);
        oyunPaneli.SetActive(true);
        duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
        if (antikaSecildi && antikaSecildi)
        {
            if (araBaseKontrol != null)
                araBaseKontrol.alfredKonustu = true;

            antikaciDiyalog.GetComponent<localizedText>().key = "alfred_bitti";
            this.enabled = false;
        }
    }
}
