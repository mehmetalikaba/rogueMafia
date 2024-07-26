using UnityEngine;
[System.Serializable]
public class envanterVerileri
{
    public bool yuklenecek, kaydedilecek, kayitKilitli, oyunaBasladi, oyunlastirmaBitti;
    public int hangiSahnede;
    public float envanterCan, envanterAni, envanterEjder, silah1Dayaniklilik, silah2Dayaniklilik;
    public string ozelGuc1AciklamaKeyi, ozelGuc2AciklamaKeyi, toplanabilirAciklamaKeyi;
    public silahSecimi silah1Secimi, silah2Secimi;
    public bool[] yemekEtkileri = new bool[10];
    public float envanterSes0, envanterSes1, envanterSes2, envanterSes3;

}