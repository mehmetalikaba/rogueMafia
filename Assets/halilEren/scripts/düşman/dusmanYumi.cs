using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class dusmanYumi : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    GameObject oyuncu;
    RaycastHit2D raycastHit;
    canKontrol canKontrol;
    public LayerMask engelLayer;

    public bool okFirlatamaz;
    bool okFirlat, geriKac, yaklas, takla, davrandi;
    public GameObject solaOk, sagaOk;

    public float hareketHizi,atilmaGucu;
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
    void LateUpdate()
    {
        if(!canKontrol.oyuncuDead)
        {
            animator.SetBool("yurume", false);

            float oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);

            if (oyuncuyaYakinlik > 6)
            {
                okFirlat = false;
                geriKac = false;
                yaklas = true;
            }
            if ((oyuncuyaYakinlik <= 10 && 6 >= oyuncuyaYakinlik) && !geriKac)
            {
                geriKac = false;
                yaklas = false;
                okFirlat = true;
            }
            if (oyuncuyaYakinlik < 1.5f)
            {
                okFirlat = false;
                yaklas = false;
                geriKac = true;
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

            Yaklas();
            OkFirlat();
            GeriKac();
            Takla();
        }
    }
    void Yaklas()
    {
        if(yaklas)
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
    void OkFirlat()
    {
        if(okFirlat)
        {
            animator.SetBool("yurume", false);
            okTimer += Time.deltaTime;
            if (okTimer >= 1.25f)
            {
                animator.SetTrigger("ok");
                StartCoroutine(okFirlamaZamani());
                okTimer = 0;
            }
        }
    }
    void GeriKac()
    {
        if(geriKac)
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
    void Takla()
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
    }
    IEnumerator okFirlamaZamani()
    {
        yield return new WaitForSeconds(0.7f);

        if (!okFirlatamaz&&okFirlat)
        {
            if (transform.localScale.x == -1)
            {
                Instantiate(solaOk, transform.position, solaOk.transform.rotation);
            }
            if (transform.localScale.x == 1)
            {
                Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
            }
        }
    }
}
