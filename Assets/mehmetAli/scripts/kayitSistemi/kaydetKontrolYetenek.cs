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

        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
    }

    public void jsonYetenekKaydet()
    {
        for (int i = 0; i < data.menzilliSeviyeler.Length; i++)
        {
            data.menzilliSeviyeler[i] = yetenekKontrol.menzilliSeviyeler[i];
        }
        for (int i = 0; i < data.pasifSeviyeler.Length; i++)
        {
            data.pasifSeviyeler[i] = yetenekKontrol.pasifSeviyeler[i];
        }
        for (int i = 0; i < data.yakinSeviyeler.Length; i++)
        {
            data.yakinSeviyeler[i] = yetenekKontrol.yakinSeviyeler[i];
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

        for (int i = 0; i < data.menzilliSeviyeler.Length; i++)
        {
            yetenekKontrol.menzilliSeviyeler[i] = data.menzilliSeviyeler[i];
            yetenekKontrol.menzilliYetenekler[i].yetenekSeviyesi = data.menzilliSeviyeler[i];
        }
        for (int i = 0; i < data.pasifSeviyeler.Length; i++)
        {
            yetenekKontrol.pasifSeviyeler[i] = data.pasifSeviyeler[i];
            yetenekKontrol.pasifYetenekler[i].yetenekSeviyesi = data.pasifSeviyeler[i];
        }
        for (int i = 0; i < data.yakinSeviyeler.Length; i++)
        {
            yetenekKontrol.yakinSeviyeler[i] = data.yakinSeviyeler[i];
            yetenekKontrol.yakinYetenekler[i].yetenekSeviyesi = data.yakinSeviyeler[i];
        }
    }

    public void jsonYetenekSifirla()
    {
        for (int i = 0; i < data.menzilliSeviyeler.Length; i++)
        {
            data.menzilliSeviyeler[i] = 0;
        }
        for (int i = 0; i < data.pasifSeviyeler.Length; i++)
        {
            data.pasifSeviyeler[i] = 0;
        }
        for (int i = 0; i < data.yakinSeviyeler.Length; i++)
        {
            data.yakinSeviyeler[i] = 0;
        }

        path = Path.Combine(Application.persistentDataPath, "verilerYetenek.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> YETENEK " + path);
    }
}