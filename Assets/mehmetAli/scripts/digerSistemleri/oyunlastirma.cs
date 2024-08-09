using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class oyunlastirma : MonoBehaviour
{
    public GameObject oyuncu, oyunPaneli, yetenekAgaciYazi, cikisTextObje, cikisKontrol, araBaseKontrol, yukleniyor;
    public GameObject[] npcObjeler;
    public Text[] textler;
    public bool ucretsizYemekSecti, sefKonustu, silahciKonustu, alfredKonustu, shifuKonustu, antikaciKonustu;

    oyuncuSaldiriTest oyuncuSaldiriTest;
    oyuncuHareket oyuncuHareket;
    sefPanelScripti sefPanelScripti;
    alfredPanelScripti alfredPanelScripti;
    shifuPanelScripti shifuPanelScripti;
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
            this.enabled = false;
        }
        else if (!kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti)
        {
            canKontrol = FindObjectOfType<canKontrol>();
            oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
            duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
            oyuncuHareket = FindObjectOfType<oyuncuHareket>();
            sefPanelScripti = FindObjectOfType<sefPanelScripti>();
            alfredPanelScripti = FindObjectOfType<alfredPanelScripti>();
            shifuPanelScripti = FindObjectOfType<shifuPanelScripti>();
            silahciPanelScripti = FindObjectOfType<silahciPanelScripti>();

            oyunPaneli.SetActive(false);
            
            oyuncu.transform.rotation = Quaternion.Euler(0, 180, 0);

            StartCoroutine(baslangicBekleme());

            if (canKontrol.can < canKontrol.baslangicCani)
                canKontrol.can = canKontrol.baslangicCani;
        }
    }

    void Update()
    {
        duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.atilmaKilitli = true;
        oyuncuHareket.inmeKilitli = true;
        oyuncuSaldiriTest.silahlarKilitli = true;
        oyuncuHareket.ziplamaKilitli = true;
        alfredPanelScripti.ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGuclerKilitli = true;
        alfredPanelScripti.ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuclerKilitli = true;

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

        if (!sefKonustu)
            oyuncuHareket.hareketKilitli = true;

        if (!silahciKonustu && (silahciPanelScripti.menzilliSecildi && silahciPanelScripti.yakinSecildi))
        {
            silahciKonustu = true;
            oyunPaneli.SetActive(true);
            npcler[2].diyalogKapat();
        }
        if (!alfredKonustu && (alfredPanelScripti.ozelGuc1Secildi && alfredPanelScripti.ozelGuc2Secildi))
        {
            alfredKonustu = true;
            npcler[1].diyalogKapat();
        }
        if (!shifuKonustu && (shifuPanelScripti.shifuPanel.activeSelf))
        {
            shifuKonustu = true;
            npcler[3].diyalogKapat();
        }

        if (asamaKontrolleri[0].oyuncuGeldi == true)
            StartCoroutine(silahciBekleme());
        if (asamaKontrolleri[1].oyuncuGeldi == true)
            StartCoroutine(alfredBekleme());
        if (asamaKontrolleri[2].oyuncuGeldi == true)
            StartCoroutine(shifuBekleme());
        if (asamaKontrolleri[3].oyuncuGeldi == true)
            StartCoroutine(antikaciBekleme());
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
        yield return new WaitForSeconds(0.05f);
        oyuncuHareket.hareketKilitli = false;
        oyuncu.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    IEnumerator silahciBekleme()
    {
        asamaKontrolleri[0].oyuncuGeldi = false;
        asamaKontrolleri[0].enabled = false;
        oyuncuHareket.hareketKilitli = true;
        textler[2].GetComponent<localizedText>().key = "silahci_baslangic1";
        npcler[2].diyalogAc();
        yield return new WaitForSeconds(2f);
        if (!silahciPanelScripti.silahciPaneli.activeSelf)
            silahciPanelScripti.durdur();
    }
    IEnumerator alfredBekleme()
    {
        asamaKontrolleri[1].oyuncuGeldi = false;
        asamaKontrolleri[1].enabled = false;
        oyuncuHareket.hareketKilitli = true;
        textler[1].GetComponent<localizedText>().key = "alfred_baslangic1";
        npcler[1].diyalogAc();
        yield return new WaitForSeconds(2f);
        if (!alfredPanelScripti.alfredPanel.activeSelf)
            alfredPanelScripti.durdur();
    }
    IEnumerator shifuBekleme()
    {
        asamaKontrolleri[2].oyuncuGeldi = false;
        asamaKontrolleri[2].enabled = false;
        oyuncuHareket.hareketKilitli = true;
        textler[3].GetComponent<localizedText>().key = "shifu_baslangic1";
        npcler[3].diyalogAc();
        yield return new WaitForSeconds(2f);
        if (!shifuPanelScripti.shifuPanel.activeSelf)
            shifuPanelScripti.durdur();
    }
    IEnumerator antikaciBekleme()
    {
        asamaKontrolleri[3].oyuncuGeldi = false;
        asamaKontrolleri[3].enabled = false;
        oyuncuHareket.hareketKilitli = true;
        asamaKontrolleri[3].enabled = false;
        textler[4].GetComponent<localizedText>().key = "antikaci_baslangic1";
        npcler[4].diyalogAc();
        yield return new WaitForSeconds(3f);
        oyuncuHareket.hareketKilitli = false;
        npcler[4].diyalogKapat();
        antikaciKonustu = true;
    }
    IEnumerator yeniSahneGecis()
    {
        kaydetKontrol.kaydetKontrolBaslangic.oyunlastirmaBitti = true;
        kaydetKontrol.kaydetKontrolBaslangic.jsonBaslangicKaydet();
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterKaydet();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
