using System.IO;
using UnityEngine;

public class kaydetKontrol : MonoBehaviour
{
    public bool baslangicKlasoruOlusturuldu;
    public kaydetKontrolBaslangic kaydetKontrolBaslangic;
    public kaydetKontrolEnvanter kaydetKontrolEnvanter;
    public kaydetKontrolOzelEtkiler kaydetKontrolOzelEtkiler;
    public kaydetKontrolSes kaydetKontrolSes;
    public kaydetKontrolYetenek kaydetKontrolYetenek;

    void Awake()
    {
        if (kaydetKontrolBaslangic == null)
        {
            kaydetKontrolBaslangic = FindObjectOfType<kaydetKontrolBaslangic>();
        }
        if (kaydetKontrolEnvanter == null)
        {
            kaydetKontrolEnvanter = FindObjectOfType<kaydetKontrolEnvanter>();
        }
        if (kaydetKontrolOzelEtkiler == null)
        {
            kaydetKontrolOzelEtkiler = FindObjectOfType<kaydetKontrolOzelEtkiler>();
        }
        if (kaydetKontrolSes == null)
        {
            kaydetKontrolSes = FindObjectOfType<kaydetKontrolSes>();
        }
        if (kaydetKontrolYetenek == null)
        {
            kaydetKontrolYetenek = FindObjectOfType<kaydetKontrolYetenek>();
        }
    }
}
/*private static readonly string SAVE_FOLDER = Path.Combine(Application.persistentDataPath, "Axeagon", "Darkness of Memories");

void Awake()
{
    if (!baslangicKlasoruOlusturuldu)
    {
        baslangicKlasoruOlusturuldu = true;
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
            Debug.Log("Klasör oluþturuldu: " + SAVE_FOLDER);
        }
        kaydetKontrolBaslangic.jsonBaslangicOlustur();
        kaydetKontrolEnvanter.jsonEnvanterOlustur();
        kaydetKontrolOzelEtkiler.jsonOzelEtkilerOlustur();
        kaydetKontrolSes.jsonSesOlustur();
        kaydetKontrolYetenek.jsonYetenekOlustur();
    }

    // Manually assign components if not assigned in Inspector
    if (kaydetKontrolBaslangic == null)
    {
        kaydetKontrolBaslangic = GetComponent<kaydetKontrolBaslangic>();
    }
    if (kaydetKontrolEnvanter == null)
    {
        kaydetKontrolEnvanter = GetComponent<kaydetKontrolEnvanter>();
    }
    if (kaydetKontrolOzelEtkiler == null)
    {
        kaydetKontrolOzelEtkiler = GetComponent<kaydetKontrolOzelEtkiler>();
    }
    if (kaydetKontrolSes == null)
    {
        kaydetKontrolSes = GetComponent<kaydetKontrolSes>();
    }
    if (kaydetKontrolYetenek == null)
    {
        kaydetKontrolYetenek = GetComponent<kaydetKontrolYetenek>();
    }
}

void Start()
{
    // Additional initialization can be added here
}

public static void Init()
{
    if (!Directory.Exists(SAVE_FOLDER))
        Directory.CreateDirectory(SAVE_FOLDER);
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

public static string GetSavePath(string fileName)
{
    return Path.Combine(SAVE_FOLDER, fileName);
}
}






/*using System.IO;
using UnityEngine;

public class kaydetKontrol : MonoBehaviour
{
public bool baslangicKlasoruOlusturuldu;
public kaydetKontrolBaslangic kaydetKontrolBaslangic;
public kaydetKontrolEnvanter kaydetKontrolEnvanter;
public kaydetKontrolOzelEtkiler kaydetKontrolOzelEtkiler;
public kaydetKontrolSes kaydetKontrolSes;
public kaydetKontrolYetenek kaydetKontrolYetenek;

private static readonly string SAVE_FOLDER = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Axeagon Games", "Darkness Of Memories");

void Awake()
{
    if (!baslangicKlasoruOlusturuldu)
    {
        baslangicKlasoruOlusturuldu = true;
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
            Debug.Log("Klasör oluþturuldu: " + SAVE_FOLDER);
        }
        kaydetKontrolBaslangic.jsonBaslangicOlustur();
        kaydetKontrolEnvanter.jsonEnvanterOlustur();
        kaydetKontrolOzelEtkiler.jsonOzelEtkilerOlustur();
        kaydetKontrolSes.jsonSesOlustur();
        kaydetKontrolYetenek.jsonYetenekOlustur();
    }

    // Manually assign components if not assigned in Inspector
    if (kaydetKontrolBaslangic == null)
    {
        kaydetKontrolBaslangic = GetComponent<kaydetKontrolBaslangic>();
    }
    if (kaydetKontrolEnvanter == null)
    {
        kaydetKontrolEnvanter = GetComponent<kaydetKontrolEnvanter>();
    }
    if (kaydetKontrolOzelEtkiler == null)
    {
        kaydetKontrolOzelEtkiler = GetComponent<kaydetKontrolOzelEtkiler>();
    }
    if (kaydetKontrolSes == null)
    {
        kaydetKontrolSes = GetComponent<kaydetKontrolSes>();
    }
    if (kaydetKontrolYetenek == null)
    {
        kaydetKontrolYetenek = GetComponent<kaydetKontrolYetenek>();
    }
}

void Start()
{
    // Additional initialization can be added here
}

public static void Init()
{
    if (!Directory.Exists(SAVE_FOLDER))
        Directory.CreateDirectory(SAVE_FOLDER);
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

public static string GetSavePath(string fileName)
{
    return Path.Combine(SAVE_FOLDER, fileName);
}
}
*/