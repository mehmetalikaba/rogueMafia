[System.Serializable]
public class envanterVerileri
{
    public bool oyunaBasladi, oyunlastirmaBitti;
    public int hangiSahnede;
    public float envanterCan, envanterAni, envanterEjder, silah1Dayaniklilik, silah2Dayaniklilik;
    public string ozelGuc1AciklamaKeyi, ozelGuc2AciklamaKeyi, toplanabilirAciklamaKeyi;
    public silahSecimi silah1Secimi, silah2Secimi;
    public bool[] yemekEtkileri = new bool[10];
    public float envanterSes0 = 0.25f, envanterSes1 = 0.25f, envanterSes2 = 0.25f, envanterSes3 = 0.25f;

}