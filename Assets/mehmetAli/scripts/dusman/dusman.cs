using System.Collections;
using UnityEngine;

public class dusman : MonoBehaviour
{
    public bool kontrollerAcik, devriyeModunda, saldiriModunda, oyuncuGorusAcisinda, oyuncuSagda, oyuncuSolda, sagaYuru, sagBekle, solaYuru, solBekle, sagaBakiyor, solaBakiyor, yuruyor;
    public RaycastHit2D oyuncuHitSag, oyuncuHitSol;
    public float hareketHizi, sagaGitmeTimer, solaGitmeTimer, beklemeTimer, sagaGitmeSuresi, solaGitmeSuresi, beklemeSuresi;

    public float kontrolTimer, oyuncuyaYakinlik, gorusMesafesi;
    public bool bekliyor, saldirabilir, saldiriyor, tekagi, katana;
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
        if (kontrollerAcik)
        {
            oyuncuyaYakinMi();
            hareketEt();
            oyuncuNerede();
            if (bekliyor)
            {
                animator.SetBool("yurume", false);
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
                    yuruyor = false;
                }
            }
            else if (sagBekle)
            {
                sagaBak();
                solaYuru = sagaYuru = solBekle = false;
                animator.SetBool("yurume", false);
                dusmanCimSes.SetActive(false);
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
                    yuruyor = false;
                }
            }
            else if (solBekle)
            {
                solaBak();
                sagaYuru = solaYuru = sagBekle = false;
                animator.SetBool("yurume", false);
                dusmanCimSes.SetActive(false);
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
            if (!dusmanSaldiri.oyuncuyaYakin)
            {
                yuru();
                dusmanSaldiri.saldirmadanOnceBekleTimer = 0f;
            }
            else if (dusmanSaldiri.oyuncuyaYakin)
                dusmanSaldiri.saldirKos();
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
            if ((sagaBakiyor && oyuncuSolda) || (solaBakiyor && oyuncuSagda))
                kontrollerAcik = false;
            if (oyuncuSagda)
                sagaBak();
            else if (oyuncuSolda)
                solaBak();
        }
        else
            oyuncuGorusAcisinda = false;
        if (saldiriModunda && !oyuncuGorusAcisinda)
            StartCoroutine(gozdenCikti());
    }
    public void oyuncuyaYakinMi()
    {
        oyuncuyaYakinlik = Vector2.Distance(transform.position, oyuncu.transform.position);
        if (oyuncuyaYakinlik <= dusmanSaldiri.davranmaMesafesi)
            dusmanSaldiri.oyuncuyaYakin = true;
        else if (oyuncuyaYakinlik > dusmanSaldiri.davranmaMesafesi)
            dusmanSaldiri.oyuncuyaYakin = false;
    }
    IEnumerator gozdenCikti()
    {
        saldiriModunda = false;
        animator.SetBool("yurume", false);
        yield return new WaitForSeconds(0.25f);
        saldiriyor = false;
        saldirabilir = false;
        oyuncuGorusAcisinda = false;
        StartCoroutine(randomYurume());
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
    public void yuru()
    {
        yuruyor = true;
        transform.Translate(Vector3.right * hareketHizi * Time.deltaTime);
        animator.SetBool("yurume", true);
        dusmanCimSes.SetActive(true);
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