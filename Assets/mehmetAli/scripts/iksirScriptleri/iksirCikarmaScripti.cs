using UnityEngine;
using UnityEngine.Rendering.Universal;

public class iksirCikarmaScripti : MonoBehaviour
{
    public GameObject dusecekOlanIksir, fx;
    public Light2D light2d;
    public Transform iksirOlusmaNoktasi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public SpriteRenderer kutu;
    public AudioSource kutuKir;
    public bool oyuncuYakin, sandikAcildi, iksirDustu;


    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && oyuncuYakin && !oyuncuSaldiriTest.yumruk1)
            sandikAcma();

        if (oyuncuYakin) light2d.enabled = true;
        else light2d.enabled = false;

    }
    public void sandikAcma()
    {
        if (!sandikAcildi)
        {
            sandikAcildi = true;
            Instantiate(fx, transform.position, Quaternion.identity);
            kutuKir = GameObject.Find("kutuKir").GetComponent<AudioSource>();
            kutuKir.Play();
            kutu.enabled = false;
            float random = Random.Range(0, 100);
            if (random > 1 && !iksirDustu)
            {
                iksirDustu = true;
                Instantiate(dusecekOlanIksir, new Vector3(iksirOlusmaNoktasi.transform.position.x, iksirOlusmaNoktasi.transform.position.y, transform.position.z), transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shuriken") || (collision.CompareTag("ok")))
            sandikAcma();
    }
}
