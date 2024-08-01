using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class oyunlastirma : MonoBehaviour
{
    public GameObject oyuncu, oyunPaneli, yetenekAgaciYazi, cikisTextObje, cikisKontrol, araBaseKontrol, yukleniyor;
    public GameObject[] npcObjeler;
    public Text[] textler;
    public bool ucretsizYemekSecti, sefKonustu, alfredKonustu, shifuKonustu, silahciKonustu, antikaciKonustu;

    oyuncuSaldiriTest oyuncuSaldiriTest;
    oyuncuHareket oyuncuHareket;
    sefPanelScripti sefPanelScripti;
    alfredPanelScripti alfredPanelScripti;
    Sifu sifu;
    silahciPanelScripti silahciPanelScripti;
    DuraklatmaMenusu duraklatmaMenusu;
    canKontrol canKontrol;
    public kaydetKontrol kaydetKontrol;

    public asamaKontrol[] asamaKontrolleri;
    public Npc[] npcler;

    void Start()
    {
        if (kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti)
        {
            Debug.Log("oyunlastirma bitti");

            //----------------------------------------------------------------
            this.enabled = false;
        }
        else if (!kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti)
        {
            Debug.Log("oyunlastirma bitmedi");

            //----------------------------------------------------------------

            canKontrol = FindObjectOfType<canKontrol>();
            oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
            duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
            oyuncuHareket = FindObjectOfType<oyuncuHareket>();
            sefPanelScripti = FindObjectOfType<sefPanelScripti>();
            alfredPanelScripti = FindObjectOfType<alfredPanelScripti>();
            sifu = FindObjectOfType<Sifu>();
            silahciPanelScripti = FindObjectOfType<silahciPanelScripti>();

            duraklatmaMenusu.duraklatmaKilitli = true;

            oyuncuHareket.atilmaKilitli = true;
            oyuncuHareket.inmeKilitli = true;
            oyuncuSaldiriTest.silahlarKilitli = true;
            oyuncuHareket.ziplamaKilitli = true;
            oyuncuHareket.hareketKilitli = true;

            alfredPanelScripti.ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGuclerKilitli = true;
            alfredPanelScripti.ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuclerKilitli = true;

            oyunPaneli.SetActive(false);

            oyuncu.transform.rotation = Quaternion.Euler(0, 180, 0);

            StartCoroutine(baslangicBekleme());

            if (canKontrol.can < 100)
                canKontrol.can = 100;
        }
    }

    void Update()
    {
        if (cikisKontrol.GetComponent<asamaKontrol>().oyuncuGeldi && (sefKonustu && alfredKonustu && silahciKonustu && shifuKonustu))
            cikisTextObje.SetActive(true);
        else
            cikisTextObje.SetActive(false);

        if (!cikisKontrol.activeSelf && (sefKonustu && alfredKonustu && silahciKonustu && shifuKonustu))
            cikisKontrol.SetActive(true);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && cikisKontrol.GetComponent<asamaKontrol>().oyuncuGeldi)
        {
            cikisTextObje.SetActive(false);
            yukleniyor.SetActive(true);
            oyunPaneli.SetActive(false);
            StartCoroutine(yeniSahneGecis());

        }

        if (sefPanelScripti.yemekSecti && !ucretsizYemekSecti)
        {
            ucretsizYemekSecti = true;
            sefPanelScripti.devamEt();
            oyunPaneli.SetActive(false);
            StartCoroutine(sefKonusmaBekleme());
        }

        if (oyuncuHareket.hareketKilitli)
        {
            if (!alfredKonustu && (alfredPanelScripti.ozelGuc1Secildi && alfredPanelScripti.ozelGuc2Secildi))
            {
                alfredKonustu = true;
                oyuncuHareket.hareketKilitli = false;
                npcler[1].diyalogKapat();
            }
            if (!silahciKonustu && (silahciPanelScripti.menzilliSecildi && silahciPanelScripti.yakinSecildi))
            {
                silahciKonustu = true;
                oyuncuHareket.hareketKilitli = false;
                oyunPaneli.SetActive(true);
                npcler[2].diyalogKapat();
            }
        }

        if (asamaKontrolleri[0].oyuncuGeldi == true)
            StartCoroutine(alfredBekleme());
        if (asamaKontrolleri[1].oyuncuGeldi == true)
            StartCoroutine(silahciBekleme());
        if (asamaKontrolleri[2].oyuncuGeldi == true)
            StartCoroutine(shifuBekleme());
        if (asamaKontrolleri[3].oyuncuGeldi == true)
            StartCoroutine(antikaciBekleme());
    }

    IEnumerator yeniSahneGecis()
    {
        kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti = true;
        kaydetKontrol.kaydetKontrolBaslangic.jsonBaslangicKaydet();
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterKaydet();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
    }

    IEnumerator baslangicBekleme()
    {
        yield return new WaitForSeconds(4f);
        npcler[0].diyalogAc();
        yield return new WaitForSeconds(3f);
        textler[0].GetComponent<localizedText>().key = "sef_baslangic2";
        yield return new WaitForSeconds(3f);
        textler[0].GetComponent<localizedText>().key = "sef_baslangic3";
        yield return new WaitForSeconds(3f);
        sefPanelScripti.durdur();
        sefPanelScripti.yemekUcretsiz = true;
        textler[5].GetComponent<localizedText>().key = "ucretsiz_yemek";
    }
    IEnumerator sefKonusmaBekleme()
    {
        textler[0].GetComponent<localizedText>().key = "sef_baslangic4";
        yield return new WaitForSeconds(2f);
        textler[0].GetComponent<localizedText>().key = "sef_baslangic4";
        npcler[0].diyalogKapat();
        yield return new WaitForSeconds(1f);
        sefKonustu = true;
        oyuncu.transform.rotation = Quaternion.Euler(0, 0, 0);
        oyuncuHareket.hareketKilitli = false;
        npcler[0].serbest = false;
    }

    IEnumerator alfredBekleme()
    {
        asamaKontrolleri[0].oyuncuGeldi = false;
        Destroy(asamaKontrolleri[0]);
        oyuncuHareket.hareketKilitli = true;
        textler[1].GetComponent<localizedText>().key = "alfred_baslangic1";
        npcler[1].diyalogAc();
        yield return new WaitForSeconds(2f);
        if (!alfredPanelScripti.alfredPanel.activeSelf)
            alfredPanelScripti.durdur();
    }
    IEnumerator silahciBekleme()
    {
        asamaKontrolleri[1].oyuncuGeldi = false;
        Destroy(asamaKontrolleri[1]);
        oyuncuHareket.hareketKilitli = true;
        textler[2].GetComponent<localizedText>().key = "silahci_baslangic1";
        npcler[2].diyalogAc();
        yield return new WaitForSeconds(2f);
        if (!silahciPanelScripti.silahciPaneli.activeSelf)
            silahciPanelScripti.durdur();
    }
    IEnumerator shifuBekleme()
    {
        asamaKontrolleri[2].oyuncuGeldi = false;
        Destroy(asamaKontrolleri[2]);
        oyuncuHareket.hareketKilitli = true;
        textler[3].GetComponent<localizedText>().key = "shifu_baslangic1";
        npcler[3].diyalogAc();
        yield return new WaitForSeconds(2f);
        sifu.aniAgaciAc();
        yield return new WaitForSeconds(5f);
        sifu.aniAgaciKapat();
        yield return new WaitForSeconds(1f);
        oyuncuHareket.hareketKilitli = false;
        npcler[3].diyalogKapat();
        shifuKonustu = true;
    }
    IEnumerator antikaciBekleme()
    {
        asamaKontrolleri[3].oyuncuGeldi = false;
        Destroy(asamaKontrolleri[3]);
        oyuncuHareket.hareketKilitli = true;
        asamaKontrolleri[3].enabled = false;
        textler[4].GetComponent<localizedText>().key = "antikaci_baslangic1";
        npcler[4].diyalogAc();
        yield return new WaitForSeconds(3f);
        oyuncuHareket.hareketKilitli = false;
        npcler[4].diyalogKapat();
        antikaciKonustu = true;
    }
}
