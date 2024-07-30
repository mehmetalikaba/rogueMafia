using System.IO;
using UnityEngine;

public class kaydetKontrolOzelEtkiler : MonoBehaviour
{
    public verilerOzelEtkiler data = new verilerOzelEtkiler();
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public kaydetKontrol kaydetKontrol;
    public string path;

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerOzelEtkiler.json");

        // Eðer dosya yoksa oluþtur ve yükle
        if (!File.Exists(path))
        {
            jsonOzelEtkilerKaydet();
        }
        else
        {
            //jsonOzelEtkilerYukle();
        }
    }

    public void jsonOzelEtkilerKaydet()
    {
        for (int i = 0; i < data.yemekEtkileri.Length; i++)
        {
            data.yemekEtkileri[i] = ozelEtkilerKontrol.yemekEtkileri[i];
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> OZEL ETKILER " + path);
    }

    public void jsonOzelEtkilerYukle()
    {
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerOzelEtkiler>(json);
        Debug.Log("YUKLEDI <==> OZEL ETKILER " + path);

        for (int i = 0; i < data.yemekEtkileri.Length; i++)
        {
            ozelEtkilerKontrol.yemekEtkileri[i] = data.yemekEtkileri[i];
        }
    }
}
