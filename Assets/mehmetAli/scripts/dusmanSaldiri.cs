using System.Collections;
using UnityEditor;
using UnityEngine;

public class dusmanSaldiri : MonoBehaviour
{
    public RaycastHit2D oyuncuHitSag, oyuncuHitSol;
    public LayerMask engelLayer, oyuncuLayer;
    public bool oyuncuYakin, oyuncuSagda, oyuncuSolda, oyuncuVar, yuruyor, sagaYuru, bekleSag, solaYuru, bekleSol;
    public float oyuncuyaYakinlik, hareketHizi, timer, beklemeTimer, solaGitmeTimer, sagaGitmeTimer, sagaGitmeSuresi, solaGitmeSuresi, beklemeSuresi, gorusMesafesi;
    public GameObject oyuncu, zeminKontrol, dusmanCimSes, dusmanTasSes;
    public Animator animator;
    public dusmanZeminKontrol dusmanZeminKontrol;
    public Rigidbody2D rb;

    dusmanAgresif dusmanAgresif;

    private void Awake()
    {
        randomYurume();
    }

    private void Start()
    {
        oyuncu = GameObject.Find("Oyuncu");
        animator = GetComponent<Animator>();
        dusmanZeminKontrol = zeminKontrol.GetComponent<dusmanZeminKontrol>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);

        yuru();

        oyuncuNerede();
    }
    public void yuru()
    {
        if (!yuruyor)
        {
            sagaYuru = false;
            solaYuru = false;
            bekleSag = false;
            bekleSag = false;
            animator.SetBool("yurume", false);
            dusmanCimSes.SetActive(false);
            dusmanTasSes.SetActive(false);
        }
        else if (yuruyor)
        {
            if (sagaYuru)
            {
                solaYuru = false;

                bekleSol = false;
                bekleSag = false;
                animator.SetBool("yurume", true);
                dusmanCimSes.SetActive(true);
                //dusmanTasSes.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(transform.right * hareketHizi * Time.deltaTime);
                sagaGitmeTimer += Time.deltaTime;
                if (sagaGitmeTimer >= sagaGitmeSuresi)
                {
                    sagaGitmeTimer = 0;
                    sagaYuru = false;
                    bekleSag = true;
                }
            }
            if (bekleSag)
            {
                bekleSol = false;

                solaYuru = false;
                sagaYuru = false;
                animator.SetBool("yurume", false);
                dusmanCimSes.SetActive(false);
                beklemeTimer += Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (beklemeTimer >= beklemeSuresi)
                {
                    beklemeTimer = 0;
                    bekleSag = false;
                    solaYuru = true;
                }
            }
            if (solaYuru)
            {
                sagaYuru = false;

                bekleSol = false;
                bekleSag = false;
                animator.SetBool("yurume", true);
                dusmanCimSes.SetActive(true);
                //dusmanTasSes.SetActive(true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(transform.right * -hareketHizi * Time.deltaTime);
                sagaGitmeTimer += Time.deltaTime;
                if (solaGitmeTimer >= solaGitmeSuresi)
                {
                    solaGitmeTimer = 0;
                    solaYuru = false;
                    bekleSol = true;
                }
            }
            if (bekleSol)
            {
                bekleSag = false;

                solaYuru = false;
                sagaYuru = false;
                animator.SetBool("yurume", false);
                dusmanCimSes.SetActive(false);
                beklemeTimer += Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (beklemeTimer >= beklemeSuresi)
                {
                    beklemeTimer = 0;
                    bekleSol = false;
                    sagaYuru = true;
                }
            }
        }
    }
    public void oyuncuNerede()
    {
        oyuncuHitSag = Physics2D.Raycast(transform.position, transform.right, gorusMesafesi, oyuncuLayer);
        oyuncuHitSol = Physics2D.Raycast(transform.position, -transform.right, gorusMesafesi, oyuncuLayer);
        if ((oyuncuHitSol.collider != null) || (oyuncuHitSag.collider != null))
        {
            if (!oyuncuVar)
                oyuncuVar = true;
            else
            if (oyuncuVar)
                StartCoroutine(gozdenCikti());
        }
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 3f, oyuncuLayer);
        if (oyuncuYakin)
        {
            if (oyuncu.transform.position.x > transform.position.x)
            {
                if (!oyuncuSagda)
                {
                    oyuncuSagda = true;
                    oyuncuSolda = false;
                    timer = 0f;
                }
            }
            else if (oyuncu.transform.position.x < transform.position.x)
            {
                if (!oyuncuSolda)
                {
                    oyuncuSolda = true;
                    oyuncuSagda = false;
                    timer = 0f;
                }
            }
            timer += Time.deltaTime;
            if (oyuncuSagda && timer >= 1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                timer = 0f;
            }
            else if (oyuncuSolda && timer >= 1f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                timer = 0f;
            }
        }
    }
    IEnumerator gozdenCikti()
    {
        animator.SetBool("yurume", false);
        yield return new WaitForSeconds(0.25f);

        oyuncuVar = false;
        //dusmanAgresif.gordu = false;
    }
    public void randomYurume()
    {
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            sagaYuru = true;
            solaYuru = false;
        }
        else
        {
            solaYuru = true;
            sagaYuru = false;
        }
        float a = Random.Range(1f, 3f);
        float b = Random.Range(1f, 3f);
        float c = Random.Range(1f, 3f);
        sagaGitmeSuresi = a;
        solaGitmeTimer = b;
        beklemeSuresi = c;
    }
}










/*
public bool oyuncuYakin, oyuncuSagda, oyuncuSolda, saga, sola, , oyuncuVar;
public float timer, oyuncuyaYakinlik, hareketHizi, sagaGitmeSuresi, solaGitmeSuresi, beklemeSuresi, sagaGitmeTimer, solaGitmeTimer, beklemeTimer, gorusMesafesi;
public RaycastHit2D oyuncuHitSag, oyuncuHitSol;
public LayerMask engelLayer, oyuncuLayer;
public int atilmaSayac;

public GameObject oyuncu, zeminKontrol, uyari, soruIsareti, dusmanCimSes, dusmanTasSes;
public Animator animator;
public Rigidbody2D rb;
public Transform saldiriPos;
public dusmanHasar dusmanHasar;

dusmanYumi dusmanYumi;
canKontrol canKontrol;
dusmanZeminKontrol dusmanZeminKontrol;


private void Awake()
{
    
}
void Start()
{
    oyuncu = GameObject.Find("Oyuncu");
    dusmanHasar = GetComponent<dusmanHasar>();
    dusmanAgresif = GetComponent<dusmanAgresif>();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    dusmanZeminKontrol = zeminKontrol.GetComponent<dusmanZeminKontrol>();
}

void Update()
{
    oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);

    devriyeAt();
    oyuncuNerede();
    Kontrol();

    if (!dusmanZeminKontrol.zeminde)
    {
        animator.SetBool("yurume", false);
        animator.SetBool("nobet", false);
        animator.SetBool("saldiri", false);
        animator.SetBool("atilma", false);
    }
}



public void devriyeAt()
{
    if (!oyuncuYakin && dusmanZeminKontrol.zeminde)
    {
        Debug.Log("nobet tutuyor");
        if (saga)
        {
            animator.SetBool("nobet", true);
            dusmanCimSes.SetActive(true);
            dusmanTasSes.SetActive(true);
            sagaGitmeTimer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
            dusmanCimSes.SetActive(false);
            dusmanTasSes.SetActive(false);
            beklemeTimer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
            dusmanCimSes.SetActive(true);
            dusmanTasSes.SetActive(true);
            solaGitmeTimer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 180, 0);
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
            dusmanCimSes.SetActive(false);
            dusmanTasSes.SetActive(false);
            beklemeTimer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (beklemeTimer >= beklemeSuresi)
            {
                beklemeTimer = 0;
                saga = true;
                bekleSol = false;
            }
        }
    }
    else
    {
        if (!dusmanZeminKontrol.zeminde)
        {
            Debug.Log("zeminden cikti oyuncu yakin");
            if (sola)
                transform.Translate(transform.right * -hareketHizi * Time.deltaTime);
            else if (saga)
                transform.Translate(transform.right * hareketHizi * Time.deltaTime);
        }
        else
        {
            Debug.Log("nobetten cikti oyuncu yakin");
            if (oyuncuYakin)
            {
                saga = false;
                bekleSol = false;
                sola = false;
                bekleSag = false;

                if (oyuncuyaYakinlik > dusmanAgresif.davranmaMesafesi)
                {
                    Debug.Log("davranma mesafesine geliyor");
                    animator.SetBool("yurume", true);
                    animator.SetBool("nobet", false);
                    if (oyuncuSolda)
                        transform.Translate(transform.right * -hareketHizi * Time.deltaTime);
                    else if (oyuncuSagda)
                        transform.Translate(transform.right * hareketHizi * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("yurume", false);
                    animator.SetBool("nobet", false);
                }
            }

        }
    }
}
public void oyuncuNerede()
{
    oyuncuYakin = Physics2D.OverlapCircle(transform.position, 3f, oyuncuLayer);

    if (oyuncuYakin)
    {
        if (oyuncu.transform.position.x > transform.position.x)
        {
            if (!oyuncuSagda)
            {
                oyuncuSagda = true;
                oyuncuSolda = false;
                timer = 0f;
            }
        }
        else if (oyuncu.transform.position.x < transform.position.x)
        {
            if (!oyuncuSolda)
            {
                oyuncuSolda = true;
                oyuncuSagda = false;
                timer = 0f;
            }
        }

        timer += Time.deltaTime;

        if (oyuncuSagda && timer >= 1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timer = 0f;
        }
        else if (oyuncuSolda && timer >= 1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            timer = 0f;
        }
    }
}
}





/*
public void solaKos()
{
    if (!dusmanZeminKontrol.zemindeDegil)
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
    }
}
public void sagaKos()
{
    if (!dusmanZeminKontrol.zemindeDegil)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(transform.right * hareketHizi * Time.deltaTime);
    }
}

public void duvarVarMi()
{
    raycastHitSol = Physics2D.Raycast(transform.position, -transform.right, 0.3f, engelLayer);
    raycastHitSag = Physics2D.Raycast(transform.position, transform.right, 0.3f, engelLayer);
    if (raycastHitSag.collider != null) sagdaDuvarVar = true;
    else if (raycastHitSol.collider != null) soldaDuvarVar = true;

    //if (soldaDuvarVar || sagdaDuvarVar)
    //{
    //    timer -= Time.deltaTime;
    //    if (timer < 0)
    //    {
    //        timer = 1.5f;
    //        sagdaDuvarVar = false;
    //        soldaDuvarVar = false;
    //    }
    //}

    if (oyuncuYakin)
    {
        if (soldaDuvarVar && oyuncuSagda || soldaDuvarVar && oyuncuSolda)
            sagaKos();
        else if (sagdaDuvarVar && oyuncuSolda || sagdaDuvarVar && oyuncuSagda)
            solaKos();
        else if (!sagdaDuvarVar && !soldaDuvarVar)
        {
            if (oyuncuSolda)
                sagaKos();
            else if (oyuncuSagda)
                solaKos();
        }
    }
}
*/