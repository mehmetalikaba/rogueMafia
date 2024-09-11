using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenSilah : MonoBehaviour
{
    public GameObject yuvarlakButonu, ozellikTexti, silah1, silah2, isik;
    public silahOzellikleri dusenSilah;
    public silahKontrol silahKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public silahOzellikleriniGetir silah1OzellikleriniGetir, silah2OzellikleriniGetir;
    public SpriteRenderer spriteRenderer;
    public bool oyuncuYakin, silahiAldi;
    public float yokOlmaSuresi, dayaniklilik;
    public AudioSource aldi;
    oyuncuHareket oyuncuHareket;
    antikaYadigarKontrol antikaYadigarKontrol;
    canKontrol canKontrol;


    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();

        yokOlmaSuresi = 15f;

        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        spriteRenderer = GetComponent<SpriteRenderer>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        aldi = GameObject.Find("aldi").GetComponent<AudioSource>();

        silah1OzellikleriniGetir = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2OzellikleriniGetir = silah2.GetComponent<silahOzellikleriniGetir>();

        spriteRenderer.sprite = dusenSilah.silahIcon;

        ozellikTexti = GameObject.Find("yadigarOzelligi");
        ozellikTexti.GetComponent<Text>().text = "";
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
        {
            yuvarlakButonu.SetActive(true);
            ozellikTexti.GetComponent<Text>().text = dusenSilah.silahAdi;
            isik.SetActive(true);
        }
        else
        {
            yuvarlakButonu.SetActive(false);
            if (ozellikTexti.GetComponent<localizedText>().key == dusenSilah.silahAdi)
                ozellikTexti.GetComponent<localizedText>().key = "";
            isik.SetActive(false);
        }

        if (oyuncuYakin) isik.SetActive(true);
        else isik.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
        {
            ozellikTexti.GetComponent<localizedText>().key = "";
            if (dusenSilah.silahTuru == "yakin")
            {
                if (!oyuncuSaldiriTest.yumruk1)
                    silahKontrol.silah1YereAt();
                silah1Getir();
            }
            else if (dusenSilah.silahTuru == "menzilli")
            {
                if (!oyuncuSaldiriTest.yumruk2)
                    silahKontrol.silah2YereAt();
                silah2Getir();
            }
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
        {
            ozellikTexti.GetComponent<localizedText>().key = "";
            if (dusenSilah.silahTuru == "yakin")
            {
                if (!oyuncuSaldiriTest.yumruk1)
                    silahKontrol.silah1YereAt();
                silah1Getir();
            }
            else if (dusenSilah.silahTuru == "menzilli")
            {
                if (!oyuncuSaldiriTest.yumruk2)
                    silahKontrol.silah2YereAt();
                silah2Getir();
            }
        }


        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
        {
            if (antikaYadigarKontrol.hangiYadigarAktif[2])
            {
                /*Collider2D[] alanHasari = Physics2D.OverlapCircleAll(transform.position, 5, LayerMask.GetMask("Oyuncu"));
                for (int i = 0; i < alanHasari.Length; i++)
                {
                    if (alanHasari[i].name == "Oyuncu")
                    {
                        canKontrol = FindObjectOfType<canKontrol>();
                        canKontrol.canAzalmasi(5, "atesMuhru");
                    }
                }*/
            }
            Destroy(gameObject);
            if (ozellikTexti.GetComponent<Text>().text == dusenSilah.silahAdi)
                ozellikTexti.GetComponent<localizedText>().key = "";
        }
    }


    public void silah1Getir()
    {
        if (!silahiAldi)
        {
            silahiAldi = true;
            try
            {
                aldi.Play();
                silahKontrol.yerdenAliyor = true;
                oyuncuSaldiriTest.yumruk1 = false;
                oyuncuSaldiriTest.silahUltileri.silah1Ulti = 0f;
                silah1OzellikleriniGetir.simdikiSilah = dusenSilah.aciklamaKeyi;
                silah1OzellikleriniGetir.seciliSilahinBilgileriniGetir();
                oyuncuSaldiriTest.animator.runtimeAnimatorController = silah1OzellikleriniGetir.karakterAnimator;
                silah1OzellikleriniGetir.silahDayanikliligi = dayaniklilik;
                oyuncuSaldiriTest.silah1DayanikliligiImage.fillAmount = oyuncuSaldiriTest.silah1Script.silahDayanikliligi / oyuncuSaldiriTest.silah1Script.silahDayanikliligi;
            }
            finally
            {
                ozellikTexti.GetComponent<Text>().text = "";
                Destroy(gameObject);
            }
        }
    }

    public void silah2Getir()
    {
        if (!silahiAldi)
        {
            silahiAldi = true;
            try
            {
                aldi.Play();
                silahKontrol.yerdenAliyor = true;
                oyuncuSaldiriTest.yumruk2 = false;
                oyuncuSaldiriTest.silahUltileri.silah2Ulti = 0f;
                silah2OzellikleriniGetir.simdikiSilah = dusenSilah.aciklamaKeyi;
                silah2OzellikleriniGetir.seciliSilahinBilgileriniGetir();
                oyuncuSaldiriTest.animator.runtimeAnimatorController = silah2OzellikleriniGetir.karakterAnimator;
                silah2OzellikleriniGetir.silahDayanikliligi = dayaniklilik;
                oyuncuSaldiriTest.silah2DayanikliligiImage.fillAmount = oyuncuSaldiriTest.silah2Script.silahDayanikliligi / oyuncuSaldiriTest.silah1Script.silahDayanikliligi;
            }
            finally
            {
                ozellikTexti.GetComponent<Text>().text = "";
                Destroy(gameObject);
            }
        }
    }
}
