using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenAntika : MonoBehaviour
{
    public RaycastHit2D zemin;
    public LayerMask Engel;
    public float mesafe;

    public antikaYadigarOzellikleri[] tumAntikalar;
    public antikaYadigarOzellikleri buAntika;
    public int hangiAntika;
    public bool yerKontrol, oyuncuYakin, antikayiAldi, rastgeleAntikaBelirlendi;
    public float yokOlmaSuresi, xGucu, yGucu;
    public Rigidbody2D rb;
    public GameObject isik, yuvarlakButonu;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;
    public AudioSource antikalarDolu;
    public GameObject ozellikTexti;
    public SpriteRenderer spriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public antikaYadigarKontrol antikaYadigarKontrol;
    canKontrol canKontrol;

    void Start()
    {
        yokOlmaSuresi = 15f;

        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        ozellikTexti = GameObject.Find("yadigarOzelligi");
        StartCoroutine(ucmaHareketi());
        antikaDusurme();
    }

    void Update()
    {
        if (yerKontrol)
            RaycastKontrol();

        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
        {
            yuvarlakButonu.SetActive(true);
            ozellikTexti.GetComponent<localizedText>().key = buAntika.yadigarAciklamaKeyi;
            isik.SetActive(true);
        }
        else
        {
            yuvarlakButonu.SetActive(false);
            ozellikTexti.GetComponent<localizedText>().key = "";
            isik.SetActive(false);
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenAntikaAl();
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenAntikaAl();

        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
        {
            /*if (antikaYadigarKontrol.hangiYadigarAktif[2])
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
            }*/
            Destroy(gameObject);
            if (ozellikTexti.GetComponent<localizedText>().key == buAntika.antikaAciklamaKeyi)
                ozellikTexti.GetComponent<localizedText>().key = "";
        }
    }

    public void yerdenAntikaAl()
    {
        for (int i = 0; i < antikaYadigarKontrol.antikaSlotBos.Length; i++)
        {
            if (antikaYadigarKontrol.yadigarSlotBos[i] && !antikayiAldi)
            {
                antikayiAldi = true;
                ozellikTexti.GetComponent<Text>().text = "";
                silahKontrol.yerdenAliyor = true;
                antikaYadigarKontrol.antikaSlotBos[i] = false;
                antikaYadigarKontrol.elindekiAntikalar[i] = buAntika;
                antikaYadigarKontrol.antikalarImage[i].sprite = buAntika.antikaIcon;
                Destroy(gameObject);
                break;
            }
            else
            {
                //antikalarDolu.Play();
            }
        }
    }

    public void antikaDusurme()
    {
        if (!rastgeleAntikaBelirlendi)
        {
            rastgeleAntikaBelirlendi = true;
            hangiAntika = Random.Range(0, tumAntikalar.Length);
            buAntika = tumAntikalar[hangiAntika];
            spriteRenderer.sprite = tumAntikalar[hangiAntika].antikaIcon;
        }
    }
    void RaycastKontrol()
    {
        zemin = Physics2D.Raycast(transform.position, Vector2.down, mesafe, LayerMask.GetMask("Engel"));

        if (zemin.collider != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("yer");
        }
    }
    IEnumerator ucmaHareketi()
    {
        rb.constraints = RigidbodyConstraints2D.None;

        int random = Random.Range(1, 3);

        Vector2 launchDirection = random == 1 ? new Vector2(xGucu, yGucu) : new Vector2(-xGucu, yGucu);
        rb.velocity = launchDirection;
        yield return new WaitForSeconds(0.15f);
        yerKontrol = true;
    }
}
