using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ozelGucKullanmaScripti : MonoBehaviour
{
    public bool ozelGuc1Mi, ozelGuc2Mi, medKit, ozelGuc1BeklemeSuresiAktiflesti, ozelGuc2BeklemeSuresiAktiflesti;
    public float ozelGuc1KalanSure, ozelGuc2KalanSure;
    public float ozelGuc1ToplamSure = 10f;
    public float ozelGuc2ToplamSure = 10f;
    public TextMeshProUGUI ozelGuc1KalanSureText, ozelGuc2KalanSureText;
    public GameObject ozelGucObjesi, ozelGucObjesiSag, ozelGucObjesiSol, agirCekimVolume;
    public Image ozelGuc1Image, ozelGuc2Image, ozelGuc1KalanSureImage, ozelGuc2KalanSureImage;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public canKontrol canKontrol;
    public ozelGucOzellikleri ozelGucOzellikleri;
    public string ozelGucAdi, ozelGucAciklamaKeyi;
    public yetenekKontrol yetenekKontrol;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();

        ozelGuc1KalanSureImage.fillAmount = 0f;
        ozelGuc2KalanSureImage.fillAmount = 0f;

        yetenekKontrol.pasif2SkillEtkileriniUygula();

        ozelGuc1KalanSure = ozelGuc1ToplamSure;
        ozelGuc2KalanSure = ozelGuc2ToplamSure;
    }
    void Update()
    {
        if (ozelGucObjesi != null)
        {
            ozelGucOzellikleri = ozelGucObjesi.GetComponent<ozelGucOzellikleri>();
            ozelGucAdi = ozelGucOzellikleri.ozelGucAd;
            ozelGucAciklamaKeyi = ozelGucOzellikleri.ozelGucAciklamaKeyi;
            if (ozelGuc1Mi)
                ozelGuc1Image.sprite = ozelGucOzellikleri.ozelGucIconu;
            else if (ozelGuc2Mi)
                ozelGuc2Image.sprite = ozelGucOzellikleri.ozelGucIconu;
        }
        if (!ozelGuc1BeklemeSuresiAktiflesti)
        {
            if (ozelGuc1Mi && ozelGucObjesi != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (ozelGucObjesi.name == "medKitOzelGuc")
                    {
                        if (canKontrol.can < 100)
                        {
                            ozelGuc1BeklemeSuresiAktiflesti = true;
                            medKitKullanildi();
                        }
                    }
                    else
                    {
                        Instantiate(ozelGucObjesi, transform.position, Quaternion.identity);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = false;
                        agirCekimVolume.SetActive(true);
                    }
                }
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    agirCekimVolume.SetActive(false);
                    ozelGuc1BeklemeSuresiAktiflesti = true;
                }
            }
        }
        if (!ozelGuc2BeklemeSuresiAktiflesti)
        {
            if (ozelGuc2Mi && ozelGucObjesi != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ozelGucObjesi.name == "medKitOzelGuc")
                    {
                        if (canKontrol.can < 100)
                        {
                            ozelGuc1BeklemeSuresiAktiflesti = true;
                            medKitKullanildi();
                        }
                    }
                    else
                    {
                        Instantiate(ozelGucObjesi, transform.position, Quaternion.identity);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = false;
                        agirCekimVolume.SetActive(true);
                    }

                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    agirCekimVolume.SetActive(false);
                    ozelGuc2BeklemeSuresiAktiflesti = true;
                }
            }
        }

        if (ozelGuc1BeklemeSuresiAktiflesti)
        {
            ozelGuc1KalanSure -= Time.deltaTime;
            ozelGuc1KalanSureImage.fillAmount = ozelGuc1KalanSure / ozelGuc1ToplamSure;
            ozelGuc1KalanSureText.text = ozelGuc1KalanSure.ToString("F0");
            if (ozelGuc1KalanSure <= 0)
            {
                ozelGuc1BeklemeSuresiAktiflesti = false;
                ozelGuc1KalanSure = ozelGuc1ToplamSure;
                ozelGuc1KalanSureImage.fillAmount = 1f;
            }
        }
        if (ozelGuc2BeklemeSuresiAktiflesti)
        {
            ozelGuc2KalanSure -= Time.deltaTime;
            ozelGuc2KalanSureImage.fillAmount = ozelGuc2KalanSure / ozelGuc2ToplamSure;
            ozelGuc2KalanSureText.text = ozelGuc2KalanSure.ToString("F0");
            if (ozelGuc2KalanSure <= 0)
            {
                ozelGuc2BeklemeSuresiAktiflesti = false;
                ozelGuc2KalanSure = ozelGuc2ToplamSure;
                ozelGuc2KalanSureImage.fillAmount = 1f;
            }
        }
    }
    public void medKitKullanildi()
    {
        canKontrol.canArtmaMiktari = ((canKontrol.can / 100) * 50);
        canKontrol.canArtiyor = true;
    }
}
