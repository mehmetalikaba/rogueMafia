using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanYumi : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    GameObject oyuncu;

    bool okFirlat, geriKac, yaklas, takla, davrandi;
    public GameObject solaOk, sagaOk;

    public float hareketHizi,atilmaGucu;
    float okTimer,atilmaTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        yaklas = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);

        if (oyuncuyaYakinlik > 8)
        {
            yaklas = true;
            okFirlat = false;
            geriKac = false;
        }
        if ((oyuncuyaYakinlik<=10&&8>=oyuncuyaYakinlik)&&!geriKac)
        {
            yaklas=false;
            okFirlat = true;
        }
        if(oyuncuyaYakinlik<5&&oyuncuyaYakinlik>1.5f)
        {
            okFirlat = false;
            takla = false;
            geriKac = true;
        }
        if(oyuncuyaYakinlik<=1.5f)
        {
            geriKac=false;
            okFirlat = false;
            takla = true;
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
    void Yaklas()
    {
        if(yaklas)
        {
            okTimer = 0;
            animator.SetBool("ok", false);
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
            if(okTimer>=1.25f)
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

            animator.SetBool("ok", false);

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
        yield return new WaitForSeconds(0.75f);
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
