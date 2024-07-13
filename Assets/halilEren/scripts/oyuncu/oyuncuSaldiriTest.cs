using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class oyuncuSaldiriTest : MonoBehaviour
{
    oyuncuHareket oyuncuHareket;

    public int okSayisi;
    bool firlatildi;
    public GameObject okSag, okSol, silah1, silah2;
    silahOzellikleriniGetir silahTest1, silahTest2;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public float saldiriMenzili1, saldiriMenzili2;
    public float sonSaldiriMenzili;
    public float hasar1, hasar2;
    public float sonHasar;
    public RuntimeAnimatorController oyuncuAnimator, silah1Animator, silah2Animator;
    public Animator animator;
    public bool hasarObjesiAktif;

    int comboSayac;

    private void Start()
    {

        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        silahTest1 = silah1.GetComponent<silahOzellikleriniGetir>();
        silahTest2 = silah2.GetComponent<silahOzellikleriniGetir>();

        silahGuncelle();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            silahGuncelle();
        }
        if (Input.GetKeyDown(tusDizilimiGetirTest.instance.tusIsleviGetir("silah1Tus")))
        {
            sonSaldiriMenzili = saldiriMenzili1;

            if (hasarObjesiAktif)
                sonHasar = (hasar1 * 2);
            else
                sonHasar = hasar1;

            if (silahTest1.silahTuru == "yakin")
            {
                yakinSaldiri();
            }
            else if (silahTest1.silahTuru == "rogue")
            {
                rogueSaldiri();
            }
            else if (silahTest1.silahTuru == "menzilli")
            {
                menziliSaldiri();
            }
        }
        if (Input.GetKeyDown(tusDizilimiGetirTest.instance.tusIsleviGetir("silah2Tus")))
        {
            sonSaldiriMenzili = saldiriMenzili2;

            if (hasarObjesiAktif)
                sonHasar = (hasar2 * 2);
            else
                sonHasar = hasar2;

            if (silahTest2.silahTuru == "yakin")
            {
                yakinSaldiri();
            }
            else if (silahTest2.silahTuru == "rogue")
            {
                rogueSaldiri();
            }
            else if (silahTest2.silahTuru == "menzilli")
            {
                menziliSaldiri();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.runtimeAnimatorController = oyuncuAnimator;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriMenzili1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriMenzili2);
    }
    IEnumerator okZaman()
    {
        yield return new WaitForSeconds(0.55f);
        if (transform.localScale.x == 1)
        {
            for (int i = 0; i < okSayisi; i++)
            {
                Instantiate(okSag, transform.position, okSag.transform.rotation);
            }
        }
        if (transform.localScale.x == -1)
        {
            for (int i = 0; i < okSayisi; i++)
            {
                Instantiate(okSol, transform.position, okSol.transform.rotation);
            }
        }
        yield return new WaitForSeconds(0.25f);
        oyuncuHareket.enabled = true;

        animator.SetBool("saldiriyor", false);

        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;

        firlatildi = false;
    }
    IEnumerator saldiriZaman()
    {
        yield return new WaitForSeconds(0.5f);
        oyuncuHareket.enabled = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;

    }

    void yakinSaldiri()
    {
        if (!firlatildi)
        {
            oyuncuHareket.enabled = false;

            oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

            animator.runtimeAnimatorController = silah1Animator;

            animator.SetTrigger("saldiri");

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasar);
            }

            StartCoroutine(saldiriZaman());

        }
    }

    void menziliSaldiri()
    {
        if (!firlatildi)
        {
            firlatildi = true;

            animator.runtimeAnimatorController = silah2Animator;

            animator.SetBool("saldiriyor", true);

            animator.SetTrigger("saldiri");

            oyuncuHareket.enabled = false;

            oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

            StartCoroutine(okZaman());
        }
    }

    void rogueSaldiri()
    {
        comboSayac++;
        if (comboSayac == 3)
        {
            menziliSaldiri();
            comboSayac = 0;
        }
        else
        {
            yakinSaldiri();
        }
    }

    public void silah1UltiSaldiri()
    {

    }
    public void silah2UltiSaldiri()
    {

    }

    public void silahGuncelle()
    {
        saldiriMenzili1 = silahTest1.silahSaldiriMenzili;
        saldiriMenzili2 = silahTest2.silahSaldiriMenzili;
        hasar1 = silahTest1.silahSaldiriHasari;
        hasar2 = silahTest2.silahSaldiriHasari;
        silah1Animator = silahTest1.karakterAnimator;
        silah2Animator = silahTest2.karakterAnimator;
    }
}
