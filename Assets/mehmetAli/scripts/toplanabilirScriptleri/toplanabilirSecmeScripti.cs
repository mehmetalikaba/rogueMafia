using UnityEngine;
using UnityEngine.Rendering.Universal;

public class toplanabilirSecmeScripti : MonoBehaviour
{
    public GameObject[] toplanabilirler;
    public bool oyuncuYakin;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public Light2D light2d;
    public Transform toplanabilirOlusmaNoktasi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public SpriteRenderer sandikKapali, sandikAcik;

    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        sandikAcik.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && oyuncuYakin && !oyuncuSaldiriTest.yumruk1)
        {
            sandikAcildi();
        }
    }
    public void sandikAcildi()
    {
        sandikKapali.enabled = false;
        sandikAcik.enabled = true;
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        int rastgeleIndex = Random.Range(0, toplanabilirler.Length);
        Instantiate(toplanabilirler[rastgeleIndex], toplanabilirOlusmaNoktasi.transform.position, transform.rotation);
        light2d.enabled = false;
        Destroy(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            oyuncuYakin = true;
            light2d.enabled = true;
        }
        if (collision.CompareTag("shuriken") || (collision.CompareTag("ok")))
        {
            sandikAcildi();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            oyuncuYakin = false;
            light2d.enabled = false;
        }
    }
}
