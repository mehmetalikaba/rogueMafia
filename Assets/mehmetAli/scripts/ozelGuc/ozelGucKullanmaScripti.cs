using UnityEngine;
using UnityEngine.UI;

public class ozelGucKullanmaScripti : MonoBehaviour
{
    public Text ozelGucKalanSureYazisi;
    public bool ozelGuclerKilitli, ozelGuc1Mi, ozelGuc2Mi, medKit, ozelGuc1BeklemeSuresiAktiflesti, ozelGuc2BeklemeSuresiAktiflesti;
    public float ozelGuc1KalanSure, ozelGuc2KalanSure;
    public float ozelGuc1ToplamSure = 10f;
    public float ozelGuc2ToplamSure = 10f;
    public GameObject ozelGucObjesi, ozelGucObjesiSag, ozelGucObjesiSol, agirCekimVolume, ozelGuc1Olustur, ozelGuc2Olustur;
    public Image ozelGuc1Image, ozelGuc2Image, ozelGuc1KalanSureImage, ozelGuc2KalanSureImage;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public canKontrol canKontrol;
    public ozelGucOzellikleri ozelGucOzellikleri;
    public string ozelGucAdi, ozelGucAciklamaKeyi;
    public yetenekKontrol yetenekKontrol;
    public GameObject[] ozelGucler;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();

        ozelGuc1KalanSureImage.fillAmount = 0f;
        ozelGuc2KalanSureImage.fillAmount = 0f;

        ozelGuc1KalanSure = ozelGuc1ToplamSure;
        ozelGuc2KalanSure = ozelGuc2ToplamSure;
    }
    void Update()
    {
        if (ozelGucObjesi == null)
        {
            if (ozelGucAciklamaKeyi == "medkit_aciklama")
                ozelGucObjesi = ozelGucler[0];
            if (ozelGucAciklamaKeyi == "buz_bomba_aciklama")
                ozelGucObjesi = ozelGucler[1];
            if (ozelGucAciklamaKeyi == "zehir_bomba_aciklama")
                ozelGucObjesi = ozelGucler[2];
            if (ozelGucAciklamaKeyi == "patlayan_bomba_aciklama")
                ozelGucObjesi = ozelGucler[3];
            if (ozelGucAciklamaKeyi == "havai_fisek_aciklama")
                ozelGucObjesi = ozelGucler[4];
        }

        if (!ozelGuclerKilitli)
        {
            if (!ozelGuc1BeklemeSuresiAktiflesti && !canKontrol.canIksiriAktif)
            {
                if (ozelGuc1Mi && ozelGucObjesi != null)
                {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
                    {
                        if (ozelGucObjesi.name == "medKitOzelGuc")
                        {
                            if (canKontrol.can < 100)
                            {
                                ozelGuc1BeklemeSuresiAktiflesti = true;
                                medKitKullanildi();
                            }
                        }
                        else if (ozelGucObjesi.name == "havaiFisek")
                        {
                            ozelGuc1BeklemeSuresiAktiflesti = true;
                            Instantiate(ozelGucObjesi, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            ozelGuc1Olustur = Instantiate(ozelGucObjesi, transform.position, Quaternion.identity);
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = false;
                            agirCekimVolume.SetActive(true);
                        }
                    }
                    if (Input.GetKeyUp(KeyCode.Q))
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        agirCekimVolume.SetActive(false);
                        if (ozelGuc1Olustur != null)
                        {
                            ozelGuc1BeklemeSuresiAktiflesti = true;
                        }
                    }
                }
            }
            if (!ozelGuc2BeklemeSuresiAktiflesti && !canKontrol.canIksiriAktif)
            {
                if (ozelGuc2Mi && ozelGucObjesi != null)
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
                    {
                        if (ozelGucObjesi.name == "medKitOzelGuc")
                        {
                            if (canKontrol.can < 100)
                            {
                                ozelGuc2BeklemeSuresiAktiflesti = true;
                                medKitKullanildi();
                            }
                        }
                        else if (ozelGucObjesi.name == "havaiFisek")
                        {
                            ozelGuc2BeklemeSuresiAktiflesti = true;
                            Instantiate(ozelGucObjesi, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            ozelGuc2Olustur = Instantiate(ozelGucObjesi, transform.position, Quaternion.identity);
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = false;
                            agirCekimVolume.SetActive(true);
                        }
                    }
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        agirCekimVolume.SetActive(false);
                        if (ozelGuc2Olustur != null)
                        {
                            ozelGuc2BeklemeSuresiAktiflesti = true;
                        }
                    }
                }
            }
        }
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


        if (ozelGuc1BeklemeSuresiAktiflesti)
        {
            ozelGucKalanSureYazisi.enabled = true;
            ozelGuc1KalanSure -= Time.deltaTime;
            ozelGucKalanSureYazisi.text = ozelGuc1KalanSure.ToString("F0");
            ozelGuc1KalanSureImage.fillAmount = ozelGuc1KalanSure / ozelGuc1ToplamSure;
            if (ozelGuc1KalanSure <= 0)
            {
                ozelGucKalanSureYazisi.enabled = false;
                ozelGuc1Olustur = null;
                ozelGuc1BeklemeSuresiAktiflesti = false;
                ozelGuc1KalanSure = ozelGuc1ToplamSure;
                canKontrol.canArtiyor = false;
            }
        }
        if (ozelGuc2BeklemeSuresiAktiflesti)
        {
            ozelGucKalanSureYazisi.enabled = true;
            ozelGuc2KalanSure -= Time.deltaTime;
            ozelGucKalanSureYazisi.text = ozelGuc2KalanSure.ToString("F0");
            ozelGuc2KalanSureImage.fillAmount = ozelGuc2KalanSure / ozelGuc2ToplamSure;
            if (ozelGuc2KalanSure <= 0)
            {
                ozelGucKalanSureYazisi.enabled = false;
                ozelGuc2Olustur = null;
                ozelGuc2BeklemeSuresiAktiflesti = false;
                ozelGuc2KalanSure = ozelGuc2ToplamSure;
                canKontrol.canArtiyor = false;
            }
        }
    }
    public void medKitKullanildi()
    {
        if (ozelGuc1Mi)
            canKontrol.canArtmaMiktari = ozelGuc1ToplamSure;
        else
            canKontrol.canArtmaMiktari = ozelGuc2ToplamSure;

        canKontrol.canArtiyor = true;
    }
}
