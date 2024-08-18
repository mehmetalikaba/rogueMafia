using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DuraklatmaMenusu : MonoBehaviour
{
    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, iksir, duraklatmaMenusu, oyunPanel, escPanel, ayarlarPanel, bilgilendirmeMetni, oyunObjeleri, yagmur;
    public Text[] degerler, adlar, hasarlar, menziller;
    public GameObject[] silahObjeleri, ozelGuclerVeToplanabilir;
    public Image[] iconlar;
    public bool menuAcik, duraklatmaKilitli;
    public Button button;


    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public iksirKullanmaScripti iksirKullanmaScripti;
    public silahOzellikleriniGetir silah1Ozellikleri, silah2Ozellikleri;
    public localizedText[] aciklamalar;
    LocalizationManager localizationManager;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    canKontrol canKontrol;
    envanterKontrol envanterKontrol;
    oyuncuHareket oyuncuHareket;
    string hasarValue, menzilValue;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        localizationManager = FindObjectOfType<LocalizationManager>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        canKontrol = FindObjectOfType<canKontrol>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        hasarValue = localizationManager.GetLocalizedValue("hasar");
        menzilValue = localizationManager.GetLocalizedValue("menzil");
    }
    void Update()
    {
        degerleriGetir();

        if (Input.GetKeyDown(KeyCode.Keypad0))
            textDuzenleyici();

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("escTusu")) && !duraklatmaKilitli)
        {
            if (!menuAcik)
                durdur();
            else
                devamEt();
        }
    }
    public void durdur()
    {
        menuAcik = true;
        oyuncuSaldiriTest.silahlarKilitli = true;
        yagmur.SetActive(false);
        oyunObjeleri.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        duraklatmaMenusu.SetActive(true);
        oyunPanel.SetActive(false);
        silahBilgileriniGetir();
    }
    public void devamEt()
    {
        menuAcik = false;
        oyuncuSaldiriTest.silahlarKilitli = false;
        yagmur.SetActive(true);
        oyunObjeleri.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        duraklatmaMenusu.SetActive(false);
        oyunPanel.SetActive(true);
        silahBilgileriniGetir();
    }
    public void ayarlar()
    {
        escPanel.SetActive(false);
        ayarlarPanel.SetActive(true);
    }
    public void geriDon()
    {
        escPanel.SetActive(true);
        ayarlarPanel.SetActive(false);
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
    public void degerleriGetir()
    {
        degerler[0].text = "sonHasarYakin: " + oyuncuSaldiriTest.sonHasarYakin.ToString();
        degerler[1].text = "sonHasarMenzilli: " + oyuncuSaldiriTest.sonHasarMenzilli.ToString();
        degerler[2].text = "bonusHasarlarYakin: " + oyuncuSaldiriTest.bonusHasarlarYakin.ToString();
        degerler[3].text = "bonusHasarlarMenzilli: " + oyuncuSaldiriTest.bonusHasarlarMenzilli.ToString();
        degerler[4].text = "silah1DayanikliligiBonus: " + oyuncuSaldiriTest.silah1DayanikliligiBonus.ToString();
        degerler[5].text = "silah2DayanikliligiBonus: " + oyuncuSaldiriTest.silah2DayanikliligiBonus.ToString();
        degerler[6].text = "sonSaldiriMenzili: " + oyuncuSaldiriTest.sonSaldiriMenzili.ToString();
        degerler[7].text = "kritikIhtimali: " + oyuncuSaldiriTest.kritikIhtimali.ToString();
        degerler[8].text = "kritikHasari: " + oyuncuSaldiriTest.kritikHasari.ToString();
        degerler[9].text = "baslangicCani: " + canKontrol.baslangicCani.ToString();
        degerler[10].text = "olmemeSansiVar: " + canKontrol.olmemeSansiVar.ToString();
        degerler[11].text = "iskaSansi: " + canKontrol.iskaSansi.ToString();
        degerler[12].text = "ejderhaPuaniArtmaMiktari: " + envanterKontrol.ejderhaPuaniArtmaMiktari.ToString();
        degerler[13].text = "aniArttirmaMiktari: " + envanterKontrol.aniArttirmaMiktari.ToString();
        degerler[14].text = "olunceAniMiktariAzalmaYuzdesi: " + envanterKontrol.olunceAniMiktariAzalmaYuzdesi.ToString();
        degerler[15].text = "ozelGuc1ToplamSure: " + ozelGuc1KullanmaScripti.ozelGuc1ToplamSure.ToString();
        degerler[16].text = "ozelGuc2ToplamSure: " + ozelGuc2KullanmaScripti.ozelGuc2ToplamSure.ToString();
        degerler[17].text = "canAzalmaAzalisi: " + canKontrol.canAzalmaAzalisi.ToString();
        degerler[18].text = "sonHareketHizi: " + oyuncuHareket.sonHareketHizi.ToString();
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

        if (ozelGuclerVeToplanabilir[2].GetComponent<iksirKullanmaScripti>().eldekiIksir != null)
            iksirKullanmaScripti = ozelGuclerVeToplanabilir[2].GetComponent<iksirKullanmaScripti>();

        iconlar[0].sprite = silah1Ozellikleri.silahImage.sprite;
        iconlar[1].sprite = silah2Ozellikleri.silahImage.sprite;
        iconlar[2].sprite = ozelGuc1KullanmaScripti.ozelGuc1Image.sprite;
        iconlar[3].sprite = ozelGuc2KullanmaScripti.ozelGuc2Image.sprite;
        iconlar[4].sprite = iksirKullanmaScripti.iksirImage.sprite;

        adlar[0].text = silah1Ozellikleri.silahAdi;
        adlar[1].text = silah2Ozellikleri.silahAdi;
        adlar[2].GetComponent<localizedText>().key = ozelGuc1KullanmaScripti.ozelGucAdi;
        adlar[3].GetComponent<localizedText>().key = ozelGuc2KullanmaScripti.ozelGucAdi;
        adlar[4].GetComponent<localizedText>().key = iksirKullanmaScripti.eldekiIksir.iksirAdi;

        hasarlar[0].text = hasarValue + silah1Ozellikleri.silahSaldiriHasari.ToString("F0");
        hasarlar[1].text = hasarValue + silah2Ozellikleri.silahSaldiriHasari.ToString("F0");

        menziller[0].text = menzilValue + silah1Ozellikleri.silahSaldiriMenzili.ToString("F0");
        menziller[1].text = menzilValue + silah2Ozellikleri.silahSaldiriMenzili.ToString("F0");

        aciklamalar[0].key = silah1Ozellikleri.aciklamaKeyi;
        aciklamalar[1].key = silah2Ozellikleri.aciklamaKeyi;
        aciklamalar[2].key = ozelGuc1KullanmaScripti.ozelGucAciklamaKeyi;
        aciklamalar[3].key = ozelGuc2KullanmaScripti.ozelGucAciklamaKeyi;
        aciklamalar[4].key = iksirKullanmaScripti.eldekiIksir.iksirAciklamaKeyi;
    }

    void textDuzenleyici()
    {
        GameObject[] tumMetinler = FindObjectsOfType<GameObject>(true);

        foreach (GameObject obj in tumMetinler)
        {
            Text uiText = obj.GetComponent<Text>();
            if (uiText != null)
            {
                if (uiText.GetComponent<localizedText>() == null)
                {
                    uiText.text = "-----";
                    Debug.Log(uiText.name + " <==> LOCALIZED <==> YOK <==> TEXT");
                }
                else
                {
                    if (uiText.GetComponent<localizedText>().key == null)
                        Debug.Log(uiText.name + " <==> KEY <==> BOÞ <==> TEXT");
                }

            }

            TextMeshProUGUI textMeshProUGUI = obj.GetComponent<TextMeshProUGUI>();
            if (textMeshProUGUI != null)
            {
                if (textMeshProUGUI.GetComponent<localizedText>() == null)
                {
                    textMeshProUGUI.text = "-----";
                    Debug.Log(textMeshProUGUI.name + " <==> LOCALIZED <==> YOK <==> textMeshProUGUI");
                }
                else
                {
                    if (textMeshProUGUI.GetComponent<localizedText>().key == null)
                        Debug.Log(gameObject.name + " <==> KEY <==> BOÞ <==> textMeshProUGUI");
                }
            }

            TextMeshPro textMeshPro = obj.GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                if (textMeshPro.GetComponent<localizedText>() == null)
                {
                    textMeshPro.text = "-----";
                    Debug.Log(textMeshPro.name + " <==> LOCALIZED <==> YOK <==> textMeshPro");
                }
                else if (textMeshPro.GetComponent<localizedText>().key == null)
                    Debug.Log(gameObject.name + " <==> KEY <==> BOÞ <==> textMeshPro");
            }
        }
    }
}