using System.Collections;
using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public canKontrol canKontrol;
    public Rigidbody2D rb;
    public Animator animator;
    public bool sagaBakiyor = true;
    public bool havada, yuruyor, cakiliyor, atiliyor, atilmaBekliyor, ipde, hareketHizObjesiAktif;
    public int ziplamaSayisi, ziplamaSayaci;
    public float hareketHizi, ziplamaGucu, atilmaGucu, atilmaSuresi, atilmaBeklemeSuresi, cakilmaSuresi, atilmaYonu, hareketInput;
    public Vector2 movementX, movementY;
    public AnimationClip atilmaClip;
    public silahKontrol silahKontrol;
    public tirmanma tirmanma;

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
        if (hareketHizObjesiAktif)
            hareketHizi = (6 * 1.25f);
        else
            hareketHizi = 6;

        if (!atiliyor && !cakiliyor && !tirmanma.tirmaniyor)
        {
            if (!silahKontrol.silahAldi)
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
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.freezeRotation = true;
                rb.velocity = Vector2.zero;
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
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("spaceTusu")) && ziplamaSayaci > 0 && !atiliyor)
            {
                rb.velocity = Vector2.up * ziplamaGucu;
                oyuncuEfektYoneticisi.ZiplamaToz();
                oyuncuEfektYoneticisi.ZiplamaSesi();
                havada = true;
                ziplamaSayaci--;
            }

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftControlTusu")) && havada)
            {
                cakiliyor = true;
                yuruyor = false;
                rb.velocity = Vector2.down * ziplamaGucu * 1.5f;

                animator.SetBool("cakilma", true);
                oyuncuEfektYoneticisi.ZiplamaSesi();
                oyuncuEfektYoneticisi.ZiplamaToz();
            }

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftShiftTusu")) && !atilmaBekliyor)
            {
                animator.SetTrigger("atilma");
                atiliyor = true;
                atilmaBekliyor = true;
                atilmaBeklemeSuresi = atilmaClip.length;
                atilmaSuresi = atilmaClip.length / 2;
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
                animator.SetBool("kosu", false);
                animator.SetBool("zipla", false);
                animator.SetBool("dusus", false);

                atilmaSuresi -= Time.deltaTime;
                if (atilmaSuresi < 0)
                {
                    atiliyor = false;
                    animator.SetBool("kosu", true);
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
                animator.SetBool("kosu", false);
                animator.SetBool("zipla", false);
                animator.SetBool("dusus", true);
                cakilmaSuresi -= Time.deltaTime;
                if (cakilmaSuresi < 0)
                {
                    cakilmaSuresi = 0.4f;
                    cakiliyor = false;
                    yuruyor = true;
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
        }
    }
}