using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenIksir : MonoBehaviour
{
    public iksirOzellikleri[] tumIksirler;
    public iksirOzellikleri seciliIksir;
    public int hangiIksir;
    public bool oyuncuYakin, iksiriAldi, rastgeleIksirBelirlendi;
    public float yokOlmaSuresi, iksirSuresi, xGucu, yGucu;
    public Rigidbody2D rb;
    public GameObject isik;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;
    public AudioSource yadigarlarDolu;
    public GameObject ozellikTexti;
    public iksirKullanmaScripti iksirKullanmaScripti;
    public SpriteRenderer spriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;

    void Start()
    {
        yokOlmaSuresi = 15f;

        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        iksirKullanmaScripti = FindObjectOfType<iksirKullanmaScripti>();
        ozellikTexti = GameObject.Find("yadigarOzelligi");
        ucmaHareketi();
        iksirDusurme();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
        {
            ozellikTexti.GetComponent<localizedText>().key = seciliIksir.iksirAciklamaKeyi;
            isik.SetActive(true);
        }
        else
        {
            ozellikTexti.GetComponent<localizedText>().key = "";
            isik.SetActive(false);
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenIksirAl();

        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
        {
            Destroy(gameObject);
            if (ozellikTexti.GetComponent<localizedText>().key == seciliIksir.iksirAciklamaKeyi)
                ozellikTexti.GetComponent<localizedText>().key = "";
        }
    }
    public void yerdenIksirAl()
    {
        if (!iksiriAldi)
        {
            iksiriAldi = true;
            ozellikTexti.GetComponent<Text>().text = "";
            silahKontrol.yerdenAliyor = true;
            iksirKullanmaScripti.eldekiIksir = seciliIksir;
            Destroy(gameObject);
        }
    }
    public void iksirDusurme()
    {
        if (!rastgeleIksirBelirlendi)
        {
            rastgeleIksirBelirlendi = true;
            hangiIksir = Random.Range(0, tumIksirler.Length);
            seciliIksir = tumIksirler[hangiIksir];
            spriteRenderer.sprite = tumIksirler[hangiIksir].iksirIcon;
        }
    }
    public void ucmaHareketi()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        int random = Random.Range(1, 2);
        if (random == 1)
        {
            Vector2 launchDirection = new Vector2(xGucu, yGucu);
            rb.velocity = launchDirection;
        }
        else
        {
            Vector2 launchDirection = new Vector2(-xGucu, yGucu);
            rb.velocity = launchDirection;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("zemin") || (collision.CompareTag("cimZemin")))
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}