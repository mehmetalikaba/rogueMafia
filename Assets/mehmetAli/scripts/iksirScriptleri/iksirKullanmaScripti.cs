using UnityEngine;
using UnityEngine.UI;

public class iksirKullanmaScripti : MonoBehaviour
{
    public iksirOzellikleri[] tumIksirler;
    public iksirOzellikleri eldekiIksir;
    public string iksirAdi, iksirAciklamaKeyi, simdikiIksir, kullanilanIksir;
    public float simdikiIksirSuresi, kalanIksirSuresi, ilkCan, sonCan, artanCan;
    public GameObject iksirEtkiSuresiBG, dusecekOlanIksir;
    public bool iksirOzelliginiKullandi, canObjesiAktif, pozisyonBelirlendi;
    public Image iksirImage, iksirEtkiImage;
    public AudioSource iksirActi, iksirEtkisi;
    public RectTransform rectTransform;
    private Vector3 originalPosition;
    canKontrol canKontrol;
    oyuncuHareket oyuncuHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        originalPosition = rectTransform.anchoredPosition;
    }
    void Update()
    {
        if (eldekiIksir != null && eldekiIksir.iksirAdi != simdikiIksir)
        {
            simdikiIksir = eldekiIksir.iksirAdi;
            iksirAdi = eldekiIksir.iksirAdi;
            iksirAciklamaKeyi = eldekiIksir.iksirAciklamaKeyi;
            iksirImage.sprite = eldekiIksir.iksirIcon;
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("rTusu")) && !iksirOzelliginiKullandi && !canKontrol.canArtiyor)
            iksirKullanma();
        if (Input.GetKeyDown(KeyCode.JoystickButton6) && !iksirOzelliginiKullandi && !canKontrol.canArtiyor)
            iksirKullanma();

        if (iksirOzelliginiKullandi)
        {
            // eger oyuncu, canObjesinin kattýðýndan daha fazla hasar alýrsa, iksir anında kaybolur.
            if (canObjesiAktif)
                if (canKontrol.canIksiriKatkisi <= 0)
                    iksirBitti();
            // eger oyuncu, canObjesinin kattýðýndan daha fazla hasar alýrsa, iksir anında kaybolur.

            kalanIksirSuresi -= Time.deltaTime;
            iksirEtkiImage.fillAmount = kalanIksirSuresi / simdikiIksirSuresi;
            if (kalanIksirSuresi <= 0)
                iksirBitti();
        }
    }
    public void iksirKullanma()
    {
        if (eldekiIksir != null)
        {
            if (iksirAdi == "bagisiklik_iksiri")
            {
                canKontrol.bagisiklikIksiriAktif = true;
                iksirKullandi();
            }
            if (iksirAdi == "can_iksiri" && canKontrol.can < canKontrol.baslangicCani - 25)
            {
                canKontrol.canIksiriKatkisi = 25f;
                canObjesiAktif = true;
                canKontrol.canIksiriAktif = true;
                iksirKullandi();
            }
            if (iksirAdi == "dayaniklilik_iksiri")
            {
                canKontrol.dayaniklilikIksiriAktif = true;
                iksirKullandi();
            }
            if (iksirAdi == "hareket_hizi_iksiri")
            {
                oyuncuHareket.hareketHizObjesiAktif = true;
                canKontrol.hareketHiziIksiriAktif = true;
                iksirKullandi();
            }
            if (iksirAdi == "hasar_iksiri")
            {
                oyuncuSaldiriTest.hasarObjesiAktif = true;
                canKontrol.hareketHiziIksiriAktif = true;
                iksirKullandi();
            }
            if (iksirAdi == "ziplama_iksiri")
            {
                canKontrol.ziplamaIksiriAktif = true;
                oyuncuHareket.ziplamaGucuBonus = 1.25f;
                iksirKullandi();
            }
        }
    }
    public void iksirKullandi()
    {
        iksirOzelliginiKullandi = true;
        simdikiIksirSuresi = eldekiIksir.iksirSuresi;
        iksirEtkiSuresiBG.SetActive(true);
        iksirActi.Play();
        kullanilanIksir = eldekiIksir.iksirAdi;
        kalanIksirSuresi = eldekiIksir.iksirSuresi;
        iksirImage.sprite = oyuncuSaldiriTest.yumrukSprite;
        eldekiIksir = null;
        iksirEtkisi.Play();
    }
    public void iksirBitti()
    {
        iksirEtkiSuresiBG.SetActive(false);
        if (kullanilanIksir == "can_iksiri")
        {
            canKontrol.canIksiriKatkisi = 0f;
            canKontrol.canIksiriBari.fillAmount = 0f;
            pozisyonBelirlendi = false;
            rectTransform.anchoredPosition = originalPosition;
        }
        if (kullanilanIksir == "ziplama_iksiri")
            oyuncuHareket.ziplamaGucuBonus = 1f;

        kalanIksirSuresi = 0f;
        canObjesiAktif = false;
        canKontrol.canIksiriAktif = false;
        canKontrol.dayaniklilikIksiriAktif = false;
        canKontrol.hareketHiziIksiriAktif = false;
        canKontrol.hasarIksiriAktif = false;
        canKontrol.bagisiklikIksiriAktif = false;
        canKontrol.ziplamaIksiriAktif = false;
        oyuncuHareket.hareketHizObjesiAktif = false;
        oyuncuSaldiriTest.hasarObjesiAktif = false;
        iksirOzelliginiKullandi = false;
    }

    public void iksirler()
    {
        if (canObjesiAktif)
        {
            float toplamCan = canKontrol.can + canKontrol.canIksiriKatkisi;
            canKontrol.canText.text = toplamCan.ToString("F0") + "/" + canKontrol.baslangicCani.ToString("F0");

            if (!pozisyonBelirlendi)
            {
                pozisyonBelirlendi = true;
                float xDegeri = (canKontrol.can / canKontrol.baslangicCani) * 100 * 1.28f;
                rectTransform.anchoredPosition = new Vector2(xDegeri, 0f);
            }

            if (toplamCan > canKontrol.baslangicCani)
            {
                canKontrol.baslangicCani = toplamCan;
                canKontrol.canIksiriBari.fillAmount = (canKontrol.baslangicCani - canKontrol.can) / canKontrol.baslangicCani;
            }
            else
                canKontrol.canIksiriBari.fillAmount = canKontrol.canIksiriKatkisi / canKontrol.baslangicCani;
        }
        else if (canKontrol.dayaniklilikIksiriAktif)
        {
            canKontrol.canBari.color = Color.gray;
            canKontrol.damar.color = Color.gray;
        }
        else if (canKontrol.hasarIksiriAktif)
        {
            canKontrol.canBari.color = Color.magenta;
            canKontrol.damar.color = Color.magenta;
        }
        else if (canKontrol.hareketHiziIksiriAktif)
        {
            canKontrol.canBari.color = Color.blue;
            canKontrol.damar.color = Color.blue;
        }
        else if (canKontrol.ziplamaIksiriAktif)
        {
            canKontrol.canBari.color = Color.green;
            canKontrol.damar.color = Color.green;
        }
        else if (canKontrol.bagisiklikIksiriAktif)
        {
            canKontrol.canBari.color = Color.yellow;
            canKontrol.damar.color = Color.yellow;
        }
    }
    public void iksirBirak()
    {
        dusecekOlanIksir.GetComponent<rastgeleDusenIksir>().seciliIksir = eldekiIksir;
        dusecekOlanIksir.GetComponent<rastgeleDusenIksir>().spriteRenderer.sprite = eldekiIksir.iksirIcon;
        Instantiate(dusecekOlanIksir, transform.position, transform.rotation);
    }
}