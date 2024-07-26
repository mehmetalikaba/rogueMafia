using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class araBaseKontrol : MonoBehaviour
{

    public GameObject cikisKontrol, cikisTextObje;
    public scriptKontrol scriptKontrol;

    public Npc[] npcler;
    public localizedText[] npcTextler;

    public GameObject silah1, silah2, ozelGuc1, ozelGuc2;


    void Start()
    {
        silah1 = GameObject.Find("silah1");
        silah2 = GameObject.Find("silah2");
        ozelGuc1 = GameObject.Find("ozelGuc1");
        ozelGuc2 = GameObject.Find("ozelGuc2");


        scriptKontrol.kaydetKontrol.jsonKaydet();
        scriptKontrol.sefPanelScripti.etkilesimKilitli = false;
        scriptKontrol.alfredPanelScripti.etkilesimKilitli = false;
        scriptKontrol.silahciPanelScripti.etkilesimKilitli = false;
        scriptKontrol.sefPanelScripti.yemekUcretsiz = false;

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

        scriptKontrol.oyuncuSaldiriTest.silahlarKilitli = true;
        scriptKontrol.oyuncuHareket.ziplamaKilitli = true;
        scriptKontrol.ozelGuc1KullanmaScripti.ozelGuclerKilitli = true;
        scriptKontrol.ozelGuc2KullanmaScripti.ozelGuclerKilitli = true;

        scriptKontrol.kaydetKontrol.envanterAni = scriptKontrol.envanterKontrol.aniPuani;
        scriptKontrol.kaydetKontrol.envanterEjder = scriptKontrol.envanterKontrol.ejderParasi;
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && cikisKontrol.GetComponent<asamaKontrol>().oyuncuGeldi)
            if (silah1.GetComponent<silahOzellikleriniGetir>().secilenSilahOzellikleri != null && ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi != null)
                StartCoroutine(yeniSahneGecis());

        if (cikisKontrol.GetComponent<asamaKontrol>().oyuncuGeldi)
            cikisTextObje.SetActive(true);
        else
            cikisTextObje.SetActive(false);
    }

    IEnumerator yeniSahneGecis()
    {
        scriptKontrol.kaydetKontrol.jsonKaydet();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

}
