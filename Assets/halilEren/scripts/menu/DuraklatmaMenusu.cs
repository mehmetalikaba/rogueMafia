using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DuraklatmaMenusu : MonoBehaviour
{
    public GameObject duraklatmaMenusu;
    public silahOzellikleriniGetir silah1Ozellikleri, silah2Ozellikleri;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public GameObject[] tumSilahlar;
    public GameObject[] ozelGuclerVeToplanabilir;
    public Image[] iconlar;
    public Text[] adlar, hasarlar, menziller;
    public localizedText[] aciklamalar;
    public bool menuAcik;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!menuAcik)
            {
                menuAcik = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                duraklatmaMenusu.SetActive(true);
                Time.timeScale = 0;
                silahBilgileriniGetir();
            }
            else
            {
                menuAcik = false;
                DevamEt();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (menuAcik)
                silahBilgileriniGetir();
        }
    }
    public void DevamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        duraklatmaMenusu.SetActive(false);
        Time.timeScale = 1;
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
    public void silahBilgileriniGetir()
    {
        silah1Ozellikleri = tumSilahlar[0].GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = tumSilahlar[1].GetComponent<silahOzellikleriniGetir>();

        ozelGuc1KullanmaScripti = ozelGuclerVeToplanabilir[0].GetComponent<ozelGucKullanmaScripti>();
        ozelGuc2KullanmaScripti = ozelGuclerVeToplanabilir[1].GetComponent<ozelGucKullanmaScripti>();

        iconlar[0].sprite = silah1Ozellikleri.silahImage.sprite;
        iconlar[1].sprite = silah2Ozellikleri.silahImage.sprite;
        iconlar[2].sprite = ozelGuc1KullanmaScripti.ozelGuc1Image.sprite;
        iconlar[3].sprite = ozelGuc2KullanmaScripti.ozelGuc2Image.sprite;

        adlar[0].text = silah1Ozellikleri.silahAdi;
        adlar[1].text = silah2Ozellikleri.silahAdi;
        adlar[2].text = ozelGuc1KullanmaScripti.ozelGucAdi;
        adlar[3].text = ozelGuc2KullanmaScripti.ozelGucAdi;

        hasarlar[0].text = "hasar: " + silah1Ozellikleri.silahSaldiriHasari.ToString();
        hasarlar[1].text = "hasar: " + silah2Ozellikleri.silahSaldiriHasari.ToString();

        menziller[0].text = "menzil: " + silah1Ozellikleri.silahSaldiriMenzili.ToString();
        menziller[1].text = "menzil: " + silah2Ozellikleri.silahSaldiriMenzili.ToString();

        aciklamalar[0].key = silah1Ozellikleri.aciklamaKeyi;
        aciklamalar[1].key = silah2Ozellikleri.aciklamaKeyi;
        aciklamalar[2].key = ozelGuc1KullanmaScripti.ozelGucAciklamaKeyi;
        aciklamalar[3].key = ozelGuc2KullanmaScripti.ozelGucAciklamaKeyi;

    }
}