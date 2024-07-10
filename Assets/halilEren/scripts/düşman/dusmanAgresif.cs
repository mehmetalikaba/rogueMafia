using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class dusmanAgresif : MonoBehaviour
{
    Rigidbody2D rb;

    public LayerMask dusmanLayer;

    public Transform saldiriPos;
    public GameObject uyari;
    GameObject oyuncu;

    public float hareketHizi,atilmaGucu, uyariBeklemeSuresi,hasar;
    float  uyariBeklemeTimer,atilmaTimer;
    public bool gordu;

    RaycastHit2D oyuncuHitSag, oyuncuHitSol;
    public LayerMask oyuncuLayer;
    public float gorusMesafesi, davranmaMesafesi,saldiriAlan;

    public Animator animator;

    bool davrandi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gordu)
        {
            Kontrol();
        }
        else
        {
            animator.SetBool("yurume", false);

            uyariBeklemeTimer += Time.deltaTime;

            if (uyariBeklemeTimer >= uyariBeklemeSuresi)
            {
                SaldirKos();
            }
        }
        if(davrandi)
        {
            atilmaTimer += Time.deltaTime;
            if(atilmaTimer>=1f)
            {
                davrandi = false;
                atilmaTimer = 0;
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
    }

    void SaldirKos()
    {
        float f = Vector2.Distance(transform.position, oyuncu.transform.position);
        if (f <= davranmaMesafesi)
        {
            if(!davrandi)
            {
                int i = Random.Range(0, 3);
                if(i==1)
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
        }
        else
        {
            if(!davrandi)
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
