using System.IO;
using UnityEngine;

public class kaydetKontrolYetenek : MonoBehaviour
{
    public verilerYetenek data = new verilerYetenek();
    public yetenekKontrol yetenekKontrol;
    public kaydetKontrol kaydetKontrol;
    public string path;

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerYetenek.json");

        if (!File.Exists(path))
            jsonYetenekKaydet();
    }

    public void jsonYetenekKaydet()
    {
        for (int i = 0; i < data.yetenekSeviyeleri.Length; i++)
        {
            //data.yetenekSeviyeleri[i] = yetenekKontrol.yetenekSeviyeleri[i];
        }

        path = Path.Combine(Application.persistentDataPath, "verilerYetenek.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> YETENEK " + path);
    }

    public void jsonYetenekYukle()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerYetenek.json");

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerYetenek>(json);
        Debug.Log("YUKLEDI <==> YETENEK " + path);

        for (int i = 0; i < data.yetenekSeviyeleri.Length; i++)
        {
           // yetenekKontrol.yetenekSeviyeleri[i] = data.yetenekSeviyeleri[i];
        }
    }
}