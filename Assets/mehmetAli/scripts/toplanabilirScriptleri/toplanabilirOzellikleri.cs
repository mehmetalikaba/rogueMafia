using System.Collections;
using UnityEngine;

public class toplanabilirOzellikleri : MonoBehaviour
{
    public Sprite toplanabilirIcon;
    public string toplanabilirKeyi, toplanabilirAdi, toplanabilirAciklamaKeyi;

    public float iksirSuresi, xGucu = 4.5f, yGucu = 11.25f;
    public bool oyuncuYakin;
    public GameObject isik;
    public GameObject oyuncuIksir;
    public Rigidbody2D rb;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;

    silahKontrol silahKontrol;
    oyuncuHareket oyuncuHareket;

    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        ucmaHareketi();
        StartCoroutine(yokOlma());
    }
    public IEnumerator yokOlma()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin) isik.SetActive(true);
        else isik.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor)
            toplanabilirObjeOzellikleriniGetir();
    }

    public void toplanabilirObjeOzellikleriniGetir()
    {
        Debug.Log("IKSIR <==> aldi");
        silahKontrol.yerdenAliyor = true;
        if (toplanabilirKullanmaScripti.toplanabilirObje != null)
            oyuncuIksir = Instantiate(toplanabilirKullanmaScripti.toplanabilirObje, toplanabilirKullanmaScripti.transform.position, toplanabilirKullanmaScripti.transform.rotation);

        toplanabilirKullanmaScripti.toplanabilirImage.sprite = toplanabilirIcon;
        toplanabilirKullanmaScripti.toplanabilirObje = gameObject;
        toplanabilirKullanmaScripti.toplanabilirIcon = toplanabilirIcon;
        toplanabilirKullanmaScripti.toplanabilirKeyi = toplanabilirKeyi;
        toplanabilirKullanmaScripti.toplanabilirAdi = toplanabilirAdi;
        toplanabilirKullanmaScripti.toplanabilirAciklamaKeyi = toplanabilirAciklamaKeyi;
        if (toplanabilirKeyi == "can_iksiri")
            toplanabilirKullanmaScripti.toplanabilirObje = toplanabilirKullanmaScripti.butunToplanabilirler[0];
        if (toplanabilirKeyi == "dayaniklilik_iksiri")
            toplanabilirKullanmaScripti.toplanabilirObje = toplanabilirKullanmaScripti.butunToplanabilirler[1];
        if (toplanabilirKeyi == "hareket_hizi_iksiri")
            toplanabilirKullanmaScripti.toplanabilirObje = toplanabilirKullanmaScripti.butunToplanabilirler[2];
        if (toplanabilirKeyi == "hasar_iksiri")
            toplanabilirKullanmaScripti.toplanabilirObje = toplanabilirKullanmaScripti.butunToplanabilirler[3];
        Destroy(gameObject);
    }

    public void ucmaHareketi()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        Vector2 launchDirection = new Vector2(Random.Range(-xGucu, xGucu), yGucu);
        rb.velocity = launchDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("zemin") || (collision.CompareTag("cimZemin")))
        {
            Debug.Log("yere carpti");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}