using System.IO;
using UnityEngine;

public class kaydetKontrolYetenek : MonoBehaviour
{
    public verilerYetenek data = new verilerYetenek();
    public kaydetKontrol kaydetKontrol;
    public yetenekKontrol yetenekKontrol;
    public string path;

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerYetenek.json");
    }

    public void jsonYetenekKaydet()
    {
        for (int i = 0; i < data.yetenekSeviyeleri.Length; i++)
        {
            data.yetenekSeviyeleri[i] = yetenekKontrol.yetenekSeviyeleri[i];
        }
        for (int i = 0; i < data.yetenekUygulandi.Length; i++)
        {
            data.yetenekUygulandi[i] = yetenekKontrol.yetenekUygulandi[i];
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> YETENEK " + path);
    }

    public void jsonYetenekYukle()
    {
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerYetenek>(json);
        Debug.Log("YUKLEDI <==> YETENEK " + path);

        for (int i = 0; i < data.yetenekSeviyeleri.Length; i++)
        {
            yetenekKontrol.yetenekSeviyeleri[i] = data.yetenekSeviyeleri[i];
        }
        for (int i = 0; i < data.yetenekUygulandi.Length; i++)
        {
            yetenekKontrol.yetenekUygulandi[i] = data.yetenekUygulandi[i];
        }
    }
}