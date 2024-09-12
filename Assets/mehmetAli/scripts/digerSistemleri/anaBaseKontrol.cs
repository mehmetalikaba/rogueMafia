using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anaBaseKontrol : MonoBehaviour
{
    public string hangiSahnede;
    public GameObject yukleniyorEkrani, oyunEkrani;

    float timer;
    string ilkShifuKey;
    public Npc[] npcler;
    public localizedText[] npcTextler;
    public bool silahciKonustu, alfredKonustu, diyalogActi;
    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, cikisKontrol, cikisTextObje, silahciPanel, alfredPanel, shifuPanel;

    public canKontrol canKontrol;
    public kaydetKontrol kaydetKontrol;
    public oyuncuHareket oyuncuHareket;
    public envanterKontrol envanterKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public alfredPanelScripti alfredPanelScripti;
    public silahciPanelScripti silahciPanelScripti;

    void Start()
    {
        if (hangiSahnede == "dogum")
            kaydetKontrol.kaydetKontrolEnvanter.doguncaEnvanterGetir();

        oyuncuHareket.ziplamaKilitli = true;
        oyuncuHareket.inmeKilitli = true;
        oyuncuSaldiriTest.silahlarKilitli = true;
        ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGuclerKilitli = true;
        ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuclerKilitli = true;

        alfredPanelScripti.etkilesimKilitli = false;
        silahciPanelScripti.etkilesimKilitli = false;

        for (int i = 0; i < npcler.Length; i++)
            npcler[i].serbest = false;

        cikisKontrol.SetActive(true);

        npcTextler[0].key = "alfred_key";
        npcTextler[1].key = "shifu_key";
        npcTextler[2].key = "silahci_key";
        npcTextler[3].key = "antikaci_key";

        ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = null;
        ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = null;

        canKontrol = FindObjectOfType<canKontrol>();
        canKontrol.baslangicCani = 100f;
    }

    void Update()
    {
        oyuncuSaldiriTest.silahlarKilitli = true;
        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        ozelGuc1 = GameObject.Find("ozelGuc1");
        ozelGuc2 = GameObject.Find("ozelGuc2");

        if (silahciKonustu && alfredKonustu)
        {
            cikisTextObje.SetActive(true);

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                oyunEkrani.SetActive(false);
                yukleniyorEkrani.SetActive(true);
                StartCoroutine(yeniSahneGecis());
            }
        }
        else
            cikisTextObje.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) || Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu")))
        {
            if (silahciKonustu && !silahciPanel.activeSelf && !alfredPanel.activeSelf && !shifuPanel.activeSelf)
            {
                timer = 2.5f;
                ilkShifuKey = npcTextler[0].key;
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
    IEnumerator yeniSahneGecis()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterKaydet();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
    IEnumerator diyalogDegistir()
    {
        npcler[0].diyalogKapat();
        yield return new WaitForSeconds(3f);
        npcTextler[0].key = ilkShifuKey;
        npcler[0].serbest = false;
        diyalogActi = false;
    }
}