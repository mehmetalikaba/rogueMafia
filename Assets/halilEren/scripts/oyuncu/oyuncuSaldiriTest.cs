using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public float sonHasar, sonSaldiriMenzili, silahDayanikliligiAzalmaMiktari;

    public silahOzellikleriniGetir silah1Script, silah2Script;

    public Image silah1Image, silah2Image, silah1DayanikliligiImage, silah2DayanikliligiImage;
    public Sprite yumrukSprite;

    private void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

        silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
        silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;
    }
    private void Update()
    {
        if (!firlatildi && !oyuncuHareket.havada)
        {
            if (silah1Script != null && (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu"))))
            {
                silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();

                silah1Script.silahDayanikliligi -= silahDayanikliligiAzalmaMiktari;
                silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;

                if (hasarObjesiAktif)
                    sonHasar = silah1Script.silahSaldiriHasari * 2;
                else
                    sonHasar = silah1Script.silahSaldiriHasari;

                sonSaldiriMenzili = silah2Script.silahSaldiriMenzili;
                animator.runtimeAnimatorController = silah1Script.karakterAnimator;
                yakinSaldiri(silah1Script.silahDayanikliligi);
            }
            if (silah2Script != null && (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu"))))
            {
                silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

                silah2Script.silahDayanikliligi -= silahDayanikliligiAzalmaMiktari;
                silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;

                if (hasarObjesiAktif)
                    sonHasar = silah2Script.silahSaldiriHasari * 2;
                else
                    sonHasar = silah2Script.silahSaldiriHasari;

                sonSaldiriMenzili = silah2Script.silahSaldiriMenzili;
                animator.runtimeAnimatorController = silah2Script.karakterAnimator;
                menziliSaldiri(silah2Script.silahDayanikliligi);
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
        Gizmos.DrawWireSphere(saldiriPos.position, silah1Script.silahSaldiriMenzili);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, silah2Script.silahSaldiriMenzili);
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
    void yakinSaldiri(float silahDayanikliligi)
    {
        oyuncuHareket.enabled = false;

        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        animator.SetTrigger("saldiri");

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasar);
        }

        if (silahDayanikliligi <= 0)
        {
            silah1Script = null;
            silah1Image.sprite = yumrukSprite;
        }
        else
            StartCoroutine(saldiriZaman(silah1Script.beklemeSureleri));
    }
    void menziliSaldiri(float silahDayanikliligi)
    {
        firlatildi = true;

        animator.SetBool("saldiriyor", true);

        animator.SetTrigger("saldiri");

        oyuncuHareket.enabled = false;

        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        if (silahDayanikliligi <= 0)
        {
            silah2Script = null;
            silah2Image.sprite = yumrukSprite;
        }
        else
            StartCoroutine(okZaman(silah2Script.beklemeSureleri, silah2Script.beklemeSureleri2));

    }
    public void silah1UltiSaldiri()
    {

    }
    public void silah2UltiSaldiri()
    {

    }
}
