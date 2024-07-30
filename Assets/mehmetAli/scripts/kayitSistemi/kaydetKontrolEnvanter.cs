using System.IO;
using UnityEngine;

public class kaydetKontrolEnvanter : MonoBehaviour
{
    public verilerEnvanter data = new verilerEnvanter();
    public GameObject silah1, silah2, ozelGuc1, ozelGuc2, toplanabilir;
    public kaydetKontrol kaydetKontrol;
    public string path;

    public canKontrol canKontrol;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        path = Path.Combine(Application.persistentDataPath, "verilerEnvanter.json");
    }
    public void jsonEnvanterKaydet()
    {
        data.envanterCan = canKontrol.baslangicCani;
        data.envanterAni = canKontrol.envanterKontrol.aniPuani;
        data.envanterEjder = canKontrol.envanterKontrol.ejderParasi;
        data.silah1Secimi = silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi;
        data.silah2Secimi = silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi;
        data.silah1Dayaniklilik = silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
        data.silah2Dayaniklilik = silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
        data.ozelGuc1AciklamaKeyi = ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi;
        data.ozelGuc2AciklamaKeyi = ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi;
        data.toplanabilirAciklamaKeyi = toplanabilir.GetComponent<toplanabilirKullanmaScripti>().toplanabilirAciklamaKeyi;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> ENVANTER " + path);
    }

    public void jsonEnvanterYukle()
    {
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerEnvanter>(json);

        canKontrol.baslangicCani = data.envanterCan;
        canKontrol.envanterKontrol.aniPuani = data.envanterAni;
        canKontrol.envanterKontrol.ejderParasi = data.envanterEjder;
        silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi = data.silah1Secimi;
        silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi = data.silah2Secimi;
        silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah1Dayaniklilik;
        silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah2Dayaniklilik;
        ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc1AciklamaKeyi;
        ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc2AciklamaKeyi;
        toplanabilir.GetComponent<toplanabilirKullanmaScripti>().toplanabilirAciklamaKeyi = data.toplanabilirAciklamaKeyi;

        Debug.Log("YUKLEDI <==> ENVANTER " + path);
    }

    public void olunceEnvanterKaydet()
    {
        data.envanterAni = canKontrol.envanterKontrol.aniPuani * 0.5f;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> OLUNCE " + path);
    }
}
