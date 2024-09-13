using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alfredPanelScripti : MonoBehaviour
{
    public GameObject daireButon;
    public bool oyuncuYakin, ozelGuc1Secildi, ozelGuc2Secildi, randomOzelGuclerGeldi, etkilesimKilitli;
    public Button buton1, buton2, buton3;
    public Image guc1Image, guc2Image, guc3Image;
    public GameObject oyunPaneli, alfredPanel, ozelGuc1, ozelGuc2;
    public Text ozelGuc1Adi, ozelGuc2Adi, ozelGuc3Adi, alfredDiyalog;
    public localizedText aciklama1, aciklama2, aciklama3, aciklamaText;
    public GameObject[] ozelGucObjeleri;
    public List<int> secilenOzelGucler = new List<int>();
    public araBaseKontrol araBaseKontrol;
    public anaBaseKontrol anaBaseKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public DuraklatmaMenusu duraklatmaMenusu;
    public oyuncuHareket oyuncuHareket;
    public GameObject alfredPelerinAcma, idleAlfred;
    public Npc npc;

    public void Start()
    {
        araBaseKontrol = FindObjectOfType<araBaseKontrol>();
        anaBaseKontrol = FindObjectOfType<anaBaseKontrol>();
        aciklamaText.GetComponent<localizedText>().key = "secim1_key";
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
        {
            daireButon.SetActive(true);
            alfredPelerinAcma.SetActive(true);
            idleAlfred.SetActive(false);
        }
        else if (!oyuncuYakin)
        {
            daireButon.SetActive(false);
            alfredPelerinAcma.SetActive(false);
            idleAlfred.SetActive(true);
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !alfredPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && alfredPanel.activeSelf)
                devamEt();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !alfredPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && alfredPanel.activeSelf)
                devamEt();
        }
    }

    public void randomOzelGucGetir()
    {
        randomOzelGuclerGeldi = true;
        while (secilenOzelGucler.Count < 3)
        {
            int rastgeleSayi = Random.Range(0, ozelGucObjeleri.Length);
            if (!secilenOzelGucler.Contains(rastgeleSayi))
            {
                secilenOzelGucler.Add(rastgeleSayi);
            }
        }

        ozelGuc1Adi.GetComponent<localizedText>().key = ozelGucObjeleri[secilenOzelGucler[0]].GetComponent<ozelGucOzellikleri>().ozelGucAd;
        ozelGuc2Adi.GetComponent<localizedText>().key = ozelGucObjeleri[secilenOzelGucler[1]].GetComponent<ozelGucOzellikleri>().ozelGucAd;
        ozelGuc3Adi.GetComponent<localizedText>().key = ozelGucObjeleri[secilenOzelGucler[2]].GetComponent<ozelGucOzellikleri>().ozelGucAd;

        aciklama1.key = ozelGucObjeleri[secilenOzelGucler[0]].GetComponent<ozelGucOzellikleri>().ozelGucAciklamaKeyi;
        aciklama2.key = ozelGucObjeleri[secilenOzelGucler[1]].GetComponent<ozelGucOzellikleri>().ozelGucAciklamaKeyi;
        aciklama3.key = ozelGucObjeleri[secilenOzelGucler[2]].GetComponent<ozelGucOzellikleri>().ozelGucAciklamaKeyi;

        guc1Image.sprite = ozelGucObjeleri[secilenOzelGucler[0]].GetComponent<ozelGucOzellikleri>().ozelGucIconu;
        guc2Image.sprite = ozelGucObjeleri[secilenOzelGucler[1]].GetComponent<ozelGucOzellikleri>().ozelGucIconu;
        guc3Image.sprite = ozelGucObjeleri[secilenOzelGucler[2]].GetComponent<ozelGucOzellikleri>().ozelGucIconu;
    }
    public void ozelGucSecimButonu1()
    {
        ozelGucSecimIslemi(secilenOzelGucler[0], buton1);
    }
    public void ozelGucSecimButonu2()
    {
        ozelGucSecimIslemi(secilenOzelGucler[1], buton2);
    }
    public void ozelGucSecimButonu3()
    {
        ozelGucSecimIslemi(secilenOzelGucler[2], buton3);
    }
    public void ozelGucSecimIslemi(int secilenOzelGuc, Button buton)
    {
        if (!ozelGuc1Secildi)
        {
            aciklamaText.key = "secim2_key";
            ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuc1Image.sprite = ozelGucObjeleri[secilenOzelGuc].GetComponent<SpriteRenderer>().sprite;
            ozelGuc1Secildi = true;
        }
        else
        {
            aciklamaText.key = "secim2_key";
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucObjesi = ozelGucObjeleri[secilenOzelGuc];
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGuc2Image.sprite = ozelGucObjeleri[secilenOzelGuc].GetComponent<SpriteRenderer>().sprite;
            ozelGuc2Secildi = true;
        }
        buton.interactable = false;

        if (ozelGuc1Secildi && ozelGuc2Secildi)
            devamEt();
    }
    public void durdur()
    {
        //duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
        if (!randomOzelGuclerGeldi)
            randomOzelGucGetir();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        alfredPanel.SetActive(true);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        alfredPanel.SetActive(false);
        oyunPaneli.SetActive(true);
        //duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
        if (ozelGuc1Secildi && ozelGuc2Secildi)
        {
            if (anaBaseKontrol != null)
                anaBaseKontrol.alfredKonustu = true;
            if (araBaseKontrol != null)
                araBaseKontrol.alfredKonustu = true;
            alfredDiyalog.GetComponent<localizedText>().key = "alfred_bitti";
            alfredPelerinAcma.SetActive(false);
            idleAlfred.SetActive(true);
            daireButon.SetActive(false);
            npc.GetComponent<Npc>();
            npc.enabled = false;
            this.enabled = false;

        }
    }
}
