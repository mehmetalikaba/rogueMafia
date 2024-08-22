using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenYadigar : MonoBehaviour
{
    public LayerMask Engel;
    public antikaYadigarOzellikleri[] tumYadigarlar;
    public antikaYadigarOzellikleri buYadigar;
    public int hangiYadigar;
    public bool oyuncuYakin, yadigariAldi, rastgeleYadigarBelirlendi;
    public float yokOlmaSuresi, xGucu, yGucu, mesafe;
    public Rigidbody2D rb;
    public GameObject isik;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;
    public AudioSource yadigarlarDolu;
    public GameObject ozellikTexti;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public SpriteRenderer spriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    canKontrol canKontrol;

    void Start()
    {
        yokOlmaSuresi = 15f;

        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        ozellikTexti = GameObject.Find("yadigarOzelligi");
        ucmaHareketi();
        yadigarDusurme();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
        {
            ozellikTexti.GetComponent<localizedText>().key = buYadigar.yadigarAciklamaKeyi;
            isik.SetActive(true);
        }
        else
        {
            ozellikTexti.GetComponent<localizedText>().key = "";
            isik.SetActive(false);
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenYadigarAl();

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
            if (ozellikTexti.GetComponent<localizedText>().key == buYadigar.yadigarAciklamaKeyi)
                ozellikTexti.GetComponent<localizedText>().key = "";
        }
    }

    public void yerdenYadigarAl()
    {
        for (int i = 0; i < antikaYadigarKontrol.yadigarSlotBos.Length; i++)
        {
            if (antikaYadigarKontrol.yadigarSlotBos[i] && !yadigariAldi)
            {
                yadigariAldi = true;
                ozellikTexti.GetComponent<Text>().text = "";
                silahKontrol.yerdenAliyor = true;
                antikaYadigarKontrol.yadigarSlotBos[i] = false;
                antikaYadigarKontrol.elindekiYadigarlar[i] = buYadigar;
                antikaYadigarKontrol.yadigarlarImage[i].sprite = buYadigar.yadigarIcon;
                Destroy(gameObject);
                break;
            }
            else
            {
                //yadigarlarDolu.Play();
            }
        }
    }

    public void yadigarDusurme()
    {
        if (!rastgeleYadigarBelirlendi)
        {
            rastgeleYadigarBelirlendi = true;
            hangiYadigar = Random.Range(0, tumYadigarlar.Length);
            buYadigar = tumYadigarlar[hangiYadigar];
            spriteRenderer.sprite = tumYadigarlar[hangiYadigar].yadigarIcon;
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