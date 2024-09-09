using System.Collections;
using UnityEngine;

public class dusman : MonoBehaviour
{
    public bool menzilli, yakin, topcu;
    public bool kontrollerAcik, devriyeModunda, saldiriModunda, oyuncuGorusAcisinda, oyuncuSagda, oyuncuSolda, yuruyor;
    public bool sagaYuru, sagBekle, solaYuru, solBekle, sagaBakiyor, solaBakiyor;
    public RaycastHit2D oyuncuHitSag, oyuncuHitSol;
    public float hareketHizi, sagaGitmeTimer, solaGitmeTimer, beklemeTimer, sagaGitmeSuresi, solaGitmeSuresi, beklemeSuresi;

    public float kontrolTimer, oyuncuyaYakinlik, gorusMesafesi;
    public bool bekliyor, kaciyor;
    public Transform saldiriPos;

    public GameObject oyuncu, zeminKontrol, unlem, soruIsareti;
    public Animator animator;
    public dusmanZeminKontrol dusmanZeminKontrol;
    public Rigidbody2D rb;
    public dusmanSaldiri dusmanSaldiri;

    void Start()
    {
        oyuncu = GameObject.Find("Oyuncu");
        if (!topcu)
            StartCoroutine(randomYurume());
    }
    void Update()
    {
        oyuncuHangiYonde();
        oyuncuyaYakinMi();
        if (topcu)
            topcuKontrol();
        if (kontrollerAcik && !topcu)
        {
            hareketEt();
            oyuncuNerede();
            if (bekliyor)
            {
                yuruyor = false;
                animator.SetBool("kosma", false);
                animator.SetBool("nobet", false);
                animator.SetBool("idle", true);
            }
            else if (!devriyeModunda)
            {
                beklemeTimer = 0f;
                sagaGitmeTimer = 0f;
                solaGitmeTimer = 0f;
                sagaYuru = false;
                sagBekle = false;
                solaYuru = false;
                solBekle = false;
                animator.SetBool("nobet", false);
            }
        }
        else if (!kontrollerAcik)
        {
            kontrolTimer += Time.deltaTime;
            if (kontrolTimer > 0.35f)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                kontrolTimer = 0f;
                kontrollerAcik = true;
            }
        }
    }
    public void hareketEt()
    {
        if (devriyeModunda)
        {
            if (sagaYuru)
            {
                sagaBak();
                yuru();
                solaYuru = solBekle = sagBekle = false;
                sagaGitmeTimer += Time.deltaTime;
                if (sagaGitmeTimer >= sagaGitmeSuresi)
                {
                    sagaGitmeTimer = 0;
                    sagaYuru = false;
                    sagBekle = true;
                }
            }
            else if (sagBekle)
            {
                sagaBak();
                solaYuru = sagaYuru = solBekle = false;
                bekliyor = true;
                beklemeTimer += Time.deltaTime;
                if (beklemeTimer >= beklemeSuresi)
                {
                    beklemeTimer = 0;
                    sagBekle = false;
                    solaYuru = true;
                }
            }
            else if (solaYuru)
            {
                solaBak();
                yuru();
                sagaYuru = sagBekle = solBekle = false;
                solaGitmeTimer += Time.deltaTime;
                if (solaGitmeTimer >= solaGitmeSuresi)
                {
                    solaGitmeTimer = 0;
                    solaYuru = false;
                    solBekle = true;
                }
            }
            else if (solBekle)
            {
                solaBak();
                sagaYuru = solaYuru = sagBekle = false;
                bekliyor = true;
                beklemeTimer += Time.deltaTime;
                if (beklemeTimer >= beklemeSuresi)
                {
                    beklemeTimer = 0;
                    solBekle = false;
                    sagaYuru = true;
                }
            }
        }
        else if (saldiriModunda && !dusmanSaldiri.saldiriyor)
        {
            if (!dusmanSaldiri.oyuncuyaYakin)
            {
                oyuncuyaBak();
                yuru();
                dusmanSaldiri.saldirmadanOnceBekleTimer = 0f;
            }
            else if (dusmanSaldiri.oyuncuyaYakin)
            {
                if (menzilli && oyuncuyaYakinlik < dusmanSaldiri.davranmaMesafesi)
                    dusmanSaldiri.saldirKos();
                else if (dusmanSaldiri.saldirdiktanSonraBekliyor)
                    kac();
                else if (yakin)
                    dusmanSaldiri.saldirKos();
            }
        }
    }
    public void oyuncuNerede()
    {
        oyuncuHitSag = Physics2D.Raycast(transform.position, Vector2.right, gorusMesafesi, LayerMask.GetMask("Oyuncu"));
        oyuncuHitSol = Physics2D.Raycast(transform.position, Vector2.left, gorusMesafesi, LayerMask.GetMask("Oyuncu"));
        if ((oyuncuHitSol.collider != null) || (oyuncuHitSag.collider != null))
        {
            saldiriModunda = true;
            if (!oyuncuGorusAcisinda)
            {
                oyuncuGorusAcisinda = true;
                //Instantiate(unlem, transform.position, Quaternion.identity,transform.transform);
            }
            devriyeModunda = false;
            if (!kaciyor)
            {
                if ((sagaBakiyor && oyuncuSolda) || (solaBakiyor && oyuncuSagda))
                    kontrollerAcik = false;
                if (oyuncuSagda && !dusmanSaldiri.suAndaOkAtiyor)
                    sagaBak();
                else if (oyuncuSolda && !dusmanSaldiri.suAndaOkAtiyor)
                    solaBak();
            }
        }
        else
            oyuncuGorusAcisinda = false;
        if (saldiriModunda && !oyuncuGorusAcisinda && !dusmanSaldiri.saldiriyor)
            StartCoroutine(gozdenCikti());
    }
    public void oyuncuyaYakinMi()
    {
        if (oyuncuGorusAcisinda)
        {
            if (oyuncuSolda)
            {
                oyuncuyaYakinlik = Vector2.Distance(transform.position, oyuncuHitSol.point);
                if (oyuncuyaYakinlik < dusmanSaldiri.davranmaMesafesi)
                    dusmanSaldiri.oyuncuyaYakin = true;
                else
                    dusmanSaldiri.oyuncuyaYakin = false;
            }
            else
            {
                oyuncuyaYakinlik = Vector2.Distance(transform.position, oyuncuHitSag.point);
                if (oyuncuyaYakinlik < dusmanSaldiri.davranmaMesafesi)
                    dusmanSaldiri.oyuncuyaYakin = true;
                else
                    dusmanSaldiri.oyuncuyaYakin = false;
            }
        }
    }
    public void oyuncuHangiYonde()
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
    IEnumerator gozdenCikti()
    {
        bekliyor = true;
        //Instantiate(unlem, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.25f);
        saldiriModunda = false;
        oyuncuGorusAcisinda = false;
        StartCoroutine(randomYurume());
    }
    public void yuru()
    {
        animator.SetBool("idle", false);
        bekliyor = false;
        yuruyor = true;
        kaciyor = false;
        if (devriyeModunda)
        {
            transform.Translate(Vector3.right * (hareketHizi / 2) * Time.deltaTime);
            animator.SetBool("nobet", true);
            animator.SetBool("kosma", false);
        }
        else if (saldiriModunda)
        {
            transform.Translate(Vector3.right * hareketHizi * Time.deltaTime);
            animator.SetBool("nobet", false);
            animator.SetBool("kosma", true);
        }
    }
    public void kac()
    {
        animator.SetBool("idle", false);
        animator.SetBool("nobet", false);
        bekliyor = false;
        yuruyor = false;
        kaciyor = true;
        if (oyuncuSolda)
            sagaBak();
        if (oyuncuSagda)
            solaBak();

        transform.Translate(Vector3.right * (hareketHizi) * Time.deltaTime);
        animator.SetBool("kosma", true);
    }
    public void sagaBak()
    {
        sagaBakiyor = true;
        solaBakiyor = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void solaBak()
    {
        sagaBakiyor = false;
        solaBakiyor = true;
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void oyuncuyaBak()
    {
        if (oyuncuSagda)
            sagaBak();
        if (oyuncuSolda)
            solaBak();
    }
    IEnumerator randomYurume()
    {
        yield return new WaitForSeconds(2f);
        int i = Random.Range(0, 2);
        if (i == 1)
            sagaYuru = true;
        else
            solaYuru = true;
        float a = Random.Range(2f, 3f);
        float b = Random.Range(2f, 3f);
        float c = Random.Range(0.5f, 1.5f);
        sagaGitmeSuresi = a;
        solaGitmeSuresi = b;
        beklemeSuresi = c;
        devriyeModunda = true;
    }
    public void topcuKontrol()
    {
        if (oyuncuyaYakinlik < dusmanSaldiri.davranmaMesafesi)
            dusmanSaldiri.saldirKos();

        if (oyuncuSolda)
            solaBak();
        if (oyuncuSagda)
            sagaBak();

    }
}