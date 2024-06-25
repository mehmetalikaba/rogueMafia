using System.Collections;
using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    oyuncuEfektYoneticisi oyuncuEfektYoneticisi;

    public Rigidbody2D rb;
    public float hareketHizi;

    public float atilmaHizi;
    public float atilmaMesafesi;
    bool atildi;

    public bool sagaBakiyor = true;

    public bool egilme, atilma, atilmaBekle;
    bool ipde;
    int ziplamaSayaci;
    public int ziplamaSayisi;
    public float ziplamaGucu, atilmaGucu, atilmaSuresi, kalanAtilmaSuresi, atilmaYonu, ilkAtilmaSuresi, ilkKalanAtilmaSuresi;

    public Animator animator;

    Vector2 movement;

    void Start()
    {
        oyuncuEfektYoneticisi = GetComponent<oyuncuEfektYoneticisi>();
        rb = GetComponent<Rigidbody2D>();
        ziplamaSayaci = ziplamaSayisi;

        kalanAtilmaSuresi = ilkKalanAtilmaSuresi;
        atilmaSuresi = ilkAtilmaSuresi;
    }

    private void FixedUpdate()
    {
        if (egilme)
        {
            hareketHizi = 3;
        }
        else
        {
            hareketHizi = 6;
        }

        if (!atilma)
        {
            float input = 0f;
            if (Input.GetKey(tusDizilimiGetirTest.instance.tusIsleviGetir("solaGit")))
            {
                input -= 1f;
                if (sagaBakiyor)
                    Flip();
            }
            if (Input.GetKey(tusDizilimiGetirTest.instance.tusIsleviGetir("sagaGit")))
            {
                input += 1f;
                if (!sagaBakiyor)
                    Flip();
            }

            rb.velocity = new Vector2(input * hareketHizi, rb.velocity.y);
        }
    }

    private void Update()
    {
        if (Input.GetKey(tusDizilimiGetirTest.instance.tusIsleviGetir("egilme")) && oyuncuEfektYoneticisi.zeminde)
        {
            egilme = true;
        }
        else
        {
            if (!ipde)
            {
                egilme = false;
            }
        }

        if (Input.GetKeyDown(tusDizilimiGetirTest.instance.tusIsleviGetir("ucmak")))
        {

        }

        if (Input.GetKeyDown(tusDizilimiGetirTest.instance.tusIsleviGetir("zipla")) && ziplamaSayaci > 0)
        {
            rb.velocity = Vector2.up * ziplamaGucu;
            oyuncuEfektYoneticisi.ZiplamaToz();
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.zeminde = false;
            ziplamaSayaci--;
        }

        if (Input.GetKeyDown(tusDizilimiGetirTest.instance.tusIsleviGetir("cakilma")) && !oyuncuEfektYoneticisi.zeminde)
        {
            rb.velocity = Vector2.down * ziplamaGucu * 1.5f;
            animator.SetBool("cakilma", true);
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.ZiplamaToz();
        }

        if (Input.GetKeyDown(tusDizilimiGetirTest.instance.tusIsleviGetir("atilma")) && !atilmaBekle)
        {
            atilma = true;
            atilmaBekle = true;
        }

        if (atilmaBekle)
        {
            kalanAtilmaSuresi -= Time.deltaTime;

            if (kalanAtilmaSuresi < 0)
            {
                kalanAtilmaSuresi = ilkKalanAtilmaSuresi;
                atilmaBekle = false;
            }
        }

        if (atilma)
        {
            atilmaSuresi -= Time.deltaTime;

            if (atilmaSuresi < 0)
            {
                atilma = false;
                atilmaSuresi = ilkAtilmaSuresi;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                atilmaYonu = transform.localScale.x > 0 ? 1 : -1;
                rb.velocity = new Vector2(atilmaYonu * atilmaGucu, rb.velocity.y);
            }
        }
    }

    void Flip()
    {
        sagaBakiyor = !sagaBakiyor;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            ziplamaSayaci = ziplamaSayisi;

            animator.SetBool("cakilma", false);
            animator.SetBool("dusus", false);

            oyuncuEfektYoneticisi.zeminde = true;
            oyuncuEfektYoneticisi.DusmeToz();
            oyuncuEfektYoneticisi.DusmeSesi();
        }
        if (collision.gameObject.CompareTag("ip"))
        {
            animator.SetBool("cakilma", false);

            ipde = true;
            egilme = true;
            ziplamaSayaci = ziplamaSayisi;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            oyuncuEfektYoneticisi.zeminde = false;
        }
        if (collision.gameObject.CompareTag("ip"))
        {
            oyuncuEfektYoneticisi.zeminde = false;

            ipde = false;
            egilme = false;
        }
    }
}