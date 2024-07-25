using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "kaydedileceklerKontrol", menuName = "Scriptable Objects/kaydedileceklerKontrol")]
public class kaydedilecekler : ScriptableObject
{
    public TextAsset kayitJson;


    public bool oyunaBasladi;
    public int hangiSahnede;
    public float oyuncuCan, aniPuani, ejderParasi, silah1Dayaniklilik, silah2Dayaniklilik;
    public GameObject toplanabilirObje, ozelGuc1Obje, ozelGuc2Obje;
    public silahSecimi silah1Secimi, silah2Secimi;
    public float[] sesSeviyeleri;


    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public void tumVerileriSil()
    {
        oyunaBasladi = false;
        hangiSahnede = 0;
        oyuncuCan = 0f;
        aniPuani = 0f;
        ejderParasi = 0f;
        silah1Dayaniklilik = 0f;
        silah2Dayaniklilik = 0f;
        toplanabilirObje = null;
        ozelGuc1Obje = null;
        ozelGuc2Obje = null;
        silah1Secimi.tumSilahlar = silahSecimi.silahlar.yumruk;
        silah2Secimi.tumSilahlar = silahSecimi.silahlar.yumruk;

        for (int i = 0; i < sesSeviyeleri.Length; i++)
        {
            sesSeviyeleri[i] = 0.1f;
        }

        string emptyJson = "{}";
        LoadFromJson(emptyJson);
    }

    public void jsonKaydet()
    {
        if (kayitJson != null)
        {
            string jsonPath = Path.Combine(Application.dataPath, "kayitlar.json");
            string json = SaveToJson();
            File.WriteAllText(jsonPath, json);
            Debug.Log("JSON kaydedildi: " + jsonPath);
        }
        else
        {
            Debug.LogWarning("kayitJson atanmamýþ!");
        }
    }

    public void jsonYukle()
    {
        if (kayitJson != null)
        {
            string jsonPath = Path.Combine(Application.dataPath, "kayitlar.json");
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                LoadFromJson(json);
                Debug.Log("JSON yüklendi: " + jsonPath);
            }
            else
            {
                Debug.LogWarning("JSON dosyasý bulunamadý: " + jsonPath);
            }
        }
        else
        {
            Debug.LogWarning("kayitJson atanmamýþ!");
        }
    }
}