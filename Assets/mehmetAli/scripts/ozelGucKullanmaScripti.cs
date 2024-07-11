using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ozelGucKullanmaScripti : MonoBehaviour
{
    public bool ozelGuc1Mi, ozelGuc2Mi, medKit, ozelGuc1BeklemeSuresiAktiflesti, ozelGuc2BeklemeSuresiAktiflesti;

    public float ozelGuc1KalanSure, ozelGuc2KalanSure;
    public float ozelGuc1ToplamSure = 25f;
    public float ozelGuc2ToplamSure = 25f;

    public TextMeshProUGUI ozelGuc1KalanSureText, ozelGuc2KalanSureText;

    public GameObject ozelGucObjesi;

    public Image ozelGuc1Image, ozelGuc2Image, ozelGuc1KalanSureImage, ozelGuc2KalanSureImage;

    public SpriteRenderer ozelGuc1SpriteRenderer, ozelGuc2SpriteRenderer;

    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public canKontrol canKontrol;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        ozelGuc1SpriteRenderer = ozelGucObjesi.GetComponent<SpriteRenderer>();
        ozelGuc2SpriteRenderer = ozelGucObjesi.GetComponent<SpriteRenderer>();

        ozelGuc1Image.sprite = ozelGuc1SpriteRenderer.sprite;
        ozelGuc2Image.sprite = ozelGuc2SpriteRenderer.sprite;

        ozelGuc1KalanSureImage.fillAmount = 0f;
        ozelGuc2KalanSureImage.fillAmount = 0f;

        ozelGuc1KalanSure = ozelGuc1ToplamSure;
        ozelGuc2KalanSure = ozelGuc2ToplamSure;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ozelGuc1Mi)
        {
            if (!ozelGuc1BeklemeSuresiAktiflesti)
                ozelGuc1Kullanimi();
            else
                Debug.Log("Q ozel guc suresi dolmadi");
        }
        if (Input.GetKeyDown(KeyCode.E) && ozelGuc2Mi)
        {
            if (!ozelGuc2BeklemeSuresiAktiflesti)
                ozelGuc2Kullanimi();
            else
                Debug.Log("E ozel guc suresi dolmadi");
        }

        if (ozelGuc1BeklemeSuresiAktiflesti)
        {
            Debug.Log("ozelGuc1 sayaci basladi");

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
            Debug.Log("ozelGuc2 sayaci basladi");

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

    public void ozelGuc1Kullanimi()
    {
        if (ozelGucObjesi.name == "medKitOzelGuc")
        {
            if (canKontrol.can < 100)
            {
                ozelGuc1BeklemeSuresiAktiflesti = true;
                medKitKullanildi();
            }
            else
                Debug.Log("can zaten 100");
        }
        else
        {
            Debug.Log("ozel guc 1 kullanildi");
            ozelGuc1BeklemeSuresiAktiflesti = true;
            ozelGucKullanildi();
        }
    }

    public void ozelGuc2Kullanimi()
    {
        if (ozelGucObjesi.name == "medKitOzelGuc")
        {
            if (canKontrol.can < 100)
            {
                ozelGuc2BeklemeSuresiAktiflesti = true;
                medKitKullanildi();
            }
            else
                Debug.Log("can zaten 100");
        }
        else
        {
            Debug.Log("ozel guc 2 kullanildi");
            ozelGuc2BeklemeSuresiAktiflesti = true;
            ozelGucKullanildi();
        }
    }

    public void ozelGucKullanildi()
    {
        if (oyuncuSaldiriTest.transform.localScale.x == 1)
        {
            Instantiate(ozelGucObjesi, transform.position, ozelGucObjesi.transform.rotation);
        }
        if (oyuncuSaldiriTest.transform.localScale.x == -1)
        {
            Instantiate(ozelGucObjesi, transform.position, ozelGucObjesi.transform.rotation);
        }
    }

    public void medKitKullanildi()
    {
        Debug.Log("ozel guc medkit");
        canKontrol.canArtmaMiktari = 10;
        canKontrol.canArtiyor = true;
    }
}
