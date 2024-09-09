using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenYadigar : MonoBehaviour
{
    public RaycastHit2D zemin;
    public LayerMask Engel;
    public float mesafe;

    public antikaYadigarOzellikleri[] tumYadigarlar;
    public antikaYadigarOzellikleri buYadigar;
    public int hangiYadigar;
    public bool yerKontrol, oyuncuYakin, yadigariAldi, rastgeleYadigarBelirlendi;
    public float yokOlmaSuresi, xGucu, yGucu;
    public Rigidbody2D rb;
    public GameObject isik;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;
    public AudioSource yadigarlarDolu;
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
        yadigarDusurme();
    }

    void Update()
    {
        if (yerKontrol)
            RaycastKontrol();

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
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenYadigarAl();

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