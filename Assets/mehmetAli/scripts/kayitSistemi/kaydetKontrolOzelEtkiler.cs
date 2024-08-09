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

        if (!File.Exists(path))
            jsonOzelEtkilerKaydet();

        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
    }

    public void jsonOzelEtkilerKaydet()
    {
        for (int i = 0; i < data.yemekEtkileri.Length; i++)
        {
            data.yemekEtkileri[i] = ozelEtkilerKontrol.yemekEtkileri[i];
        }

        path = Path.Combine(Application.persistentDataPath, "verilerOzelEtkiler.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> OZEL ETKILER " + path);
    }

    public void jsonOzelEtkilerYukle()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerOzelEtkiler.json");
        Debug.Log(path);
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerOzelEtkiler>(json);
        Debug.Log("YUKLEDI <==> OZEL ETKILER " + path);

        for (int i = 0; i < data.yemekEtkileri.Length; i++)
        {
            ozelEtkilerKontrol.yemekEtkileri[i] = data.yemekEtkileri[i];
        }
    }
}
