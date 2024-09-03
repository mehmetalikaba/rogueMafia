using System.Collections;
using UnityEngine;

public class dusman : MonoBehaviour
{
    public bool menzilli, yakin;
    public bool kontrollerAcik, devriyeModunda, saldiriModunda, oyuncuGorusAcisinda, oyuncuSagda, oyuncuSolda, yuruyor;
    public bool sagaYuru, sagBekle, solaYuru, solBekle, sagaBakiyor, solaBakiyor;
    public RaycastHit2D oyuncuHitSag, oyuncuHitSol;
    public float hareketHizi, sagaGitmeTimer, solaGitmeTimer, beklemeTimer, sagaGitmeSuresi, solaGitmeSuresi, beklemeSuresi;

    public float kontrolTimer, oyuncuyaYakinlik, gorusMesafesi;
    public bool bekliyor, kaciyor;
    public Transform saldiriPos;

    public GameObject oyuncu, zeminKontrol, dusmanCimSes, dusmanTasSes;
    public Animator animator;
    public dusmanZeminKontrol dusmanZeminKontrol;
    public Rigidbody2D rb;
    public dusmanSaldiri dusmanSaldiri;

    void Start()
    {
        StartCoroutine(randomYurume());
    }
    void Update()
    {
        oyuncuHangiYonde();
        oyuncuyaYakinMi();
        if (kontrollerAcik)
        {
            hareketEt();
            oyuncuNerede();
            if (bekliyor)
            {
                yuruyor = false;
                animator.SetBool("kosma", false);
                animator.SetBool("nobet", false);
                animator.SetBool("idle", true);
                dusmanCimSes.SetActive(false);
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
        else if (saldiriModunda)
        {
            if (!dusmanSaldiri.oyuncuyaYakin && !dusmanSaldiri.saldiriyor)
            {
                yuru();
                dusmanSaldiri.saldirmadanOnceBekleTimer = 0f;
            }
            else if (dusmanSaldiri.oyuncuyaYakin)
            {
                if (menzilli)
                {
                    if (oyuncuyaYakinlik < dusmanSaldiri.davranmaMesafesi / 1.5f && !dusmanSaldiri.saldiriyor)
                        kac();
                    else
                    {
                        kaciyor = false;
                        dusmanSaldiri.saldirKos();
                    }
                }
                if (yakin)
                    dusmanSaldiri.saldirKos();
            }
        }
    }
    public void oyuncuNerede()
    {
        oyuncuHitSag = Physics2D.Raycast(transform.position, transform.right, gorusMesafesi, LayerMask.GetMask("Oyuncu"));
        oyuncuHitSol = Physics2D.Raycast(transform.position, -transform.right, gorusMesafesi, LayerMask.GetMask("Oyuncu"));
        if ((oyuncuHitSol.collider != null) || (oyuncuHitSag.collider != null))
        {
            saldiriModunda = true;
            oyuncuGorusAcisinda = true;
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
        else
        {
            transform.Translate(Vector3.right * hareketHizi * Time.deltaTime);
            animator.SetBool("nobet", false);
            animator.SetBool("kosma", true);
        }
        dusmanCimSes.SetActive(true);
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
        dusmanCimSes.SetActive(true);
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
    public void oyuncuyaYakinMi()
    {
        oyuncuyaYakinlik = Vector2.Distance(transform.position, oyuncu.transform.position);
        if (oyuncuyaYakinlik <= dusmanSaldiri.davranmaMesafesi)
            dusmanSaldiri.oyuncuyaYakin = true;
        else if (oyuncuyaYakinlik > dusmanSaldiri.davranmaMesafesi)
            dusmanSaldiri.oyuncuyaYakin = false;
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
}