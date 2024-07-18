using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class dusmanShuriken : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    GameObject oyuncu;
    RaycastHit2D raycastHit;
    canKontrol canKontrol;
    public LayerMask engelLayer;

    public bool firlatamaz;
    bool bicakFirlat, geriKac, yaklas, davrandi,atiyor;
    public GameObject solaBicak, sagaBicak;

    public float hareketHizi;
    float okTimer,atilmaTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canKontrol=FindObjectOfType<canKontrol>();  
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        yaklas = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!canKontrol.oyuncuDead)
        {
            animator.SetBool("yurume", false);

            float oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);

            if (oyuncuyaYakinlik > 5&&!atiyor)
            {
                bicakFirlat = false;
                geriKac = false;
                yaklas = true;
            }
            if ((oyuncuyaYakinlik <= 7 && 4 >= oyuncuyaYakinlik) && !geriKac)
            {
                geriKac = false;
                yaklas = false;
                bicakFirlat = true;
            }
            if (oyuncuyaYakinlik < 1.5f&&!atiyor)
            {
                bicakFirlat = false;
                yaklas = false;
                geriKac = true;
            }

            if (davrandi)
            {
                atilmaTimer += Time.deltaTime;
                if (atilmaTimer >= 0.65f)
                {
                    davrandi = false;
                    atilmaTimer = 0;
                }
            }

            Yaklas();
            BicakFirlat();
            GeriKac();
        }
    }
    void Yaklas()
    {
        if(yaklas&&!atiyor)
        {
            okTimer = 0;
            animator.SetBool("yurume", true);
            if (oyuncu.transform.position.x > transform.position.x)
            {
                transform.localScale=new Vector2(1, transform.localScale.y);
                transform.Translate(transform.right * hareketHizi * Time.deltaTime);
            }
            else
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
                transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
            }
        }
    }
    void BicakFirlat()
    {
        if(bicakFirlat)
        {
            animator.SetBool("yurume", false);
            okTimer += Time.deltaTime;
            if (okTimer >= 0.75f)
            {
                atiyor = true;
                animator.SetTrigger("ok");
                StartCoroutine(bicakFirlamaZamani());
                okTimer = 0;
            }
        }
    }
    void GeriKac()
    {
        if(geriKac&&!atiyor)
        {
            okTimer = 0;
            animator.SetBool("yurume", true);

            if (oyuncu.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
                transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
            }
            else
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
                transform.Translate(transform.right * hareketHizi * Time.deltaTime);
            }

            if (transform.localScale.x == -1)
            {
                raycastHit = Physics2D.Raycast(transform.position, -transform.right, 0.3f, engelLayer);
                if (raycastHit.collider != null)
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
                raycastHit = Physics2D.Raycast(transform.position, transform.right, 0.3f, engelLayer);
                if (raycastHit.collider != null)
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
            }
        }
    }
    IEnumerator bicakFirlamaZamani()
    {
        atiyor = true;
        yield return new WaitForSeconds(0.5f);
        if (!firlatamaz&&bicakFirlat)
        {
            if (transform.localScale.x == -1)
            {
                Instantiate(solaBicak, transform.position, solaBicak.transform.rotation);
            }
            if (transform.localScale.x == 1)
            {
                Instantiate(sagaBicak, transform.position, sagaBicak.transform.rotation);
            }
            atiyor = false;
        }
    }
}
