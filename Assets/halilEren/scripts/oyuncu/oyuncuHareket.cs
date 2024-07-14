using System.Collections;
using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;

    public canKontrol canKontrol;

    public Rigidbody2D rb;

    public Animator animator;

    public bool sagaBakiyor = true;
    public bool havada, yuruyor, egilme, atilma, atilmaBekle, ipde, hareketHizObjesiAktif;

    public int ziplamaSayisi, ziplamaSayaci;
    public float hareketHizi, ziplamaGucu, atilmaGucu, atilmaSuresi, kalanAtilmaSuresi, atilmaYonu, ilkAtilmaSuresi, ilkKalanAtilmaSuresi, atilmaStaminaAzalmasi;

    public Vector2 movementX, movementY;

    //--------------------------------------------------------------------------------------------------------
    private float previousPositionX;
    private float positionUnchangedTime;
    public float thresholdTime = 0.1f;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();

        oyuncuEfektYoneticisi = GetComponent<oyuncuEfektYoneticisi>();
        rb = GetComponent<Rigidbody2D>();
        ziplamaSayaci = ziplamaSayisi;

        kalanAtilmaSuresi = ilkKalanAtilmaSuresi;
        atilmaSuresi = ilkAtilmaSuresi;


        //--------------------------------------------------------------------------------------------------------
        previousPositionX = transform.position.x;
        positionUnchangedTime = 0f;
    }

    private void FixedUpdate()
    {
        if (egilme)
        {
            if (hareketHizObjesiAktif)
                hareketHizi = (3 * 2);
            else
                hareketHizi = 3;
        }
        else
        {
            if (hareketHizObjesiAktif)
                hareketHizi = (6 * 2);
            else
                hareketHizi = 6;
        }

        if (!atilma)
        {
            float input = 0f;
            if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("aTusu")))
            {
                input -= 1f;
                if (sagaBakiyor)
                    Flip();
            }
            if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("dTusu")))
            {
                input += 1f;
                if (!sagaBakiyor)
                    Flip();
            }

            if (input != 0)
                yuruyor = true;
            else
                yuruyor = false;

            if (!yuruyor)
                movementX.x = 0;
            else
                movementX.x = rb.velocity.x;

            if (havada)
                movementY.y = rb.velocity.y;
            else
                movementY.y = 0;

            rb.velocity = new Vector2(input * hareketHizi, rb.velocity.y);


            //--------------------------------------------------------------------------------------------------------
            float currentPositionX = transform.position.x;

            if (Mathf.Approximately(currentPositionX, previousPositionX))
                positionUnchangedTime += Time.deltaTime;
            else
                positionUnchangedTime = 0f;

            if (positionUnchangedTime >= thresholdTime)
            {
                yuruyor = false;
            }
            previousPositionX = currentPositionX;
        }
    }

    private void Update()
    {
        if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("egilme")) && !havada)
            egilme = true;
        else
        {
            if (!ipde)
                egilme = false;
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("ucmak")))
        {

        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("zipla")) && ziplamaSayaci > 0)
        {
            rb.velocity = Vector2.up * ziplamaGucu;
            oyuncuEfektYoneticisi.ZiplamaToz();
            oyuncuEfektYoneticisi.ZiplamaSesi();
            havada = true;
            ziplamaSayaci--;
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("cakilma")) && havada)
        {
            rb.velocity = Vector2.down * ziplamaGucu * 1.5f;
            animator.SetBool("cakilma", true);
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.ZiplamaToz();
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("atilma")) && !atilmaBekle)
        {
            if (canKontrol.stamina > 35)
            {
                canKontrol.staminaAzalmasi(atilmaStaminaAzalmasi);
                atilma = true;
                atilmaBekle = true;
            }
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

            havada = false;
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
            havada = true;
        }
        if (collision.gameObject.CompareTag("ip"))
        {
            havada = true;
            ipde = false;
            egilme = false;
        }
    }
}