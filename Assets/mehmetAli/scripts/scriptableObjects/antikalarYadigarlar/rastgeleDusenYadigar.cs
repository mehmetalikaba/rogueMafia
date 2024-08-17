using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rastgeleDusenYadigar : MonoBehaviour
{
    public bool oyuncuYakin, yadigariAldi;
    public float yokOlmaSuresi, iksirSuresi, xGucu = 4.5f, yGucu = 11.25f;
    public Rigidbody2D rb;
    public GameObject isik;
    public antikaYadigarOzellikleri buYadigarObjesi;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;
    public AudioSource yadigarlarDolu;
    public GameObject ozellikTexti;

    void Start()
    {
        yokOlmaSuresi = 15f;

        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        rb = GetComponent<Rigidbody2D>();
        ozellikTexti = GameObject.Find("yadigarOzelligi");
        ozellikTexti.GetComponent<Text>().text = "";


        ucmaHareketi();

        StartCoroutine(yokOlma());
    }
    public IEnumerator yokOlma()
    {
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
        {
            ozellikTexti.GetComponent<localizedText>().key = buYadigarObjesi.yadigarAciklamaKeyi;
            isik.SetActive(true);
        }
        else
        {
            ozellikTexti.GetComponent<localizedText>().key = "";
            isik.SetActive(false);
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenYadigarAl();

        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
        {
            Destroy(gameObject);
            if (ozellikTexti.GetComponent<localizedText>().key == buYadigarObjesi.yadigarAciklamaKeyi)
                ozellikTexti.GetComponent<localizedText>().key = "";
        }
    }

    public void yerdenYadigarAl()
    {
        for (int i = 0; i < antikaYadigarKontrol.yadigarSlotBos.Length; i++)
        {
            if (antikaYadigarKontrol.yadigarSlotBos[i] && !yadigariAldi)
            {
                ozellikTexti.GetComponent<Text>().text = "";
                silahKontrol.yerdenAliyor = true;
                yadigariAldi = true;
                antikaYadigarKontrol.yadigarSlotBos[i] = false;
                antikaYadigarKontrol.elindekiYadigarlar[i] = buYadigarObjesi;
                antikaYadigarKontrol.yadigarlarImage[i].sprite = buYadigarObjesi.yadigarIcon;
                Destroy(gameObject);
                break;
            }
            else
            {
                //yadigarlarDolu.Play();
            }
        }
    }

    public void ucmaHareketi()
    {
        Debug.Log("carpti");
        rb.constraints = RigidbodyConstraints2D.None;
        Vector2 launchDirection = new Vector2(Random.Range(-xGucu, xGucu), yGucu);
        rb.velocity = launchDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("zemin") || (collision.CompareTag("cimZemin")))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
