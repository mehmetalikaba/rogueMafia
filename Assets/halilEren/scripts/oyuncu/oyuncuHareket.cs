﻿using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    float dusmeTimer;

    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public PlatformEffector2D bulunduguZemin;
    public canKontrol canKontrol;
    public Rigidbody2D rb;
    public Animator animator;
    public bool sagaBakiyor = true;
    public bool inmeKilitli, hareketKilitli, ziplamaKilitli, zeminde, havada, yuruyor, cakiliyor, cakildi, atiliyor, atilmaBekliyor, ipde, hareketHizObjesiAktif,dusuyor, atilmaKilitli;
    public int ziplamaSayisi, ziplamaSayaci;
    public float hareketHizi, ziplamaGucu, atilmaGucu, atilmaSuresi, atilmaBeklemeSuresi, cakilmaSuresi, atilmaYonu, hareketInput, zeminDegisimSuresi;
    public Vector2 movementX, movementY;
    public AnimationClip atilmaClip;
    public silahKontrol silahKontrol;
    public tirmanma tirmanma;
    public GameObject bulunduguZeminObject;
    public AudioSource cakilmaSes;

    //--------------------------------------------------------------------------------------------------------
    private float previousPositionX;
    private float positionUnchangedTime;
    public float thresholdTime = 0.1f;

    void Start()
    {
        tirmanma = FindObjectOfType<tirmanma>();
        canKontrol = FindObjectOfType<canKontrol>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuEfektYoneticisi = GetComponent<oyuncuEfektYoneticisi>();
        rb = GetComponent<Rigidbody2D>();
        ziplamaSayaci = ziplamaSayisi;

        //--------------------------------------------------------------------------------------------------------
        previousPositionX = transform.position.x;
        positionUnchangedTime = 0f;
    }

    private void FixedUpdate()
    {
        /*if(dusuyor)
        {
            dusmeTimer += Time.deltaTime;
            if (dusmeTimer >= 5f)
            {
                zeminde = false;
                havada = true;
                dusmeTimer = 0;
                dusuyor = false;
            }
        }*/

        if (hareketHizObjesiAktif)
            hareketHizi = (6 * 1.25f);
        else
            hareketHizi = 6;

        if (!atiliyor && !cakiliyor && !tirmanma.tirmaniyor)
        {
            if (hareketKilitli || silahKontrol.silahAldi)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.freezeRotation = true;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.freezeRotation = true;

                float input = 0f;
                hareketInput = input;
                if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("aTusu")))
                {
                    hareketInput -= 1f;
                    if (sagaBakiyor)
                        Flip();
                }
                if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("dTusu")))
                {
                    hareketInput += 1f;
                    if (!sagaBakiyor)
                        Flip();
                }

                if (hareketInput != 0)
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

                rb.velocity = new Vector2(hareketInput * hareketHizi, rb.velocity.y);
            }

            //--------------------------------------------------------------------------------------------------------

            float currentPositionX = transform.position.x;
            if (Mathf.Approximately(currentPositionX, previousPositionX))
                positionUnchangedTime += Time.deltaTime;
            else
                positionUnchangedTime = 0f;
            if (positionUnchangedTime >= thresholdTime)
            {
                animator.SetBool("kosu", false);
            }
            previousPositionX = currentPositionX;
        }
    }

    private void Update()
    {
        if (!silahKontrol.silahAldi)
        {
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftControlTusu")) && havada && !cakiliyor)
            {
                cakiliyor = true;
                rb.velocity = Vector2.down * ziplamaGucu * 1.5f;
                oyuncuEfektYoneticisi.ZiplamaSesi();
                oyuncuEfektYoneticisi.ZiplamaToz();
            }

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftShiftTusu")) && !atilmaBekliyor && !tirmanma.tirmaniyor && !cakiliyor && !atilmaKilitli)
            {
                animator.SetTrigger("atilma");
                atiliyor = true;
                atilmaBekliyor = true;
                atilmaBeklemeSuresi = atilmaClip.length;
                atilmaSuresi = atilmaClip.length / 2;
            }
            if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("sTusu")) && Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("spaceTusu")) && !inmeKilitli)
            {
                havada = true;
                zeminDegisimSuresi = 0.5f;
                bulunduguZemin = bulunduguZeminObject.GetComponent<PlatformEffector2D>();
                int playerLayer = LayerMask.NameToLayer("Oyuncu");
                bulunduguZemin.colliderMask &= ~(1 << playerLayer);
            }
            else if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("spaceTusu")) && ziplamaSayaci > 0 && !atiliyor && !ziplamaKilitli && !cakiliyor)
            {
                zeminde = false;
                havada = true;
                if (tirmanma.tirmaniyor)
                {
                    tirmanma.oyuncuYakin = false;
                    tirmanma.dikeyHareket = 0f;
                    tirmanma.tirmaniyor = false;
                    tirmanma.StartCoroutine(tirmanma.tirmaniyorAnimasyon());
                }
                rb.velocity = Vector2.up * ziplamaGucu;
                oyuncuEfektYoneticisi.ZiplamaToz();
                oyuncuEfektYoneticisi.ZiplamaSesi();
                ziplamaSayaci--;
            }

            if (zeminDegisimSuresi <= 0)
            {
                if (bulunduguZemin != null)
                {
                    int playerLayer = LayerMask.NameToLayer("Oyuncu");
                    bulunduguZemin.colliderMask |= (1 << playerLayer);
                    bulunduguZemin.rotationalOffset = 0f;
                }
            }
            else if (bulunduguZemin != null)
            {
                zeminDegisimSuresi -= Time.deltaTime;
            }

            if (atilmaBekliyor)
            {
                atilmaBeklemeSuresi -= Time.deltaTime;
                if (atilmaBeklemeSuresi < 0)
                {
                    atilmaBeklemeSuresi = atilmaClip.length;
                    atilmaBekliyor = false;
                }
            }

            if (atiliyor)
            {
                yuruyor = false;
                atilmaSuresi -= Time.deltaTime;
                if (atilmaSuresi < 0)
                {
                    atiliyor = false;
                    atilmaSuresi = atilmaClip.length / 2;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    atilmaYonu = transform.localScale.x > 0 ? 1 : -1;
                    rb.velocity = new Vector2(atilmaYonu * atilmaGucu, rb.velocity.y);
                }
            }

            if (cakiliyor)
            {
                yuruyor = false;
                if (cakildi)
                {
                    cakilmaSuresi -= Time.deltaTime;
                    if (cakilmaSuresi < 0)
                    {
                        cakilmaSuresi = 0.75f;
                        cakildi = false;
                        cakiliyor = false;
                        animator.SetBool("egilme", false);
                    }
                }
            }
        }
    }

    void Flip()
    {
        if (!atiliyor)
        {
            sagaBakiyor = !sagaBakiyor;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            bulunduguZeminObject = collision.gameObject;

            zeminde = true;
            if (cakiliyor)
            {
                cakilmaSes.Play();
                silahKontrol.oyuncuSaldiriTest.alanHasariVer();
                canKontrol.kameraSarsinti.Shake();
                cakildi = true;
            }

            ziplamaSayaci = ziplamaSayisi;

            animator.SetBool("cakilma", false);
            animator.SetBool("dusus", false);

            havada = false;

            oyuncuEfektYoneticisi.tasda = true;
            oyuncuEfektYoneticisi.cimde = false;
            oyuncuEfektYoneticisi.DusmeToz();
            oyuncuEfektYoneticisi.DusmeSesi();
            //dusuyor = false;
        }
        if (collision.gameObject.CompareTag("cimZemin"))
        {
            zeminde = true;
            if (cakiliyor)
            {
                cakilmaSes.Play();
                silahKontrol.oyuncuSaldiriTest.alanHasariVer();
                canKontrol.kameraSarsinti.Shake();
                cakildi = true;
            }
            ziplamaSayaci = ziplamaSayisi;

            animator.SetBool("cakilma", false);
            animator.SetBool("dusus", false);

            havada = false;

            oyuncuEfektYoneticisi.cimde = true;
            oyuncuEfektYoneticisi.tasda = false;
            oyuncuEfektYoneticisi.DusmeToz();
            oyuncuEfektYoneticisi.DusmeSesi();
            //dusuyor = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            if (collision.gameObject == bulunduguZemin)
            {
                bulunduguZemin = null;
            }
            //dusuyor = true;
        }
        if (collision.gameObject.CompareTag("cimZemin"))
        {
            //dusuyor = true;
        }
    }
}