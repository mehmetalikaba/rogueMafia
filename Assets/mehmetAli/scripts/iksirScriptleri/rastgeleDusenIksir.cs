using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenIksir : MonoBehaviour
{
    public LayerMask Engel;
    public iksirOzellikleri[] tumIksirler;
    public iksirOzellikleri seciliIksir;
    public int hangiIksir;
    public bool oyuncuYakin, iksiriAldi, rastgeleIksirBelirlendi;
    public float yokOlmaSuresi, iksirSuresi, xGucu, yGucu, mesafe;
    public Rigidbody2D rb;
    public GameObject isik;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;
    public AudioSource yadigarlarDolu;
    public GameObject ozellikTexti;
    public iksirKullanmaScripti iksirKullanmaScripti;
    public SpriteRenderer spriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public antikaYadigarKontrol antikaYadigarKontrol;
    canKontrol canKontrol;

    void Start()
    {
        yokOlmaSuresi = 15f;

        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        iksirKullanmaScripti = FindObjectOfType<iksirKullanmaScripti>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
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

        RaycastKontrol();

        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
        {
            if (antikaYadigarKontrol.hangiYadigarAktif[2])
            {
                Debug.Log("patladi");
                Collider2D[] alanHasari = Physics2D.OverlapCircleAll(transform.position, 5, LayerMask.GetMask("Oyuncu"));
                for (int i = 0; i < alanHasari.Length; i++)
                {
                    if (alanHasari[i].name == "Oyuncu")
                    {
                        canKontrol = FindObjectOfType<canKontrol>();
                        canKontrol.canAzalmasi(5, "atesMuhru");
                    }
                }
            }
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
    void RaycastKontrol()
    {
        RaycastHit2D zemin = Physics2D.Raycast(transform.position, Vector2.down, mesafe);

        if (zemin.collider != null)
        {
            if (zemin.collider.gameObject.layer == 0) 
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Debug.Log("yer");
            }
        }
    }
    public void ucmaHareketi()
    {
        rb.constraints = RigidbodyConstraints2D.None;

        int random = Random.Range(1, 3);

        Vector2 launchDirection = random == 1 ? new Vector2(xGucu, yGucu) : new Vector2(-xGucu, yGucu);
        rb.velocity = launchDirection;
    }
}