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

    private static toplanabilirOzellikleri aktifToplanabilir;

    silahKontrol silahKontrol;

    void Start()
    {
        silahKontrol = FindObjectOfType<silahKontrol>();
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        ucmaHareketi();
    }

    void Update()
    {
        if (aktifToplanabilir == this && Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
            toplanabilirObjeOzellikleriniGetir();
    }

    public void toplanabilirObjeOzellikleriniGetir()
    {
        silahKontrol.silahAldi = true;
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
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (collision.CompareTag("oyuncu"))
        {
            oyuncuYakin = true;
            aktifToplanabilir = this;
            if (aktifToplanabilir == this)
            {
                isik.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            oyuncuYakin = false;
            isik.SetActive(false);

            if (aktifToplanabilir == this)
            {
                aktifToplanabilir = null;
            }
        }
    }

}