using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DuraklatmaMenusu : MonoBehaviour
{
    public GameObject oyunObjeleri,yagmur;
    public GameObject duraklatmaMenusu, oyunPanel, bilgilendirmeMetni;
    public silahOzellikleriniGetir silah1Ozellikleri, silah2Ozellikleri;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public GameObject[] silahObjeleri;
    public GameObject[] ozelGuclerVeToplanabilir;
    public Image[] iconlar;
    public Text[] adlar, hasarlar, menziller;
    public localizedText[] aciklamalar;
    public bool menuAcik, duraklatmaKilitli;
    public string hasarValue, menzilValue;
    LocalizationManager localizationManager;
    public scriptKontrol scriptKontrol;
    public Button button;

    private void Start()
    {
        localizationManager = FindObjectOfType<LocalizationManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        hasarValue = localizationManager.GetLocalizedValue("hasar");
        menzilValue = localizationManager.GetLocalizedValue("menzil");
        scriptKontrol = FindObjectOfType<scriptKontrol>();
    }
    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("escTusu")) && !duraklatmaKilitli)
        {
            if (!scriptKontrol.oyuncuSaldiriTest.silahlarKilitli)
                scriptKontrol.oyuncuSaldiriTest.silahlarKilitli = true;
            else
                scriptKontrol.oyuncuSaldiriTest.silahlarKilitli = false;

            if (!menuAcik)
            {
                yagmur.SetActive(false);
                oyunObjeleri.SetActive(false);
                menuAcik = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                duraklatmaMenusu.SetActive(true);
                oyunPanel.SetActive(false);
                silahBilgileriniGetir();
            }
            else
            {
                menuAcik = false;
                DevamEt();
            }
        }
    }
    public void DevamEt()
    {
        yagmur.SetActive(true);
        oyunObjeleri.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        duraklatmaMenusu.SetActive(false);
        oyunPanel.SetActive(true);
        menuAcik = false;
        silahBilgileriniGetir();

    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menuTest");
    }
    public void Masaustu()
    {
        Application.Quit();
    }

    public void bilgilendirmeMetniKontrol()
    {
        if (bilgilendirmeMetni.activeSelf)
        {
            Debug.Log("kapandi");
            bilgilendirmeMetni.SetActive(false);
            button.GetComponent<localizedText>().key = "bilgilendirici_metin_kapali";
        }
        else
        {
            Debug.Log("acildi");
            bilgilendirmeMetni.SetActive(true);
            button.GetComponent<localizedText>().key = "bilgilendirici_metin_acik";
        }
    }
    public void silahBilgileriniGetir()
    {
        silah1Ozellikleri = silahObjeleri[0].GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = silahObjeleri[1].GetComponent<silahOzellikleriniGetir>();

        ozelGuc1KullanmaScripti = ozelGuclerVeToplanabilir[0].GetComponent<ozelGucKullanmaScripti>();
        ozelGuc2KullanmaScripti = ozelGuclerVeToplanabilir[1].GetComponent<ozelGucKullanmaScripti>();

        toplanabilirKullanmaScripti = ozelGuclerVeToplanabilir[2].GetComponent<toplanabilirKullanmaScripti>();

        iconlar[0].sprite = silah1Ozellikleri.silahImage.sprite;
        iconlar[1].sprite = silah2Ozellikleri.silahImage.sprite;
        iconlar[2].sprite = ozelGuc1KullanmaScripti.ozelGuc1Image.sprite;
        iconlar[3].sprite = ozelGuc2KullanmaScripti.ozelGuc2Image.sprite;
        iconlar[4].sprite = toplanabilirKullanmaScripti.toplanabilirIcon;

        adlar[0].text = silah1Ozellikleri.silahAdi;
        adlar[1].text = silah2Ozellikleri.silahAdi;
        adlar[2].GetComponent<localizedText>().key = ozelGuc1KullanmaScripti.ozelGucAdi;
        adlar[3].GetComponent<localizedText>().key = ozelGuc2KullanmaScripti.ozelGucAdi;
        adlar[4].GetComponent<localizedText>().key = toplanabilirKullanmaScripti.toplanabilirKeyi;

        hasarlar[0].text = hasarValue + silah1Ozellikleri.silahSaldiriHasari.ToString("F0");
        hasarlar[1].text = hasarValue + silah2Ozellikleri.silahSaldiriHasari.ToString("F0");

        menziller[0].text = menzilValue + silah1Ozellikleri.silahSaldiriMenzili.ToString("F0");
        menziller[1].text = menzilValue + silah2Ozellikleri.silahSaldiriMenzili.ToString("F0");

        aciklamalar[0].key = silah1Ozellikleri.aciklamaKeyi;
        aciklamalar[1].key = silah2Ozellikleri.aciklamaKeyi;
        aciklamalar[2].key = ozelGuc1KullanmaScripti.ozelGucAciklamaKeyi;
        aciklamalar[3].key = ozelGuc2KullanmaScripti.ozelGucAciklamaKeyi;
        aciklamalar[4].key = toplanabilirKullanmaScripti.toplanabilirAciklamaKeyi;
    }

    /*
    //aciklamalar[0].DilDegistiHandler();
    //aciklamalar[1].DilDegistiHandler();
    //aciklamalar[2].DilDegistiHandler();
    //aciklamalar[3].DilDegistiHandler();
    //aciklamalar[4].DilDegistiHandler();
    */
}