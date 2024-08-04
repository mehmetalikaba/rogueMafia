using UnityEngine;

public class yetenekKontrol : MonoBehaviour
{
    public shifuPanelScripti shifuPanelScripti;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public envanterKontrol envanterKontrol;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;

    void Start()
    {
        shifuPanelScripti = FindObjectOfType<shifuPanelScripti>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    public void yetenekleriUygula()
    {
        if (shifuPanelScripti.menzilliYetenek[0])
            oyuncuSaldiriTest.bonusHasarlarMenzilli += 5f;

        if (shifuPanelScripti.menzilliYetenek[1])
            oyuncuSaldiriTest.bonusHasarlarMenzilli += 10f;

        if (shifuPanelScripti.menzilliYetenek[2])
            oyuncuSaldiriTest.silah2DayanikliligiBonus = oyuncuSaldiriTest.silah2DayanikliligiBonus * 1.5f;

        if (shifuPanelScripti.pasifYetenek[0])
            envanterKontrol.olunceAniMiktariAzalmaYuzdesi = 1.5f;

        if (shifuPanelScripti.pasifYetenek[1])
            ozelGuc2KullanmaScripti.ozelGuc2ToplamSure = 7.5f; ozelGuc1KullanmaScripti.ozelGuc1ToplamSure = 7.5f;

        if (shifuPanelScripti.pasifYetenek[2])
            envanterKontrol.ejderhaPuaniArtmaMiktari = 75f;

        if (shifuPanelScripti.yakinYetenek[0])
            oyuncuSaldiriTest.bonusHasarlarYakin += 5f;

        if (shifuPanelScripti.yakinYetenek[1])
            oyuncuSaldiriTest.bonusHasarlarYakin += 10f;

        if (shifuPanelScripti.yakinYetenek[2])
            oyuncuSaldiriTest.silah1DayanikliligiBonus = oyuncuSaldiriTest.silah1DayanikliligiBonus * 1.5f;
    }

}
