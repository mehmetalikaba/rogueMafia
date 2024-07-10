using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ozelGucKullanmaScripti : MonoBehaviour
{
    public bool ozelGuc1Mi, ozelGuc2Mi, medKit, ozelGuc1BeklemeSuresiAktiflesti, ozelGuc2BeklemeSuresiAktiflesti;

    public float ozelGuc1KalanSure, ozelGuc2KalanSure;

    public TextMeshProUGUI ozelGuc1KalanSureText, ozelGuc2KalanSureText;

    public GameObject ozelGucObjesi;

    public Image ozelGuc1Image, ozelGuc2Image;

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


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ozelGuc1Mi)
        {
            if (!ozelGuc1BeklemeSuresiAktiflesti)
                ozelGuc1Kullanimi();
        }
        if (Input.GetKeyDown(KeyCode.E) && ozelGuc2Mi)
        {
            if (!ozelGuc2BeklemeSuresiAktiflesti)
                ozelGuc2Kullanimi();
        }

        if (ozelGuc1BeklemeSuresiAktiflesti)
        {
            Debug.Log("ozelGuc1 sayaci basladi");

            ozelGuc1KalanSure -= Time.deltaTime;
            ozelGuc1KalanSureText.text = ozelGuc1KalanSure.ToString("F0");
            if (ozelGuc1KalanSure <= 0)
                ozelGuc1BeklemeSuresiAktiflesti = false;
        }
        if (ozelGuc2BeklemeSuresiAktiflesti)
        {
            Debug.Log("ozelGuc2 sayaci basladi");

            ozelGuc2KalanSure -= Time.deltaTime;
            ozelGuc2KalanSureText.text = ozelGuc2KalanSure.ToString("F0");
            if (ozelGuc2KalanSure <= 0)
                ozelGuc2BeklemeSuresiAktiflesti = false;
        }
    }

    public void ozelGuc1Kullanimi()
    {
        ozelGuc1BeklemeSuresiAktiflesti = true;

        ozelGucKullanildi();

        if (medKit)
        {
            canKontrol.canArtmaMiktari = 10;
            canKontrol.canArtiyor = true;
        }


    }
    public void ozelGuc2Kullanimi()
    {
        ozelGuc2BeklemeSuresiAktiflesti = true;

        ozelGucKullanildi();

        if (medKit)
        {
            canKontrol.canArtmaMiktari = 10;
            canKontrol.canArtiyor = true;
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
}
