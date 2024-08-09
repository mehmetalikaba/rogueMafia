using UnityEngine;
using UnityEngine.Rendering.Universal;

public class toplanabilirSecmeScripti : MonoBehaviour
{
    public GameObject fx;
    public GameObject[] toplanabilirler;
    public bool oyuncuYakin;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public Light2D light2d;
    public Transform toplanabilirOlusmaNoktasi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public SpriteRenderer kutu;
    public AudioSource kutuKir;

    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && oyuncuYakin && !oyuncuSaldiriTest.yumruk1)
            sandikAcildi();

        if (oyuncuYakin) light2d.enabled = true;
        else light2d.enabled = false;

    }
    public void sandikAcildi()
    {
        
        Instantiate(fx, transform.position, Quaternion.identity);
        kutuKir = GameObject.Find("kutuKir").GetComponent<AudioSource>();
        kutuKir.Play();
        kutu.enabled = false;
        float random = Random.Range(0, 100);
        if (random > 25)
        {
            toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
            int rastgeleIndex = Random.Range(0, toplanabilirler.Length);
            Instantiate(toplanabilirler[rastgeleIndex], toplanabilirOlusmaNoktasi.transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shuriken") || (collision.CompareTag("ok")))
            sandikAcildi();
    }
}
