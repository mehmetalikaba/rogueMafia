using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toplanabilirKullanmaScripti : MonoBehaviour
{
    public float toplanabilirEtkiSuresi, kalanToplanabilirEtkiSuresi, ilkCan, sonCan, artanCan;

    public GameObject toplanabilirObje, toplanabilirObjeEtkiSuresiBG;
    public GameObject[] butunToplanabilirler;

    public Sprite toplanabilirIcon;
    public string toplanabilirKeyi, toplanabilirAdi, toplanabilirKeyiKayit;
    public string toplanabilirAciklamaKeyi, simdikiToplanabilir;

    public bool toplanabilirObjeOzelliginiKullandi, canObjesiAktif, pozisyonBelirlendi;
    public Image toplanabilirImage, toplanabilirEtkiImage;

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
        if (toplanabilirObje != null && ((toplanabilirObje.name != simdikiToplanabilir) || toplanabilirKeyi == ""))
            toplanabiliriGetir();


        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("rTusu")) && !toplanabilirObjeOzelliginiKullandi && !canKontrol.canArtiyor)
        {
            if (toplanabilirObje != null)
            {
                if (toplanabilirKeyi == "can_iksiri" && canKontrol.can < canKontrol.baslangicCani - 25)
                {
                    canKontrol.canIksiriKatkisi = 25f;
                    canObjesiAktif = true;
                    canKontrol.toplanabilirCanObjesiAktif = true;
                    toplanabilirKullanildi();
                }
                if (toplanabilirKeyi == "dayaniklilik_iksiri")
                {
                    canKontrol.dayaniklilikObjesiAktif = true;
                    toplanabilirKullanildi();
                }
                if (toplanabilirKeyi == "hareket_hizi_iksiri")
                {
                    oyuncuHareket.hareketHizObjesiAktif = true;
                    canKontrol.hareketHiziObjesiAktif = true;
                    toplanabilirKullanildi();
                }
                if (toplanabilirKeyi == "hasar_iksiri")
                {
                    oyuncuSaldiriTest.hasarObjesiAktif = true;
                    canKontrol.hareketHiziObjesiAktif = true;
                    toplanabilirKullanildi();
                }
                if (toplanabilirKeyi == "ziplama_iksiri")
                {
                    canKontrol.ziplamaIksiriAktif = true;
                    oyuncuHareket.ziplamaGucuBonus = 1.25f;
                    toplanabilirKullanildi();
                }
                if (toplanabilirKeyi == "bagisiklik_iksiri")
                {
                    canKontrol.bagisiklikIksiriAktif = true;
                    toplanabilirKullanildi();
                }
            }
        }

        if (toplanabilirObjeOzelliginiKullandi)
        {
            // eger oyuncu, canObjesinin katt���ndan daha fazla hasar al�rsa, toplanabilir obje an�nda kaybolur.
            if (canObjesiAktif)
                if (canKontrol.canIksiriKatkisi <= 0)
                    toplanabilirObjeKullanildi();
            // eger oyuncu, canObjesinin katt���ndan daha fazla hasar al�rsa, toplanabilir obje an�nda kaybolur.

            kalanToplanabilirEtkiSuresi -= Time.deltaTime;
            toplanabilirEtkiImage.fillAmount = kalanToplanabilirEtkiSuresi / toplanabilirEtkiSuresi;
            if (kalanToplanabilirEtkiSuresi <= 0)
                toplanabilirObjeKullanildi();
        }
    }

    public void toplanabilirKullanildi()
    {
        toplanabilirObjeEtkiSuresiBG.SetActive(true);
        iksirActi.Play();
        toplanabilirKeyiKayit = toplanabilirKeyi;
        toplanabilirObjeOzelliginiKullandi = true;
        toplanabilirEtkiSuresi = toplanabilirObje.GetComponent<toplanabilirOzellikleri>().iksirSuresi;
        kalanToplanabilirEtkiSuresi = toplanabilirEtkiSuresi;
        toplanabilirImage.sprite = oyuncuSaldiriTest.yumrukSprite;
        toplanabilirObje = null;
        toplanabilirAdi = null;
        toplanabilirAciklamaKeyi = null;
        iksirEtkisi.Play();
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
        else if (canKontrol.dayaniklilikObjesiAktif)
            canKontrol.canBari.color = Color.gray;
        else if (canKontrol.hasarObjesiAktif)
            canKontrol.canBari.color = Color.magenta;
        else if (canKontrol.hareketHiziObjesiAktif)
            canKontrol.canBari.color = Color.blue;
        else if (canKontrol.ziplamaIksiriAktif)
            canKontrol.canBari.color = Color.green;
        else if (canKontrol.bagisiklikIksiriAktif)
            canKontrol.canBari.color = Color.yellow;
    }

    public void toplanabilirObjeKullanildi()
    {
        toplanabilirObjeEtkiSuresiBG.SetActive(false);
        if (toplanabilirKeyiKayit == "can_iksiri")
        {
            canKontrol.canIksiriKatkisi = 0f;
            canKontrol.canIksiriBari.fillAmount = 0f;
            pozisyonBelirlendi = false;
            rectTransform.anchoredPosition = originalPosition;
        }
        oyuncuHareket.ziplamaGucuBonus = 1f;
        kalanToplanabilirEtkiSuresi = 0f;
        canObjesiAktif = false;
        canKontrol.toplanabilirCanObjesiAktif = false;
        canKontrol.dayaniklilikObjesiAktif = false;
        canKontrol.hareketHiziObjesiAktif = false;
        canKontrol.hasarObjesiAktif = false;
        oyuncuHareket.hareketHizObjesiAktif = false;
        oyuncuSaldiriTest.hasarObjesiAktif = false;
        toplanabilirObjeOzelliginiKullandi = false;
    }

    public void toplanabiliriGetir()
    {
        toplanabilirOzellikleri toplanabilirOzellikleri;
        toplanabilirOzellikleri = toplanabilirObje.GetComponent<toplanabilirOzellikleri>();
        toplanabilirAdi = toplanabilirOzellikleri.toplanabilirAdi;
        toplanabilirIcon = toplanabilirOzellikleri.toplanabilirIcon;
        toplanabilirImage.sprite = toplanabilirIcon;
        toplanabilirKeyi = toplanabilirOzellikleri.toplanabilirKeyi;
        toplanabilirAciklamaKeyi = toplanabilirOzellikleri.toplanabilirAciklamaKeyi;
        simdikiToplanabilir = toplanabilirObje.name;
    }
}
