using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class oyuncuSaldiriTest : MonoBehaviour
{
    public projectile projectile;

    oyuncuHareket oyuncuHareket;
    kameraSarsinti kameraSarsinti;
    public int okSayisi, komboSayaci;
    public GameObject silah1, silah2;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public RuntimeAnimatorController oyuncuAnimator;
    public Animator animator;
    public bool silahlarKilitli, hasarObjesiAktif, yumruk1, yumruk2, solTikTiklandi, sagTikTiklandi;
    public float sonHasar, sonSaldiriMenzili, beklemeSuresi, silah1DayanikliligiAzalmaMiktari, silah2DayanikliligiAzalmaMiktari, komboGecerlilikSuresi, animasyonSuresi;


    public silahOzellikleriniGetir silah1Script, silah2Script, yumrukScript;
    public silahUltileri silahUltileri;

    public Image silah1Image, silah2Image, silah1DayanikliligiImage, silah2DayanikliligiImage;
    public Sprite yumrukSprite;
    public yetenekKontrol yetenekKontrol;

    private void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silahUltileri = FindObjectOfType<silahUltileri>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
        silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num3Tusu")))
            alanHasariVer();

        silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
        silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;


        if (!silahlarKilitli)
        {
            if (solTikTiklandi || sagTikTiklandi)
                animator.SetBool("kosu", false);

            if (!oyuncuHareket.havada)
            {
                if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && silah1Script != null)
                {
                    silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();

                    if (silah1Script.silahAdi != "YUMRUK")
                        yumruk1 = false;

                    if (!yumruk1 && !solTikTiklandi)
                    {
                        if (hasarObjesiAktif)
                            sonHasar = silah1Script.silahSaldiriHasari * 2;
                        else
                            sonHasar = silah1Script.silahSaldiriHasari;

                        silah1Script.saldiriSesi.Play();

                        sonSaldiriMenzili = silah1Script.silahSaldiriMenzili;
                        animator.runtimeAnimatorController = silah1Script.karakterAnimator;
                        yakinSaldiri(silah1Script.silahDayanikliligi);
                    }
                }
                if ((Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu"))) && silah2Script != null)
                {
                    silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

                    if (silah2Script.silahAdi != "YUMRUK")
                        yumruk2 = false;

                    if (!yumruk2 && !sagTikTiklandi)
                    {
                        silah2Script.silahDayanikliligi -= silah2DayanikliligiAzalmaMiktari;

                        if (hasarObjesiAktif)
                            sonHasar = silah2Script.silahSaldiriHasari * 2;
                        else
                            sonHasar = silah2Script.silahSaldiriHasari;

                        sonSaldiriMenzili = silah2Script.silahSaldiriMenzili;
                        animator.runtimeAnimatorController = silah2Script.karakterAnimator;
                        menziliSaldiri(silah2Script.silahDayanikliligi);
                    }
                }
            }
            if (3 > komboSayaci && komboSayaci > 0)
            {
                komboGecerlilikSuresi -= Time.deltaTime;
                if (komboGecerlilikSuresi < 0)
                {
                    komboSayaci = 0;
                    komboGecerlilikSuresi = 3;
                }
            }
        }
        else
            Debug.Log("SÝLAHLAR KÝLÝTLÝ, ÖNCE SÝLAH KÝLÝDÝ BOOLUNU AÇ");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, silah1Script.silahSaldiriMenzili);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, silah2Script.silahSaldiriMenzili);
    }
    IEnumerator okZaman(float silahDayanikliligi)
    {
        animator.SetBool("hazirlanma", true);
        beklemeSuresi = silah2Script.animasyonClipleri[0].length;
        yield return new WaitForSeconds(beklemeSuresi);
        silah2Script.saldiriSesi.Play();
        if (transform.localScale.x == 1)
        {
            for (int i = 0; i < okSayisi; i++)
            {
                Instantiate(silah2Script.sagMenzilli, transform.position, silah2Script.sagMenzilli.transform.rotation);
            }
        }
        if (transform.localScale.x == -1)
        {
            for (int i = 0; i < okSayisi; i++)
            {
                Instantiate(silah2Script.solMenzilli, transform.position, silah2Script.solMenzilli.transform.rotation);
            }
        }
        animator.SetBool("hazirlanma", false);
        animator.SetBool("firlatma", true);
        beklemeSuresi = silah2Script.animasyonClipleri[1].length;
        yield return new WaitForSeconds(beklemeSuresi);
        sagTikTiklandi = false;
        oyuncuHareket.enabled = true;
        animator.SetBool("firlatma", false);
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;
        if (silahDayanikliligi <= 0)
        {
            silahUltileri.silah2Ulti = 0f;
            yumruk2 = true;
            animator.runtimeAnimatorController = oyuncuAnimator;
            silah2Script.silahSecimi.silahSec(yumrukScript.silahAdi.ToLower());
            silah2Script.silahOzellikleriniGuncelle();
            silah2Image.sprite = yumrukSprite;
            oyuncuHareket.enabled = true;
            oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
            oyuncuHareket.rb.freezeRotation = true;
        }
    }
    IEnumerator saldiriZaman()
    {
        komboSayaci++;
        if (komboSayaci == 1)
        {
            komboGecerlilikSuresi = 3f;
            animator.SetBool("saldiri1", true);
            beklemeSuresi = silah1Script.animasyonClipleri[0].length;
        }
        else if (komboSayaci == 2)
        {
            sonHasar = sonHasar * 1.25f;
            komboGecerlilikSuresi = 3f;
            animator.SetBool("saldiri2", true);
            beklemeSuresi = silah1Script.animasyonClipleri[1].length;
        }
        else if (komboSayaci == 3)
        {
            sonHasar = sonHasar * 1.5f;
            komboGecerlilikSuresi = 0f;
            komboSayaci = 0;
            kameraSarsinti.Shake();
            animator.SetBool("saldiri3", true);
            beklemeSuresi = silah1Script.animasyonClipleri[2].length;
        }
        yield return new WaitForSeconds(beklemeSuresi);
        solTikTiklandi = false;
        animator.SetBool("saldiri1", false);
        animator.SetBool("saldiri2", false);
        animator.SetBool("saldiri3", false);
        oyuncuHareket.enabled = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;
    }
    void yakinSaldiri(float silahDayanikliligi)
    {
        oyuncuHareket.enabled = false;
        solTikTiklandi = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasar, "silah1");
        }

        if (silahDayanikliligi <= 0)
        {
            solTikTiklandi = false;
            silahUltileri.silah1Ulti = 0f;
            yumruk1 = true;
            animator.runtimeAnimatorController = oyuncuAnimator;
            silah1Script.silahSecimi.silahSec(yumrukScript.silahAdi.ToLower());
            silah1Script.silahOzellikleriniGuncelle();
            silah1Image.sprite = yumrukSprite;
            oyuncuHareket.enabled = true;
            oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
            oyuncuHareket.rb.freezeRotation = true;
        }
        else
            StartCoroutine(saldiriZaman());
    }
    void menziliSaldiri(float silahDayanikliligi)
    {
        animator.SetBool("saldiriyor", true);
        animator.SetTrigger("saldiri");
        oyuncuHareket.enabled = false;
        sagTikTiklandi = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(okZaman(silahDayanikliligi));
    }

    public void alanHasariVer()
    {
        sonHasar = 10f;
        Collider2D[] dusmanlar = Physics2D.OverlapCircleAll(transform.position, 2f, dusmanLayer);
        foreach (Collider2D dusman in dusmanlar)
        {
            dusman.GetComponent<dusmanHasar>().hasarAl(sonHasar, "alanHasari");
        }
    }
}
