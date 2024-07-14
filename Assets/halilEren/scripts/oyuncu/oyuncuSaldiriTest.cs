using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class oyuncuSaldiriTest : MonoBehaviour
{
    oyuncuHareket oyuncuHareket;

    public int okSayisi;
    bool firlatildi;
    public GameObject okSag, okSol, silah1, silah2;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public RuntimeAnimatorController oyuncuAnimator;
    public Animator animator;
    public bool hasarObjesiAktif;
    public float sonHasar, sonSaldiriMenzili;

    public silahOzellikleriniGetir silahTest1, silahTest2;

    private void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        silahTest1 = silah1.GetComponent<silahOzellikleriniGetir>();
        silahTest2 = silah2.GetComponent<silahOzellikleriniGetir>();
    }
    private void Update()
    {
        if (!firlatildi && !oyuncuHareket.havada)
        {
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")))
            {
                silahTest1 = silah1.GetComponent<silahOzellikleriniGetir>();

                if (hasarObjesiAktif)
                    sonHasar = silahTest1.silahSaldiriHasari * 2;
                else
                    sonHasar = silahTest1.silahSaldiriHasari;

                sonSaldiriMenzili = silahTest2.silahSaldiriMenzili;
                animator.runtimeAnimatorController = silahTest1.karakterAnimator;
                yakinSaldiri();
            }
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu")))
            {
                silahTest2 = silah2.GetComponent<silahOzellikleriniGetir>();

                if (hasarObjesiAktif)
                    sonHasar = silahTest2.silahSaldiriHasari * 2;
                else
                    sonHasar = silahTest2.silahSaldiriHasari;

                sonSaldiriMenzili = silahTest2.silahSaldiriMenzili;
                animator.runtimeAnimatorController = silahTest2.karakterAnimator;
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
        Gizmos.DrawWireSphere(saldiriPos.position, silahTest1.silahSaldiriMenzili);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, silahTest2.silahSaldiriMenzili);
    }
    IEnumerator okZaman(float beklemeSuresi1, float beklemeSuresi2)
    {
        yield return new WaitForSeconds(beklemeSuresi1);
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
        yield return new WaitForSeconds(beklemeSuresi2);
        oyuncuHareket.enabled = true;

        animator.SetBool("saldiriyor", false);

        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;

        firlatildi = false;
    }
    IEnumerator saldiriZaman(float beklemeSuresi)
    {
        yield return new WaitForSeconds(beklemeSuresi);
        oyuncuHareket.enabled = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;
    }
    void yakinSaldiri()
    {
        oyuncuHareket.enabled = false;

        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        animator.SetTrigger("saldiri");

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasar);
        }
        StartCoroutine(saldiriZaman(silahTest1.beklemeSureleri));
    }
    void menziliSaldiri()
    {
        firlatildi = true;

        animator.SetBool("saldiriyor", true);

        animator.SetTrigger("saldiri");

        oyuncuHareket.enabled = false;

        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        StartCoroutine(okZaman(silahTest2.beklemeSureleri, silahTest2.beklemeSureleri2));
    }
    public void silah1UltiSaldiri()
    {

    }
    public void silah2UltiSaldiri()
    {

    }
}
