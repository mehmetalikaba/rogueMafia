using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class araBaseKontrol : MonoBehaviour
{
    float timer;
    public string ilkSefKey;
    public Npc[] npcler;
    public localizedText[] npcTextler;
    public bool silahciKonustu, alfredKonustu, diyalogActi;
    public GameObject cikisKontrol, cikisTextObje, silahciPanel, alfredPanel, shifuPanel, sefPanel;

    public kaydetKontrol kaydetKontrol;
    public alfredPanelScripti alfredPanelScripti;
    public silahciPanelScripti silahciPanelScripti;
    public sefPanelScripti sefPanelScripti;

    void Awake()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterYukle();
    }

    void Start()
    {
        Debug.Log(gameObject.name);
    }

    void Update()
    {
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
