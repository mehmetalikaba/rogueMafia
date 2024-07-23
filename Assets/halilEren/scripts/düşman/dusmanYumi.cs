using System.Collections;
using UnityEngine;

public class dusmanYumi : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    GameObject oyuncu;
    canKontrol canKontrol;
    public LayerMask engelLayer;
    public bool geriKac, yaklas, suAndaOkAtiyor, atiyor, okFirlat, soldaDuvarVar, sagdaDuvarVar, oyuncuSolda, oyuncuSagda;
    public GameObject solaOk, sagaOk;
    public float hareketHizi, atilmaGucu, timer, oyuncuyaYakinlik, okTimer;
    public RaycastHit2D raycastHitSol, raycastHitSag;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        yaklas = true;
    }

    void FixedUpdate()
    {
        if (!canKontrol.oyuncuDead)
        {
            animator.SetBool("yurume", false);
            oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);
            if (oyuncuyaYakinlik < 3.5f)
            {
                geriKac = true;
                okFirlat = false;
                yaklas = false;

            }
            else if (oyuncuyaYakinlik >= 3.5f)
            {
                geriKac = false;
                okFirlat = true;
                yaklas = false;
            }
            else if (oyuncuyaYakinlik > 7)
            {
                geriKac = false;
                okFirlat = false;
                yaklas = true;
            }

            if (yaklas)
                Yaklas();
            else if (okFirlat)
                OkFirlat();
            else if (geriKac)
                GeriKac();

            oyuncuNerede();
        }
    }
    void Yaklas()
    {
        okTimer = 0;
        if (!suAndaOkAtiyor)
        {
            animator.SetBool("yurume", true);
            if (oyuncuSagda)
                sagaKos();
            else
                solaKos();
        }
    }
    void OkFirlat()
    {
        if (oyuncu.transform.position.x > transform.position.x)
            transform.localScale = new Vector2(1, transform.localScale.y);
        else
            transform.localScale = new Vector2(-1, transform.localScale.y);

        if (okFirlat)
        {
            if (!atiyor)
            {
                animator.SetBool("yurume", false);
                okTimer += Time.deltaTime;
                if (okTimer > 0.7f)
                {
                    suAndaOkAtiyor = true;
                    animator.SetTrigger("ok");
                    okTimer = 0f;
                    StartCoroutine(okZamanlayici());
                }
            }
        }
    }
    IEnumerator okZamanlayici()
    {
        atiyor = true;
        yield return new WaitForSeconds(0.7f);
        if (transform.localScale.x == -1 && oyuncuSolda)
            Instantiate(solaOk, transform.position, solaOk.transform.rotation);
        if (transform.localScale.x == 1 && oyuncuSagda)
            Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
        suAndaOkAtiyor = false;
        yield return new WaitForSeconds(1.3f);
        atiyor = false;
    }

    void GeriKac()
    {
        okTimer = 0;
        if (!suAndaOkAtiyor)
        {
            animator.SetBool("yurume", true);
            raycastHitSol = Physics2D.Raycast(transform.position, -transform.right, 0.3f, engelLayer);
            raycastHitSag = Physics2D.Raycast(transform.position, transform.right, 0.3f, engelLayer);
            if (raycastHitSag.collider != null)
            {
                geriKac = true;
                sagdaDuvarVar = true;
            }
            else if (raycastHitSol.collider != null)
            {
                geriKac = true;
                soldaDuvarVar = true;
            }
            if (soldaDuvarVar || sagdaDuvarVar)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 1.5f;
                    sagdaDuvarVar = false;
                    soldaDuvarVar = false;
                }
            }
            if (soldaDuvarVar && oyuncuSagda || soldaDuvarVar && oyuncuSolda)
                sagaKos();
            else if (sagdaDuvarVar && oyuncuSolda || sagdaDuvarVar && oyuncuSagda)
                solaKos();
            else if (!sagdaDuvarVar && !soldaDuvarVar)
            {
                if (oyuncuSolda)
                    sagaKos();
                else if (oyuncuSagda)
                    solaKos();
            }
        }
    }
    void oyuncuNerede()
    {
        if (oyuncu.transform.position.x > transform.position.x)
        {
            oyuncuSagda = true;
            oyuncuSolda = false;
        }
        else if (oyuncu.transform.position.x < transform.position.x)
        {
            oyuncuSolda = true;
            oyuncuSagda = false;
        }
    }
    public void solaKos()
    {
        transform.localScale = new Vector2(-1, transform.localScale.y);
        transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
    }
    public void sagaKos()
    {
        transform.localScale = new Vector2(1, transform.localScale.y);
        transform.Translate(transform.right * hareketHizi * Time.deltaTime);
    }
}




















/*
if (transform.localScale.x == -1)
{

    if (raycastHitSol.collider != null)
    {
        transform.localScale = new Vector2(1, transform.localScale.y);

        geriKac = false;
        transform.Translate(transform.right * hareketHizi * Time.deltaTime);
        animator.SetBool("yurume", false);

    }
    else
    {
        geriKac = true;
        animator.SetBool("yurume", true);
    }
}
else
{

    if (raycastHitSag.collider != null)
    {
        transform.localScale = new Vector2(-1, transform.localScale.y);

        geriKac = false;
        transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
        animator.SetBool("yurume", false);

    }
    else
    {
        geriKac = true;
        animator.SetBool("yurume", true);
    }
}*/

/*void Takla()
{
    if(takla&&!davrandi)
    {
        animator.SetBool("yurume", false);
        animator.SetTrigger("atilma");

        if (transform.position.x > oyuncu.transform.position.x)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);

            rb.velocity = Vector2.left * atilmaGucu;
            davrandi = true;

        }
        else
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);

            rb.velocity = Vector2.right * atilmaGucu;
            davrandi = true;

        }
    }
}*/