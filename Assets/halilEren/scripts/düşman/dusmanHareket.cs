using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.XR;

public class dusmanHareket : MonoBehaviour
{
    dusmanAgresif dusmanAgresif;
    dusmanYumi dusmanYumi;
    public bool yakin, menzilli;

    public GameObject uyari, soruIsareti;
    GameObject oyuncu;

    public float hareketHizi, sagaGitmeSuresi,solaGitmeSuresi,beklemeSuresi,uyariBeklemeSuresi;
    float sagaGitmeTimer,solaGitmeTimer,beklemeTimer,uyariBeklemeTimer;
    public bool gordu;
    public bool saga,sola, bekleSag, bekleSol;

    RaycastHit2D oyuncuHitSag,oyuncuHitSol;
    public LayerMask oyuncuLayer;
    public float gorusMesafesi,saldiriMesafesi;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        animator=GetComponent<Animator>();
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            saga = true;

        }
        else
        {
            sola = true;
        }

        if (yakin)
        {
            dusmanAgresif=GetComponent<dusmanAgresif>();
        }
        if(menzilli)
        {
            dusmanYumi = GetComponent<dusmanYumi>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gordu)
        {
            Nobet();
            Kontrol();
            animator.SetBool("yurume", false);
            if (yakin)
            {
                dusmanAgresif.enabled = false;
            }
            if (menzilli)
            {
                dusmanYumi.enabled = false;
            }
        }
        else
        {
            animator.SetBool("nobet", false);

            uyariBeklemeTimer += Time.deltaTime;

            if (uyariBeklemeTimer >= uyariBeklemeSuresi)
            {
                beklemeTimer = 0;
                if(yakin)
                {
                    dusmanAgresif.enabled = true;
                }
                if(menzilli)
                {
                    dusmanYumi.enabled = true;
                }
            }

            if(oyuncu.transform.position.y<transform.position.y-2||oyuncu.transform.position.y>transform.position.y+2)
            {
                gordu = false;

            }
        }
    }
    void Nobet()
    {
        if (saga)
        {
            animator.SetBool("nobet", true);

            transform.rotation = Quaternion.Euler(0, 0, 0);

            sagaGitmeTimer += Time.deltaTime;

            transform.Translate(transform.right * hareketHizi * Time.deltaTime);

            if (sagaGitmeTimer >= sagaGitmeSuresi)
            {
                sagaGitmeTimer = 0;
                bekleSag = true;
                saga = false;
            }
        }
        if (bekleSag)
        {
            animator.SetBool("nobet", false);

            beklemeTimer += Time.deltaTime;

            if (beklemeTimer >= beklemeSuresi)
            {
                beklemeTimer = 0;
                sola = true;
                bekleSag = false;
            }
        }
        if (sola)
        {
            animator.SetBool("nobet", true);

            transform.rotation = Quaternion.Euler(0, 180, 0);

            solaGitmeTimer += Time.deltaTime;

            transform.Translate(transform.right * -hareketHizi * Time.deltaTime);

            if (solaGitmeTimer >= solaGitmeSuresi)
            {
                solaGitmeTimer = 0;
                bekleSol = true;
                sola = false;
            }
        }
        if (bekleSol)
        {
            animator.SetBool("nobet", false);

            beklemeTimer += Time.deltaTime;

            if (beklemeTimer >= beklemeSuresi)
            {
                beklemeTimer = 0;
                saga = true;
                bekleSol = false;
            }
        }
    }

    void Kontrol()
    {
        oyuncuHitSag = Physics2D.Raycast(transform.position, transform.right, gorusMesafesi, oyuncuLayer);
        oyuncuHitSol = Physics2D.Raycast(transform.position, -transform.right, gorusMesafesi, oyuncuLayer);
        if ((oyuncuHitSol.collider != null) || (oyuncuHitSag.collider != null))
        {
            Instantiate(uyari,transform.position, Quaternion.identity); 
            gordu = true;
        }
    }

    /*void SaldirKos()
    {
        float f = Vector2.Distance(transform.position, oyuncu.transform.position);
        if(f<=saldiriMesafesi)
        {
            animator.SetBool("yurume", false);
            animator.SetTrigger("saldiri");
        }
        else
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

    }*/
}
