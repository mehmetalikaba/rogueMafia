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
    public GameObject[] tumSilahlar;
    public Image[] silahlarIconlari;
    public Text[] silahlarAdlari;

    public bool menuAcik;

    void Start()
    {

    }

    void Update()
    {
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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                duraklatmaMenusu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(menuAcik)
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
        SceneManager.LoadScene(0);
    }
    public void Masaustu()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    public void silahBilgileriniGetir()
    {
        silah1Ozellikleri = tumSilahlar[0].GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = tumSilahlar[1].GetComponent<silahOzellikleriniGetir>();

        silahlarIconlari[0].sprite = silah1Ozellikleri.silahImage.sprite;
        silahlarIconlari[1].sprite = silah2Ozellikleri.silahImage.sprite;

        silahlarAdlari[0].text = silah1Ozellikleri.silahAdi;
        silahlarAdlari[1].text = silah2Ozellikleri.silahAdi;

        silahlarAdlari[2].text = "hasar: " + silah1Ozellikleri.silahSaldiriHasari.ToString();
        silahlarAdlari[3].text = "menzil: " + silah1Ozellikleri.silahSaldiriMenzili.ToString();

        silahlarAdlari[4].text = "hasar: " + silah2Ozellikleri.silahSaldiriHasari.ToString();
        silahlarAdlari[5].text = "menzil: " + silah2Ozellikleri.silahSaldiriMenzili.ToString();
    }

}
