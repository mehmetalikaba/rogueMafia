using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kaydetKontrol : MonoBehaviour
{
    public bool anaMenude, oyunlastirmada, oyunda;
    public bool oyunaBasladi, oyunlastirmaBitti;
    public int hangiSahnede;
    public float envanterCan, envanterAni, envanterEjder;
    public GameObject silah1, silah2, toplanabilirObje, ozelGuc1, ozelGuc2;
    public silahOzellikleri silah1Ozellikleri, silah2Ozellikleri;
    public bool[] yemekEtkileri = new bool[10];
    public scriptKontrol scriptKontrol;
    public envanterVerileri envanterVerileri;

    void Awake()
    {
        

        if (!anaMenude)
        {
            scriptKontrol = FindAnyObjectByType<scriptKontrol>();
            silah1 = GameObject.Find("silah1");
            silah2 = GameObject.Find("silah2");
            toplanabilirObje = GameObject.Find("toplanabilirEsyaOyuncu");
            ozelGuc1 = GameObject.Find("ozelGuc1");
            ozelGuc2 = GameObject.Find("ozelGuc2");
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            jsonEnvanterYukle();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num4Tusu")))
            jsonDosyasiniTemizle();
    }

    public void jsonEnvanterKaydet()
    {
        envanterVerileri data = new envanterVerileri
        {
            envanterCan = scriptKontrol.canKontrol.can,
            envanterAni = scriptKontrol.envanterKontrol.aniPuani,
            envanterEjder = scriptKontrol.envanterKontrol.ejderParasi,
            silah1Dayaniklilik = silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi,
            silah1Secimi = silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi,
            silah2Dayaniklilik = silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi,
            silah2Secimi = silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi,
            toplanabilirAciklamaKeyi = toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirAciklamaKeyi,
            ozelGuc1AciklamaKeyi = ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi,
            ozelGuc2AciklamaKeyi = ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi
        };

        for (int i = 0; i < data.yemekEtkileri.Length; i++)
        {
            data.yemekEtkileri[i] = yemekEtkileri[i];
        }

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        File.WriteAllText(path, json);

        Debug.Log("ENVANTER KAYDETTI <==> " + path);
    }

    public void jsonEnvanterYukle()
    {
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            envanterVerileri data = JsonUtility.FromJson<envanterVerileri>(json);

            Debug.Log("ENVANTER YUKLEDI <==> " + path);

            scriptKontrol.canKontrol.can = data.envanterCan;
            scriptKontrol.envanterKontrol.aniPuani = data.envanterAni;
            scriptKontrol.envanterKontrol.ejderParasi = data.envanterEjder;
            silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah1Dayaniklilik;
            silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi = data.silah1Secimi;
            silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah2Dayaniklilik;
            silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi = data.silah2Secimi;
            toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirAciklamaKeyi = data.toplanabilirAciklamaKeyi;
            ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc1AciklamaKeyi;
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc2AciklamaKeyi;

            for (int i = 0; i < data.yemekEtkileri.Length; i++)
            {
                yemekEtkileri[i] = data.yemekEtkileri[i];
            }

            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniYukle();
            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniUygula();
        }
        else
        {
            Debug.LogWarning("JSON dosyasý bulunamadý: " + path);
        }
    }

    public void jsonOyunlastirmaKaydet()
    {
        envanterVerileri data = new envanterVerileri
        {
            oyunaBasladi = oyunaBasladi,
            oyunlastirmaBitti = oyunlastirmaBitti,
            hangiSahnede = hangiSahnede
        };

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        File.WriteAllText(path, json);

        Debug.Log("OYUNLASTIRMA KAYDETTI <==> " + path);
    }

    public void jsonOyunlastirmaGetir()
    {
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            envanterVerileri data = JsonUtility.FromJson<envanterVerileri>(json);

            Debug.Log("OYUNLASTIRMA KAYITLARI GELDI <==> " + path);

            oyunaBasladi = data.oyunaBasladi;
            oyunlastirmaBitti = data.oyunlastirmaBitti;
            hangiSahnede = data.hangiSahnede;
        }
        else
        {
            Debug.LogWarning("JSON dosyasý bulunamadý: " + path);
        }
    }

    public void jsonOlumKaydet()
    {
        envanterVerileri data = new envanterVerileri
        {
            envanterEjder = scriptKontrol.envanterKontrol.ejderParasi,
            envanterAni = scriptKontrol.envanterKontrol.aniPuani / scriptKontrol.envanterKontrol.olunceAniMiktariAzalmaYuzdesi
        };

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        File.WriteAllText(path, json);

        Debug.Log("OLDUKTEN SONRA DA KAYDETTI <==> " + path);
    }

    public void jsonDosyasiniTemizle()
    {
        string json = "{ \"sesSeviyeleri\": [ 0.25, 0.25, 0.25, 0.25 ] }";
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        File.WriteAllText(path, json);

        Debug.Log("VERILER TEMIZLENDI <==> " + Application.dataPath);
    }
}