using System.IO;
using UnityEngine;

public class kaydetKontrolSes : MonoBehaviour
{
    public verilerSes data = new verilerSes();
    public kaydetKontrol kaydetKontrol;
    public sesKontrol sesKontrol;
    public string path;

    void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerSes.json");

        if (File.Exists(path))
        {
            jsonSesYukle();
        }
        else
        {
            jsonSesKaydet();
            jsonSesYukle();
        }
    }

    private void Start()
    {

    }

    public void jsonSesKaydet()
    {
        for (int i = 0; i < data.sesSeviyeleri.Length; i++)
        {
            data.sesSeviyeleri[i] = sesKontrol.sesSeviyeleri[i];
        }

        path = Path.Combine(Application.persistentDataPath, "verilerSes.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("KAYDETTI <==> SES " + path);
    }

    public void jsonSesYukle()
    {
        path = Path.Combine(Application.persistentDataPath, "verilerSes.json");

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<verilerSes>(json);
        Debug.Log("YUKLEDI <==> SES " + path);

        for (int i = 0; i < data.sesSeviyeleri.Length; i++)
        {
            sesKontrol.sesSeviyeleri[i] = data.sesSeviyeleri[i];
        }
    }
}
