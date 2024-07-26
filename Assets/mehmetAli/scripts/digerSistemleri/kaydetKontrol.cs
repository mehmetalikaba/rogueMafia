using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86;

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

    private static kaydetKontrol instance;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            jsonEnvanterYukle();

        if (!anaMenude)
        {
            scriptKontrol = FindAnyObjectByType<scriptKontrol>();
            silah1 = GameObject.Find("silah1");
            silah2 = GameObject.Find("silah2");
            toplanabilirObje = GameObject.Find("toplanabilirEsyaOyuncu");
            ozelGuc1 = GameObject.Find("ozelGuc1");
            ozelGuc2 = GameObject.Find("ozelGuc2");
        }

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void jsonKaydet()
    {
        envanterVerileri data = new envanterVerileri();

        data.oyunaBasladi = oyunaBasladi;
        data.oyunlastirmaBitti = oyunlastirmaBitti;
        data.hangiSahnede = hangiSahnede;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            data.envanterCan = scriptKontrol.canKontrol.can;
            data.envanterAni = scriptKontrol.envanterKontrol.aniPuani;
            data.envanterEjder = scriptKontrol.envanterKontrol.ejderParasi;
            data.silah1Dayaniklilik = silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
            data.silah2Dayaniklilik = silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi;
            data.ozelGuc1AciklamaKeyi = ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi;
            data.ozelGuc2AciklamaKeyi = ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi;
            data.toplanabilirAciklamaKeyi = toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirAciklamaKeyi;
            data.silah1Secimi = silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi;
            data.silah2Secimi = silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi;

            for (int i = 0; i < data.yemekEtkileri.Length; i++)
            {
                data.yemekEtkileri[i] = yemekEtkileri[i];
            }
        }
        data.envanterSes0 = scriptKontrol.sesKontrol.ses0;
        data.envanterSes1 = scriptKontrol.sesKontrol.ses1;
        data.envanterSes2 = scriptKontrol.sesKontrol.ses2;
        data.envanterSes3 = scriptKontrol.sesKontrol.ses3;

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        File.WriteAllText(path, json);

        Debug.Log(" KAYDETTI <==> " + path);
    }

    public void jsonEnvanterYukle()
    {
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            envanterVerileri data = JsonUtility.FromJson<envanterVerileri>(json);

            Debug.Log("ENVANTER YUKLEDI <==> " + path);

            oyunaBasladi = data.oyunaBasladi;
            oyunlastirmaBitti = data.oyunlastirmaBitti;
            hangiSahnede = data.hangiSahnede;
            scriptKontrol.canKontrol.can = data.envanterCan;
            scriptKontrol.envanterKontrol.aniPuani = data.envanterAni;
            scriptKontrol.envanterKontrol.ejderParasi = data.envanterEjder;
            silah1.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah1Dayaniklilik;
            silah2.GetComponent<silahOzellikleriniGetir>().silahDayanikliligi = data.silah2Dayaniklilik;
            ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc1AciklamaKeyi;
            ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi = data.ozelGuc2AciklamaKeyi;
            toplanabilirObje.GetComponent<toplanabilirKullanmaScripti>().toplanabilirAciklamaKeyi = data.toplanabilirAciklamaKeyi;
            silah1.GetComponent<silahOzellikleriniGetir>().silahSecimi = data.silah1Secimi;
            silah2.GetComponent<silahOzellikleriniGetir>().silahSecimi = data.silah2Secimi;

            for (int i = 0; i < data.yemekEtkileri.Length; i++)
            {
                yemekEtkileri[i] = data.yemekEtkileri[i];
            }

            scriptKontrol.sesKontrol.ses0 = data.envanterSes0;
            scriptKontrol.sesKontrol.ses1 = data.envanterSes1;
            scriptKontrol.sesKontrol.ses2 = data.envanterSes2;
            scriptKontrol.sesKontrol.ses3 = data.envanterSes3;

            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniKaydet();
            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniYukle();
            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniUygula();
        }
    }

    public void jsonAraBaseYukle()
    {
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            envanterVerileri data = JsonUtility.FromJson<envanterVerileri>(json);

            Debug.Log("ENVANTER YUKLEDI <==> " + path);

            oyunaBasladi = data.oyunaBasladi;
            oyunlastirmaBitti = data.oyunlastirmaBitti;
            hangiSahnede = data.hangiSahnede;
            scriptKontrol.canKontrol.can = data.envanterCan;
            scriptKontrol.envanterKontrol.aniPuani = data.envanterAni;
            scriptKontrol.envanterKontrol.ejderParasi = data.envanterEjder;

            scriptKontrol.sesKontrol.ses0 = data.envanterSes0;
            scriptKontrol.sesKontrol.ses1 = data.envanterSes1;
            scriptKontrol.sesKontrol.ses2 = data.envanterSes2;
            scriptKontrol.sesKontrol.ses3 = data.envanterSes3;

        }
    }

    public void jsonOlumKaydet()
    {
        envanterVerileri data = new envanterVerileri();

        data.oyunaBasladi = oyunaBasladi;
        data.oyunlastirmaBitti = oyunlastirmaBitti;
        data.hangiSahnede = hangiSahnede;
        data.envanterAni = scriptKontrol.envanterKontrol.aniPuani;
        data.envanterEjder = scriptKontrol.envanterKontrol.ejderParasi;
        data.envanterSes0 = scriptKontrol.sesKontrol.ses0;
        data.envanterSes1 = scriptKontrol.sesKontrol.ses1;
        data.envanterSes2 = scriptKontrol.sesKontrol.ses2;
        data.envanterSes3 = scriptKontrol.sesKontrol.ses3;

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "envanterVerileriFile.json");
        File.WriteAllText(path, json);

        Debug.Log(" YOU DIED <==> " + path);
    }


    private static readonly string SAVE_FOLDER = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "AxeagonGames", "DarknessOfMemories");

    public static void Init()
    {
        // Test if Save Folder exists
        if (!Directory.Exists(SAVE_FOLDER))
        {
            // Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        int saveNumber = 1;
        string filePath;
        while (File.Exists(filePath = Path.Combine(SAVE_FOLDER, $"save_{saveNumber}.json")))
        {
            saveNumber++;
        }
        File.WriteAllText(filePath, saveString);
        Debug.Log($"Save file saved at {filePath}");
    }

    public static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.json");
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentFile == null || fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
            {
                mostRecentFile = fileInfo;
            }
        }
        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            Debug.Log($"Loaded save file from {mostRecentFile.FullName}");
            return saveString;
        }
        else
        {
            Debug.Log("No save files found.");
            return null;
        }
    }
}