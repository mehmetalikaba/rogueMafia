using System.Collections;
using UnityEngine;

public class dusmanAgresif : MonoBehaviour
{
    public RaycastHit2D raycastHitDuvar, raycastHitOyuncu, oyuncuHitSag, oyuncuHitSol;
    public LayerMask engelLayer, dusmanLayer, oyuncuLayer;
    public bool duvarVar, tekagi, gordu, davrandi;
    public float oyuncuyaYakinlik, gorusMesafesi, davranmaMesafesi, saldiriAlan, uyariBeklemeTimer, atilmaTimer, hareketHizi, atilmaGucu, uyariBeklemeSuresi, hasar;
    public Animator animator;
    public Transform saldiriPos;
    public GameObject uyari;
    GameObject oyuncu;
    canKontrol canKontrol;
    Rigidbody2D rb;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        rb = GetComponent<Rigidbody2D>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        animator = GetComponent<Animator>();

        gordu = true;

    }

    void FixedUpdate()
    {
        if (tekagi)
        {
            oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);
            if (oyuncuyaYakinlik < davranmaMesafesi / 1.25f)
            {
                if (oyuncu.transform.position.x > transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (oyuncu.transform.position.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        if (transform.rotation.y == 180 || transform.rotation.y == -180)
        {
            raycastHitDuvar = Physics2D.Raycast(transform.position, -transform.right, 1, engelLayer);
            if (raycastHitDuvar.collider != null)
            {
                duvarVar = true;
                animator.SetBool("yurume", false);
            }
            else
                duvarVar = false;
        }
        else
        {
            raycastHitDuvar = Physics2D.Raycast(transform.position, transform.right, 1, engelLayer);
            if (raycastHitDuvar.collider != null)
            {
                duvarVar = true;
                animator.SetBool("yurume", false);
            }
            else
                duvarVar = false;
        }

        if (!canKontrol.oyuncuDead)
        {
            animator.SetBool("yurume", false);
            if (!gordu)
                Kontrol();
            else
            {
                animator.SetBool("yurume", false);
                SaldirKos();

            }
            if (davrandi)
            {
                atilmaTimer += Time.deltaTime;
                if (atilmaTimer >= 0.75f)
                {
                    davrandi = false;
                    atilmaTimer = 0;
                }
            }
        }
    }
    void Kontrol()
    {
        oyuncuHitSag = Physics2D.Raycast(transform.position, transform.right, gorusMesafesi, oyuncuLayer);
        oyuncuHitSol = Physics2D.Raycast(transform.position, -transform.right, gorusMesafesi, oyuncuLayer);
        if ((oyuncuHitSol.collider != null) || (oyuncuHitSag.collider != null))
        {
            Instantiate(uyari, transform.position, Quaternion.identity);
            gordu = true;
        }
        else
        {
            gordu = false;
        }
    }
    void SaldirKos()
    {
        float f = Vector2.Distance(transform.position, oyuncu.transform.position);
        if (f <= davranmaMesafesi)
        {
            if (!davrandi)
            {
                if (!tekagi)
                {
                    int i = Random.Range(0, 3);
                    if (i == 1)
                    {
                        animator.SetBool("yurume", false);
                        animator.SetTrigger("atilma");
                        if (transform.position.x > oyuncu.transform.position.x)
                        {
                            rb.velocity = Vector2.left * atilmaGucu;
                            davrandi = true;
                        }
                        else
                        {
                            rb.velocity = Vector2.right * atilmaGucu;
                            davrandi = true;
                        }
                    }
                    else
                    {
                        animator.SetBool("yurume", false);
                        animator.SetTrigger("saldiri");
                        Collider2D[] toDamage = Physics2D.OverlapCircleAll(saldiriPos.position, saldiriAlan, dusmanLayer);
                        for (int a = 0; a < toDamage.Length; a++)
                        {
                            canKontrol can = FindObjectOfType<canKontrol>();
                            can.canAzalmasi(hasar);
                        }
                        davrandi = true;
                    }
                }
                else
                {
                    animator.SetBool("yurume", false);
                    animator.SetTrigger("saldiri");
                    Collider2D[] toDamage = Physics2D.OverlapCircleAll(saldiriPos.position, saldiriAlan, dusmanLayer);
                    for (int a = 0; a < toDamage.Length; a++)
                    {
                        canKontrol can = FindObjectOfType<canKontrol>();
                        can.canAzalmasi(hasar);
                    }
                    davrandi = true;
                }
            }
        }
        else
        {
            if (!davrandi)
            {
                animator.SetBool("yurume", true);
                if (oyuncu.transform.position.x > transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(transform.right * hareketHizi * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriAlan);
    }
}
