using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class araBaseKontrol : MonoBehaviour
{
    public Npc[] npcler;
    public localizedText[] npcTextler;

    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, cikisKontrol, cikisTextObje, sefPanel, silahciPanel, alfredPanel, shifuPanel;

    public bool silahciKonustu, alfredKonustu, diyalogActi;
    float timer;
    string ilkSefKey;

    public silahSecimi silahSecimi;
    public kaydetKontrol kaydetKontrol;
    public sefPanelScripti sefPanelScripti;
    public alfredPanelScripti alfredPanelScripti;
    public silahciPanelScripti silahciPanelScripti;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public envanterKontrol envanterKontrol;
    public canKontrol canKontrol;


    private void Awake()
    {
        if (kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti)
            kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterYukle();

        kaydetKontrol.kaydetKontrolEnvanter.olduktenSonraEnvanterGetir();
    }

    void Start()
    {
        if (kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti)
        {
            oyuncuHareket.atilmaKilitli = true;
            oyuncuHareket.inmeKilitli = true;
            oyuncuSaldiriTest.silahlarKilitli = true;
            oyuncuHareket.ziplamaKilitli = true;
            ozelGuc1KullanmaScripti.ozelGuclerKilitli = true;
            ozelGuc2KullanmaScripti.ozelGuclerKilitli = true;

            sefPanelScripti.etkilesimKilitli = false;
            alfredPanelScripti.etkilesimKilitli = false;
            silahciPanelScripti.etkilesimKilitli = false;
            sefPanelScripti.yemekUcretsiz = false;

            for (int i = 0; i < npcler.Length; i++)
            {
                npcler[i].serbest = false;
            }
            cikisKontrol.SetActive(true);

            npcTextler[0].key = "sef_key";
            npcTextler[1].key = "alfred_key";
            npcTextler[2].key = "shifu_key";
            npcTextler[3].key = "silahci_key";
            npcTextler[4].key = "antikaci_key";

            silah1.GetComponent<silahOzellikleriniGetir>().silahImage.sprite = oyuncuSaldiriTest.yumrukSprite;
            silah2.GetComponent<silahOzellikleriniGetir>().silahImage.sprite = oyuncuSaldiriTest.yumrukSprite;

            ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = null;
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = null;

            canKontrol = FindObjectOfType<canKontrol>();
            canKontrol.baslangicCani = 100f;
        }
    }

    void Update()
    {
        if (kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti)
        {
            oyuncuSaldiriTest.silahlarKilitli = true;
            silah1 = GameObject.Find("silah1");
            silah2 = GameObject.Find("silah2");
            ozelGuc1 = GameObject.Find("ozelGuc1");
            ozelGuc2 = GameObject.Find("ozelGuc2");

            if (silahciKonustu && alfredKonustu && cikisKontrol.GetComponent<asamaKontrol>().oyuncuGeldi)
            {
                cikisTextObje.SetActive(true);

                if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
                    StartCoroutine(yeniSahneGecis());
            }
            else
                cikisTextObje.SetActive(false);


            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) || Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu")))
            {
                if (silahciKonustu && !sefPanel.activeSelf && !silahciPanel.activeSelf && !alfredPanel.activeSelf && !shifuPanel.activeSelf)
                {
                    timer = 2.5f;
                    ilkSefKey = npcTextler[0].key;
                    npcTextler[0].key = "uyariYazisi";
                    npcler[0].serbest = true;
                    diyalogActi = true;
                    npcler[0].diyalogAc();
                }
            }
            if (diyalogActi)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 2.5f;
                    StartCoroutine(diyalogDegistir());
                }
            }
        }
    }
    IEnumerator yeniSahneGecis()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterKaydet();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
    IEnumerator diyalogDegistir()
    {
        npcler[0].diyalogKapat();
        yield return new WaitForSeconds(3f);
        npcTextler[0].key = ilkSefKey;
        npcler[0].serbest = false;
        diyalogActi = false;
    }
}
