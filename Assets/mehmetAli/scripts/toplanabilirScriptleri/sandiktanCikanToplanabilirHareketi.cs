using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandiktanCikanToplanabilirHareketi : MonoBehaviour
{
    public float xGucu = 4.5f;
    public float yGucu = 11.25f;
    private Rigidbody2D rb, oyuncuToplanabilirRb;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public bool oyuncuYakin;
    public GameObject isik;
    public sandiktanCikanToplanabilirHareketi oyuncuSandiktanCikmaHareketi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;


    void Start()
    {
        isik = transform.GetChild(0).gameObject;
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        rb = GetComponent<Rigidbody2D>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        ucmaHareketi();
    }
    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
        {
            if (toplanabilirKullanmaScripti.toplanabilirObje != null)
            {
                oyuncuToplanabilirRb = toplanabilirKullanmaScripti.toplanabilirObje.GetComponent<Rigidbody2D>();
                oyuncuSandiktanCikmaHareketi = toplanabilirKullanmaScripti.toplanabilirObje.GetComponent<sandiktanCikanToplanabilirHareketi>();
                toplanabilirKullanmaScripti.toplanabilirObje.transform.position = toplanabilirKullanmaScripti.transform.position;
                oyuncuToplanabilirRb.constraints = RigidbodyConstraints2D.None;
                toplanabilirKullanmaScripti.toplanabilirObje.SetActive(true);
                ucmaHareketi();
                oyuncuSandiktanCikmaHareketi.ucmaHareketi();
            }
            toplanabilirKullanmaScripti.toplanabilirObje = gameObject;
            toplanabilirKullanmaScripti.toplanabilirObjeOzellikleriniGetir();
            gameObject.SetActive(false);
        }
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
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        else if (collision.CompareTag("oyuncu"))
        {
            isik.SetActive(true);
            oyuncuYakin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            isik.SetActive(false);
            oyuncuYakin = false;
        }
    }
}
