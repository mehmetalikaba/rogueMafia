using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kaydetKontrolBaslangic : MonoBehaviour
{
    public verilerBaslangic data = new verilerBaslangic();
    public bool oyunaBasladi, oyunlastirmaBitti;
    public int hangiSahnede;

    public kaydetKontrol kaydetKontrol;
    public string path;

    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerBaslangic.json");

        if (File.Exists(path))
        {
            jsonBaslangicYukle();
        }
        else
        {
            jsonBaslangicKaydet();
            jsonBaslangicYukle();
        }
    }

    void Update()
    {
        hangiSahnede = SceneManager.GetActiveScene().buildIndex;
    }

    public void jsonBaslangicKaydet()
    {
        data.oyunaBasladi = oyunaBasladi;
        data.oyunlastirmaBitti = oyunlastirmaBitti;
        data.hangiSahnede = hangiSahnede;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> BASLANGIC " + path);
    }

    public void jsonBaslangicYukle()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<verilerBaslangic>(json);
            Debug.Log("YUKLEDI <==> BASLANGIC " + path);

            oyunaBasladi = data.oyunaBasladi;
            oyunlastirmaBitti = data.oyunlastirmaBitti;
            hangiSahnede = data.hangiSahnede;
        }
    }
}
