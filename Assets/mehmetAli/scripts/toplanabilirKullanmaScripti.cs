using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toplanabilirKullanmaScripti : MonoBehaviour
{

    public float toplanabilirEtkiSuresi, kalanToplanabilirEtkiSuresi;

    public GameObject toplanabilirObje;

    public Image toplanabilirIconu, toplanabilirEtkiImage;
    public string toplanabilirAdi;
    public string toplanabilirAciklamaKeyi;

    public toplanabilirOzellikleri toplanabilirOzellikleri;

    public bool toplanabilirObjeOzelliginiKullandi;


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.R) && !toplanabilirObjeOzelliginiKullandi) && toplanabilirObje != null)
        {
            Debug.Log("toplanabilir kullanildi");
            toplanabilirObjeOzelliginiKullandi = true;
            kalanToplanabilirEtkiSuresi = toplanabilirEtkiSuresi;
            toplanabilirOzellikleri.toplanabilirObjeOzelliginiKullan();
        }

        if (toplanabilirObjeOzelliginiKullandi)
        {
            kalanToplanabilirEtkiSuresi -= Time.deltaTime;
            toplanabilirEtkiImage.fillAmount = kalanToplanabilirEtkiSuresi / toplanabilirEtkiSuresi;

            if (kalanToplanabilirEtkiSuresi <= 0)
            {
                toplanabilirObje = null;
                toplanabilirObjeOzelliginiKullandi = false;
            }
        }
    }

    public void toplanabilirObjeOzellikleriniGetir()
    {
        toplanabilirOzellikleri = toplanabilirObje.GetComponent<toplanabilirOzellikleri>();
        toplanabilirAdi = toplanabilirOzellikleri.toplanabilirAdi;
        toplanabilirAciklamaKeyi = toplanabilirOzellikleri.toplanabilirAciklamaKeyi;
        toplanabilirEtkiSuresi = toplanabilirOzellikleri.toplanabilirEtkiSuresi;
        toplanabilirIconu.sprite = toplanabilirOzellikleri.toplanabilirIcon;
    }

    public void toplanabilirObjeKullanildi()
    {
        toplanabilirObje = null;
        toplanabilirAdi = null;
        toplanabilirAciklamaKeyi = null;
        toplanabilirIconu.sprite = null;
    }
}
