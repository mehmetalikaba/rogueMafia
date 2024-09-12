using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class antikaciPanelScripti : MonoBehaviour
{
    public GameObject daireButon;
    public bool oyuncuYakin, etkilesimKilitli, antikaSecildi, randomAntika1, randomAntika2, randomAntika3;
    public antikaYadigarOzellikleri[] secilebilecekler, seviye1Antikalar, seviye2Antikalar, seviye3Antikalar, seviye1Yadigarlar, seviye2Yadigarlar, seviye3Yadigarlar;
    public Button[] antikaButonlari, yadigarButonlari, oyuncununAntikalari;
    public Image[] antikaIconlar, yadigarIconlar;
    public Text[] antikaAdlari, antikaAciklamalari, yadigarAdlari, yadigarAciklamalari, oyuncununAntikalariAdlari;
    public GameObject oyunPaneli, antikaciPanel, yadigarSecme, antikaSecme;
    public Text antikaciDiyalog;
    public Sprite ejderParasi, aniPuani;
    public anaBaseKontrol anaBaseKontrol;
    public araBaseKontrol araBaseKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public DuraklatmaMenusu duraklatmaMenusu;
    public oyuncuHareket oyuncuHareket;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public envanterKontrol envanterKontrol;
    public int sayac, ranSayi1, ranSayi2, ranSayi3, aniEjder, yadigarDegeri, hangiYadigar, hangisiniSecti;
    public int[] kacAntika;
    public Animator[] kapakAnimler;
    public bool[] butonaBasti;
    public Button buton;

    public void Start()
    {
        anaBaseKontrol = FindObjectOfType<anaBaseKontrol>();
        araBaseKontrol = FindObjectOfType<araBaseKontrol>();
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
            daireButon.SetActive(true);
        else if (!oyuncuYakin)
            daireButon.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu"))||Input.GetKeyDown(KeyCode.JoystickButton2) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !antikaciPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && antikaciPanel.activeSelf)
                devamEt();
        }
    }
    /*public void antikalariGetir()
    {
        for (int i = 0; i < 3; i++)
        {
            if (antikaYadigarKontrol.elindekiAntikalar[i] != null)
            {
                oyuncununAntikalariAdlari[i].GetComponent<localizedText>().key = antikaYadigarKontrol.elindekiAntikalar[i].antikaAdi;
                oyuncununAntikalari[i].GetComponent<Image>().enabled = true;
                oyuncununAntikalari[i].GetComponent<Image>().sprite = antikaYadigarKontrol.elindekiAntikalar[i].antikaIcon;
                kapakAnimler[i].SetTrigger("kapakAc");
            }
        }
    }*/
    public void yadigarlariGetir()
    {
        for (int i = 0; i < 2; i++)
        {
            if (antikaYadigarKontrol.elindekiYadigarlar[i] != null)
            {
                yadigarAdlari[i].GetComponent<localizedText>().key = antikaYadigarKontrol.elindekiYadigarlar[i].yadigarAdi;
                yadigarIconlar[i].sprite = antikaYadigarKontrol.elindekiYadigarlar[i].yadigarIcon;
                yadigarAciklamalari[i].GetComponent<localizedText>().key = antikaYadigarKontrol.elindekiYadigarlar[i].yadigarAciklamaKeyi;
            }
            else if (antikaYadigarKontrol.elindekiYadigarlar[i] == null)
            {
                yadigarButonlari[i].interactable = false;
                yadigarAdlari[i].text = "Yadigar Yok";
                yadigarIconlar[i].sprite = oyuncuSaldiriTest.yumrukSprite;
                yadigarAciklamalari[i].text = "Düşmanları öldürerek yadigarları toplayabilirsin.";
            }
        }
    }
    public void yadigarSec1()
    {
        if (!antikaYadigarKontrol.yadigarSlotBos[0])
        {
            Debug.Log("yadigar 1");
            yadigarDegeri = antikaYadigarKontrol.elindekiYadigarlar[0].yadigarDegeri;
            hangisiniSecti = 0;
            hangiYadigar = 0;
            kacTaneAntikaOlacak();
        }
    }
    public void yadigarSec2()
    {
        if (!antikaYadigarKontrol.yadigarSlotBos[1])
        {
            Debug.Log("yadigar 2");
            yadigarDegeri = antikaYadigarKontrol.elindekiYadigarlar[1].yadigarDegeri;
            hangisiniSecti = 3;
            hangiYadigar = 1;
            kacTaneAntikaOlacak();
        }
    }
    /*public void yadigarSec3()
    {
        if (!antikaYadigarKontrol.yadigarSlotBos[2])
        {
            Debug.Log("yadigar 3");
            yadigarDegeri = antikaYadigarKontrol.elindekiYadigarlar[2].yadigarDegeri;
            hangisiniSecti = 6;
            hangiYadigar = 2;
            kacTaneAntikaOlacak();
        }
    }*/

    public void kacTaneAntikaOlacak()
    {
        yadigarSecme.SetActive(false);
        antikaSecme.SetActive(true);

        if (!butonaBasti[hangiYadigar])
        {
            butonaBasti[hangiYadigar] = true;
            kacAntika[hangiYadigar] = 1;
            /*
            int sayi = Random.Range(0, 100);
            if (sayi == 999)
                kacAntika[hangiYadigar] = 3;
            else if (sayi == 999)
                kacAntika[hangiYadigar] = 2;
            else if (sayi < 999)*/

            //Debug.Log("GELEN SAYI <==> " + sayi);
            randomAntikaGetir();
        }
        else
        {
            Debug.Log("BUNA ZATEN BASMISTI <==> " + butonaBasti[hangiYadigar]);
            adlarVeIconlar();
        }
    }
    public void randomAntikaGetir()
    {
        int ranSayi = Random.Range(0, seviye1Antikalar.Length);
        secilebilecekler[0] = seviye1Antikalar[ranSayi];

        /*
        for (int i = 0; i < kacAntika[hangiYadigar]; i++)
        {
            if (yadigarDegeri == 1)
            {
                Debug.Log("YADIGAR DEGERI == 1");
                int randomSayi = Random.Range(0, 100);
                if (randomSayi <= 5)
                {
                    ranSayi1 = Random.Range(0, seviye3Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye3Antikalar[ranSayi1];
                }
                else if (randomSayi <= 25)
                {
                    ranSayi2 = Random.Range(0, seviye2Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye2Antikalar[ranSayi2];
                }
                else if (randomSayi > 25)
                {
                    ranSayi3 = Random.Range(0, seviye1Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye1Antikalar[ranSayi3];
                }
            }
            if (yadigarDegeri == 2)
            {
                Debug.Log("YADIGAR DEGERI == 2");
                int randomSayi = Random.Range(0, 100);
                if (randomSayi <= 5)
                {
                    ranSayi1 = Random.Range(0, seviye3Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye3Antikalar[ranSayi1];
                }
                else if (randomSayi <= 25)
                {
                    ranSayi2 = Random.Range(0, seviye1Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye1Antikalar[ranSayi2];
                }
                else if (randomSayi > 25)
                {
                    ranSayi3 = Random.Range(0, seviye2Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye2Antikalar[ranSayi3];
                }
            }
            if (yadigarDegeri == 3)
            {
                Debug.Log("YADIGAR DEGERI == 3");
                int randomSayi = Random.Range(0, 100);
                if (randomSayi <= 5)
                {
                    ranSayi1 = Random.Range(0, seviye1Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye1Antikalar[ranSayi1];
                }
                else if (randomSayi <= 25)
                {
                    ranSayi2 = Random.Range(0, seviye2Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye2Antikalar[ranSayi2];
                }
                else if (randomSayi > 25)
                {
                    ranSayi3 = Random.Range(0, seviye3Antikalar.Length);
                    secilebilecekler[i + hangisiniSecti] = seviye3Antikalar[ranSayi3];
                }
            }
        }*/
        adlarVeIconlar();
    }

    public void adlarVeIconlar()
    {
        if (kacAntika[hangiYadigar] == 1)
        {
            Debug.Log("(kacAntika[hangiYadigar] == 1)");
            antikaAdlari[0].text = "Anı Puanı";
            antikaIconlar[0].sprite = aniPuani;
            antikaAciklamalari[0].text = "5 Anı Puanı Al!";

            antikaAdlari[0].GetComponent<localizedText>().key = secilebilecekler[0].antikaAdi;
            antikaIconlar[0].sprite = secilebilecekler[0].antikaIcon;
            antikaAciklamalari[0].GetComponent<localizedText>().key = secilebilecekler[0].antikaAciklamaKeyi;

            antikaAdlari[2].text = "Ejder Parası";
            antikaIconlar[2].sprite = ejderParasi;
            antikaAciklamalari[2].text = "500 Ejder Parası Kazan!";

        }
        else if (kacAntika[hangiYadigar] == 2)
        {
            Debug.Log("(kacAntika[hangiYadigar] == 2)");
            antikaAdlari[0 + hangisiniSecti].GetComponent<localizedText>().key = secilebilecekler[0 + hangisiniSecti].antikaAdi;
            antikaButonlari[0 + hangisiniSecti].GetComponent<Image>().enabled = true;
            antikaButonlari[0 + hangisiniSecti].GetComponent<Image>().sprite = secilebilecekler[0 + hangisiniSecti].antikaIcon;
            antikaAdlari[1 + hangisiniSecti].GetComponent<localizedText>().key = secilebilecekler[1 + hangisiniSecti].antikaAdi;
            antikaButonlari[1 + hangisiniSecti].GetComponent<Image>().enabled = true;
            antikaButonlari[1 + hangisiniSecti].GetComponent<Image>().sprite = secilebilecekler[1 + hangisiniSecti].antikaIcon;

            aniEjder = Random.Range(1, 2);
            if (aniEjder == 1)
            {
                antikaAdlari[2 + hangisiniSecti].GetComponent<localizedText>().key = "ani_puani";
                antikaButonlari[2 + hangisiniSecti].GetComponent<Image>().enabled = true;
                antikaButonlari[2 + hangisiniSecti].GetComponent<Image>().sprite = aniPuani;
            }
            else
            {
                antikaAdlari[2 + hangisiniSecti].GetComponent<localizedText>().key = "ejder_parasi";
                antikaButonlari[2 + hangisiniSecti].GetComponent<Image>().enabled = true;
                antikaButonlari[2 + hangisiniSecti].GetComponent<Image>().sprite = ejderParasi;
            }
        }
        else if (kacAntika[hangiYadigar] == 3)
        {
            Debug.Log("(kacAntika[hangiYadigar] == 3)");
            antikaAdlari[0 + hangisiniSecti].GetComponent<localizedText>().key = secilebilecekler[0 + hangisiniSecti].antikaAdi;
            antikaButonlari[0 + hangisiniSecti].GetComponent<Image>().enabled = true;
            antikaButonlari[0 + hangisiniSecti].GetComponent<Image>().sprite = secilebilecekler[0 + hangisiniSecti].antikaIcon;
            antikaAdlari[1 + hangisiniSecti].GetComponent<localizedText>().key = secilebilecekler[1 + hangisiniSecti].antikaAdi;
            antikaButonlari[1 + hangisiniSecti].GetComponent<Image>().enabled = true;
            antikaButonlari[1 + hangisiniSecti].GetComponent<Image>().sprite = secilebilecekler[1 + hangisiniSecti].antikaIcon;
            antikaAdlari[2 + hangisiniSecti].GetComponent<localizedText>().key = secilebilecekler[2 + hangisiniSecti].antikaAdi;
            antikaButonlari[2 + hangisiniSecti].GetComponent<Image>().enabled = true;
            antikaButonlari[2 + hangisiniSecti].GetComponent<Image>().sprite = secilebilecekler[2 + hangisiniSecti].antikaIcon;
        }
    }
    public void antikaSecimButonu()
    {
        GameObject tiklananButon = EventSystem.current.currentSelectedGameObject;
        string tiklananButonunAdi = tiklananButon.name;
        int hangiButon = int.Parse(tiklananButonunAdi);
        Debug.Log(hangiButon);

        hangiYadigar = hangiButon / 3;
        Debug.Log(hangiYadigar);
        antikaYadigarKontrol.elindekiYadigarlar[hangiYadigar] = null;
        antikaYadigarKontrol.yadigarAdi[hangiYadigar] = "";
        if (hangiYadigar == 1)
            hangiButon += hangiYadigar;

        if (secilebilecekler[hangiButon] != null)
        {
            if (antikaYadigarKontrol.antikaSlotBos[0])
                antikaYadigarKontrol.elindekiAntikalar[0] = secilebilecekler[hangiButon];
            else if (antikaYadigarKontrol.antikaSlotBos[1])
                antikaYadigarKontrol.elindekiAntikalar[1] = secilebilecekler[hangiButon];
            devamEt();
        }
        else
        {
            if (antikaIconlar[hangiButon].sprite == aniPuani)
                aniPuaniSecti();
            else if (antikaIconlar[hangiButon].sprite == ejderParasi)
                ejderParasiSecti();
        }
    }
    public void aniPuaniSecti()
    {
        Debug.Log("ani secti");
        envanterKontrol.aniPuani += 5;

        /*
        int antikaciAniPuani = 0;

        if (yadigarDegeri == 1)
            antikaciAniPuani = 3;
        if (yadigarDegeri == 2)
            antikaciAniPuani = 5;
        if (yadigarDegeri == 3)
            antikaciAniPuani = 7;
        
        envanterKontrol.aniPuani += antikaciAniPuani;
        */
        devamEt();
    }
    public void ejderParasiSecti()
    {
        Debug.Log("ejder secti");
        envanterKontrol.aniPuani += 500;

        /*
        int antikaciEjderParasi = 0;

        if (yadigarDegeri == 1)
            antikaciEjderParasi = 100;
        if (yadigarDegeri == 2)
            antikaciEjderParasi = 500;
        if (yadigarDegeri == 3)
            antikaciEjderParasi = 1000;

        envanterKontrol.ejderParasi += antikaciEjderParasi;
        */
        devamEt();
    }

    public void antikayaBasti()
    {
        // antikayi silme veya antikayi verip karsiliginda bir sey alma sistemi olabilir
    }

    public void durdur()
    {
        //antikalariGetir();
        yadigarlariGetir();
        //duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        antikaciPanel.SetActive(true);
        yadigarSecme.SetActive(true);
        antikaSecme.SetActive(false);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        antikaciPanel.SetActive(false);
        yadigarSecme.SetActive(false);
        antikaSecme.SetActive(false);
        oyunPaneli.SetActive(true);
        //duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
        if (antikaSecildi && antikaSecildi)
        {
            antikaciDiyalog.GetComponent<localizedText>().key = "antikaci_bitti";
            this.enabled = false;
        }
    }
}
