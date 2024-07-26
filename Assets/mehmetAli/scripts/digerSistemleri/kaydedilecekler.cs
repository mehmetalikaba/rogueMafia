using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "kaydedileceklerKontrol", menuName = "Scriptable Objects/kaydedileceklerKontrol")]
public class kaydedilecekler : ScriptableObject
{
    public TextAsset kayitJson; // JSON dosyas�n�n referans�
    private const string FilePath = "savedData.json"; // JSON verisinin kaydedilece�i dosya yolu

    public bool kayitKilitli, oyunaBasladi, oyunlastirmaBitti;
    public int hangiSahnede;
    public float oyuncuCan, aniPuani, ejderParasi, silah1Dayaniklilik, silah2Dayaniklilik;
    public GameObject toplanabilirObje, ozelGuc1Obje, ozelGuc2Obje;
    public silahSecimi silah1Secimi, silah2Secimi;
    public bool[] yemekEtkileri;
    public float[] sesSeviyeleri;

    [System.Serializable]
    private class Data
    {
        public bool oyunaBasladi;
        public bool oyunlastirmaBitti;
        public int hangiSahnede;
        public float oyuncuCan;
        public float aniPuani;
        public float ejderParasi;
        public float silah1Dayaniklilik;
        public float silah2Dayaniklilik;
        public string silah1Secimi;
        public string silah2Secimi;
        public bool[] yemekEtkileri;
        public float[] sesSeviyeleri;

    }

    private void OnEnable()
    {
        LoadFromJson(); // Oyunun ba�lang�c�nda JSON'dan veri y�kle
    }

    private void OnValidate()
    {
        if (!kayitKilitli)
            SaveToJson(); // Herhangi bir de�i�iklik yap�ld���nda JSON'a kaydet
    }

    public string SaveToJson()
    {
        var data = new Data
        {
            oyunaBasladi = this.oyunaBasladi,
            oyunlastirmaBitti = this.oyunlastirmaBitti,
            hangiSahnede = this.hangiSahnede,
            oyuncuCan = this.oyuncuCan,
            aniPuani = this.aniPuani,
            ejderParasi = this.ejderParasi,
            silah1Dayaniklilik = this.silah1Dayaniklilik,
            silah2Dayaniklilik = this.silah2Dayaniklilik,
            silah1Secimi = this.silah1Secimi.tumSilahlar.ToString(),
            silah2Secimi = this.silah2Secimi.tumSilahlar.ToString(),
            yemekEtkileri = this.yemekEtkileri,
            sesSeviyeleri = this.sesSeviyeleri
        };

        string jsonData = JsonUtility.ToJson(data);
        SaveJsonToFile(jsonData); // JSON verisini dosyaya kaydet

        return jsonData;
    }

    private void SaveJsonToFile(string jsonData)
    {
        string path = Path.Combine(Application.persistentDataPath, FilePath);
        File.WriteAllText(path, jsonData);
        Debug.Log("JSON data saved to: " + path);
    }

    private void LoadFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, FilePath);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<Data>(json);

            this.oyunaBasladi = data.oyunaBasladi;
            this.oyunlastirmaBitti = data.oyunlastirmaBitti;
            this.hangiSahnede = data.hangiSahnede;
            this.oyuncuCan = data.oyuncuCan;
            this.aniPuani = data.aniPuani;
            this.ejderParasi = data.ejderParasi;
            this.silah1Dayaniklilik = data.silah1Dayaniklilik;
            this.silah2Dayaniklilik = data.silah2Dayaniklilik;
            this.silah1Secimi.silahSec(data.silah1Secimi);
            this.silah2Secimi.silahSec(data.silah2Secimi);
            this.yemekEtkileri = data.yemekEtkileri;
            this.sesSeviyeleri = data.sesSeviyeleri;
        }
        else
        {
            Debug.LogWarning("JSON file not found at: " + path);
        }
    }
}
