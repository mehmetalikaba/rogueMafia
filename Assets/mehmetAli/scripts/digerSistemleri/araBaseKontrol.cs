using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class araBaseKontrol : MonoBehaviour
{

    public GameObject cikisKontrol, cikisTextObje;
    public scriptKontrol scriptKontrol;

    public Npc[] npcler;
    public localizedText[] npcTextler;


    void Start()
    {
        scriptKontrol.kaydetKontrol.jsonEnvanterKaydet();
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
            StartCoroutine(yeniSahneGecis());

        if (cikisKontrol.GetComponent<asamaKontrol>().oyuncuGeldi)
            cikisTextObje.SetActive(true);
        else
            cikisTextObje.SetActive(false);
    }

    IEnumerator yeniSahneGecis()
    {
        scriptKontrol.kaydetKontrol.jsonEnvanterKaydet();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

}
