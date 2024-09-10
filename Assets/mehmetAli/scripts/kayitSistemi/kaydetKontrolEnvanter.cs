using System.IO;
using UnityEngine;

public class kaydetKontrolEnvanter : MonoBehaviour
{
    public verilerEnvanter data = new verilerEnvanter();
    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, iksir;
    public kaydetKontrol kaydetKontrol;
    envanterKontrol envanterKontrol;
    public string path;

    public canKontrol canKontrol;

    private void Awake()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        path = Path.Combine(Application.persistentDataPath, "verilerEnvanter.json");
    }

    void Start()
    {

    }

    public void jsonEnvanterKaydet()
    {
        data.envanterAni = canKontrol.envanterKontrol.aniPuani;
        data.envanterCan = canKontrol.baslangicCani;
        data.envanterEjder = canKontrol.envanterKontrol.ejderParasi;
        data.silah1AciklamaKeyi = silah1.GetComponent<silahOzellikleriniGetir>().elindekiSilah.aciklamaKeyi;
        data.silah2AciklamaKeyi = silah2.GetComponent<silahOzellikleriniGetir>().elindekiSilah.aciklamaKeyi;
        data.silah1Dayaniklilik = silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
        data.silah2Dayaniklilik = silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
        data.ozelGuc1AciklamaKeyi = ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi;
        data.ozelGuc2AciklamaKeyi = ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi;
        if (iksir.GetComponent<iksirKullanmaScripti>().eldekiIksir != null)
            data.iksirAciklamaKeyi = iksir.GetComponent<iksirKullanmaScripti>().iksirAciklamaKeyi;

        path = Path.Combine(Application.persistentDataPath, "verilerEnvanter.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> ENVANTER " + path);
    }

    public void jsonEnvanterYukle()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerEnvanter.json");

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerEnvanter>(json);

        canKontrol.envanterKontrol.aniPuani = data.envanterAni;
        canKontrol.baslangicCani = data.envanterCan;
        canKontrol.envanterKontrol.ejderParasi = data.envanterEjder;
        silah1.GetComponent<silahOzellikleriniGetir>().simdikiSilah = data.silah1AciklamaKeyi;
        silah2.GetComponent<silahOzellikleriniGetir>().simdikiSilah = data.silah2AciklamaKeyi;
        silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah1Dayaniklilik;
        silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah2Dayaniklilik;
        ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc1AciklamaKeyi;
        ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc2AciklamaKeyi;
        iksir.GetComponent<iksirKullanmaScripti>().iksirAciklamaKeyi = data.iksirAciklamaKeyi;

        silah1.GetComponent<silahOzellikleriniGetir>().seciliSilahinBilgileriniGetir();
        silah2.GetComponent<silahOzellikleriniGetir>().seciliSilahinBilgileriniGetir();

        Debug.Log("YUKLEDI <==> ENVANTER " + path);
    }

    public void olunceEnvanterKaydet()
    {
        data.envanterAni = canKontrol.envanterKontrol.aniPuani / envanterKontrol.olunceAniMiktariAzalmaYuzdesi;

        path = Path.Combine(Application.persistentDataPath, "verilerEnvanter.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> ENVANTER " + path);
    }
    public void doguncaEnvanterGetir()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerEnvanter.json");

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerEnvanter>(json);

        canKontrol.envanterKontrol.aniPuani = data.envanterAni;
    }
}
