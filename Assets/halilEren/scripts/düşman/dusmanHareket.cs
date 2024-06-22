using UnityEngine;
using UnityEngine.XR;

public class dusmanHareket : MonoBehaviour
{
    public GameObject uyari;
    GameObject oyuncu;

    public float hareketHizi, sagaGitmeSuresi,solaGitmeSuresi,beklemeSuresi,uyariBeklemeSuresi;
    float sagaGitmeTimer,solaGitmeTimer,beklemeTimer,uyariBeklemeTimer;
    public bool saga, sola,gordu;
    bool bekleSag, bekleSol;

    RaycastHit2D oyuncuHitSag,oyuncuHitSol;
    public LayerMask oyuncuLayer;
    public float gorusMesafesi,saldiriMesafesi;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        animator=GetComponent<Animator>();
        saga = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gordu)
        {
            Nobet();
            Kontrol();
        }
        else
        {
            animator.SetBool("yurume", false);

            uyariBeklemeTimer += Time.deltaTime;

            if (uyariBeklemeTimer >= uyariBeklemeSuresi)
            {
                beklemeTimer = 0;
                SaldirKos();
            }
        }
    }
    void Nobet()
    {
        if (saga)
        {
            animator.SetBool("yurume", true);

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
            animator.SetBool("yurume", false);

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
            animator.SetBool("yurume", true);

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
            animator.SetBool("yurume", false);

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
            Debug.Log("gördü");
            Instantiate(uyari,transform.position, Quaternion.identity); 
            gordu = true;
        }
    }

    void SaldirKos()
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

    }
}
