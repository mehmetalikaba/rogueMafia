using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;

public class oyuncuHareket : MonoBehaviour
{
    Gamepad gamepad;

    public GameObject tenguKanatlari, dashEfektSola, dashEfektSaga;

    float dusmeTimer;

    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public PlatformEffector2D bulunduguZemin;
    public canKontrol canKontrol;
    public Rigidbody2D rb;
    public Animator animator;
    public bool sagaBakiyor = true;
    public bool inmeKilitli, hareketKilitli, ziplamaKilitli, zeminde, havada, yuruyor, cakiliyor, cakildi, atiliyor, atilmaBekliyor, ipde, hareketHizObjesiAktif, yakitoriYedi, dusuyor, atilmaKilitli, yavaslat;
    public int ziplamaSayisi, ziplamaSayaci;
    public float ziplamaGucu, ziplamaGucuBonus, atilmaGucu, atilmaSuresi, atilmaBeklemeSuresi, cakilmaSuresi, atilmaYonu, hareketInput, zeminDegisimSuresi;
    public float hareketHizi = 6f, hareketHiziBonus = 1.0f, hareketHiziYavaslama = 0.5f, sonHareketHizi;
    public Vector2 movementX, movementY;
    public AnimationClip atilmaClip;
    public silahKontrol silahKontrol;
    public tirmanma tirmanma;
    public GameObject bulunduguZeminObject;
    public AudioSource cakilmaSes;
    public oyuncuSaldiriTest oyuncuSaldiriTest;

    //--------------------------------------------------------------------------------------------------------
    private float previousPositionX;
    private float positionUnchangedTime;
    public float thresholdTime = 0.1f;

    void Start()
    {
        //gamepad = Gamepad.current;

        tirmanma = FindObjectOfType<tirmanma>();
        canKontrol = FindObjectOfType<canKontrol>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuEfektYoneticisi = GetComponent<oyuncuEfektYoneticisi>();
        rb = GetComponent<Rigidbody2D>();
        ziplamaSayaci = ziplamaSayisi;
        hareketHiziBonus = 1.0f;
        hareketHiziYavaslama = 0.75f;

        //--------------------------------------------------------------------------------------------------------
        previousPositionX = transform.position.x;
        positionUnchangedTime = 0f;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);

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

        sonHareketHizi = hareketHizi;
        sonHareketHizi = (hareketHizi * hareketHiziBonus);

        if (hareketHizObjesiAktif)
            sonHareketHizi = ((hareketHizi * 1.25f));

        if (canKontrol.etmenler[1] || yavaslat)
            sonHareketHizi = hareketHizi * hareketHiziYavaslama;

        if (!atiliyor && !cakiliyor && !tirmanma.tirmaniyor)
        {
            if (hareketKilitli || silahKontrol.yerdenAliyor)
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

                /*if (Gamepad.current.dpad.left.isPressed)
                {
                    SolaHareket();
                }
                if (Gamepad.current.dpad.right.isPressed)
                {
                    SagaHareket();
                }*/
                if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("aTusu")))
                {
                    SolaHareket();
                }
                if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("dTusu")))
                {
                    SagaHareket();
                }

                float controllerInput = 0f;
                controllerInput = Input.GetAxisRaw("Horizontal");
                if (controllerInput > 0)
                {
                    SagaHareket();
                }
                if (controllerInput < 0)
                {
                    SolaHareket();
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

                rb.velocity = new Vector2(hareketInput * sonHareketHizi, rb.velocity.y);
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
        if (!silahKontrol.yerdenAliyor)
        {
            /*if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftControlTusu")) && havada && !cakiliyor)
            {
                cakiliyor = true;
                rb.velocity = Vector2.down * ziplamaGucu * 1.5f;
                oyuncuEfektYoneticisi.ZiplamaSesi();
                oyuncuEfektYoneticisi.ZiplamaToz();
            }*/

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("leftShiftTusu")))
            {
                Atilma();
            }
            float analogY = Input.GetAxis("Vertical");
            if ((Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("sTusu")) && Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("spaceTusu"))||(analogY<0 && Input.GetKeyDown(KeyCode.JoystickButton1) && !inmeKilitli)))
            {
                havada = true;
                zeminDegisimSuresi = 0.5f;
                bulunduguZemin = bulunduguZeminObject.GetComponent<PlatformEffector2D>();
                int playerLayer = LayerMask.NameToLayer("Oyuncu");
                bulunduguZemin.colliderMask &= ~(1 << playerLayer);
            }
            else if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("spaceTusu")))
            {
                Ziplama();
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

    public void SagaHareket()
    {
        hareketInput += 1f;
        if (!sagaBakiyor)
            Flip();
    }
    public void SolaHareket()
    {
        hareketInput -= 1f;
        if (sagaBakiyor)
            Flip();
    }
    public void Ziplama()
    {
        if (!atiliyor && !ziplamaKilitli && !cakiliyor && !silahKontrol.yerdenAliyor  && !oyuncuSaldiriTest.saldiriBasladi)
        {
            if (ziplamaSayisi == 2 && ziplamaSayaci == 1)
            {
                Instantiate(tenguKanatlari, transform.position, transform.rotation);
            }
            if (ziplamaSayaci > 0)
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
                rb.velocity = Vector2.up * (ziplamaGucu * ziplamaGucuBonus);
                oyuncuEfektYoneticisi.ZiplamaToz();
                oyuncuEfektYoneticisi.ZiplamaSesi();
                ziplamaSayaci--;
            }
            else if (tirmanma.tirmaniyor)
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
                rb.velocity = Vector2.up * (ziplamaGucu * ziplamaGucuBonus);
                oyuncuEfektYoneticisi.ZiplamaToz();
                oyuncuEfektYoneticisi.ZiplamaSesi();
                ziplamaSayaci--;
            }
        }

    }
    public void Atilma()
    {
        if (!atilmaBekliyor && !tirmanma.tirmaniyor && !cakiliyor && !atilmaKilitli && !silahKontrol.yerdenAliyor && !oyuncuSaldiriTest.saldiriBasladi)
        {
            animator.SetTrigger("atilma");
            atiliyor = true;
            atilmaBekliyor = true;
            atilmaBeklemeSuresi = atilmaClip.length;
            atilmaSuresi = atilmaClip.length / 2;
            /*if (transform.localScale.x == 1)
                Instantiate(dashEfektSaga, transform.position, transform.rotation);
            if (transform.localScale.x == -1)
                Instantiate(dashEfektSola, transform.position, transform.rotation);*/
        }
    }
}