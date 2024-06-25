using System.Collections;
using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    oyuncuEfektYoneticisi oyuncuEfektYoneticisi;

    public Rigidbody2D rb;
    public float hareketHizi;
    public float input;

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            input = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(input * hareketHizi, rb.velocity.y);
        }


        if (!sagaBakiyor && input > 0)
        {
            Flip();
        }
        else if (sagaBakiyor && input < 0)
        {
            Flip();
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && oyuncuEfektYoneticisi.zeminde)
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
        if (Input.GetKeyDown(KeyCode.Space) && ziplamaSayaci > 0)
        {
            rb.velocity = Vector2.up * ziplamaGucu;
            oyuncuEfektYoneticisi.ZiplamaToz();
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.zeminde = false;
            ziplamaSayaci--;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !oyuncuEfektYoneticisi.zeminde)
        {
            rb.velocity = Vector2.down * ziplamaGucu * 1.5f;
            animator.SetBool("cakilma", true);
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.ZiplamaToz();

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !atilmaBekle)
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
