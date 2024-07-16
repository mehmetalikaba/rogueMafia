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
    public bool hasarObjesiAktif, yumruk1, yumruk2;
    public float sonHasar, sonSaldiriMenzili, silahDayanikliligiAzalmaMiktari;

    public silahOzellikleriniGetir silah1Script, silah2Script, yumrukScript;
    public silahUltileri silahUltileri;

    public Image silah1Image, silah2Image, silah1DayanikliligiImage, silah2DayanikliligiImage;
    public Sprite yumrukSprite;

    private void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silahUltileri = FindObjectOfType<silahUltileri>();
        silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

        silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
        silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;
    }
    private void Update()
    {
        if (!firlatildi && !oyuncuHareket.havada)
        {
            if (silah1Script != null && !yumruk1 && (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu"))))
            {
                silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();

                if (hasarObjesiAktif)
                    sonHasar = silah1Script.silahSaldiriHasari * 2;
                else
                    sonHasar = silah1Script.silahSaldiriHasari;

                sonSaldiriMenzili = silah2Script.silahSaldiriMenzili;
                animator.runtimeAnimatorController = silah1Script.karakterAnimator;
                yakinSaldiri(silah1Script.silahDayanikliligi);
            }
            if (silah2Script != null && !yumruk2 && (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu"))))
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

        /*if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("bTusu")))
        {
            animator.runtimeAnimatorController = oyuncuAnimator;
        }*/
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, silah1Script.silahSaldiriMenzili);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, silah2Script.silahSaldiriMenzili);
    }
    IEnumerator okZaman(float beklemeSuresi1, float beklemeSuresi2, float silahDayanikliligi)
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

        if (silahDayanikliligi <= 0)
        {
            silahUltileri.silah2Ulti = 0f;
            yumruk2 = true;
            animator.runtimeAnimatorController = oyuncuAnimator;
            silah2Script.silahSecimi.silahSec(yumrukScript.silahAdi.ToLower());
            silah2Script.silahOzellikleriniGuncelle();
            silah2Image.sprite = yumrukSprite;
        }
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

        silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;

        if (silahDayanikliligi <= 0)
        {
            silahUltileri.silah1Ulti = 0f;
            yumruk1 = true;
            animator.runtimeAnimatorController = oyuncuAnimator;
            silah1Script.silahSecimi.silahSec(yumrukScript.silahAdi.ToLower());
            silah1Script.silahOzellikleriniGuncelle();
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

        silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;

        StartCoroutine(okZaman(silah2Script.beklemeSureleri, silah2Script.beklemeSureleri2, silahDayanikliligi));
    }
}
