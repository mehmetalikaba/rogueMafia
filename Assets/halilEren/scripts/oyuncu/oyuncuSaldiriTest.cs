using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oyuncuSaldiriTest : MonoBehaviour
{
    public projectile projectile;

    oyuncuHareket oyuncuHareket;
    kameraSarsinti kameraSarsinti;
    public int okSayisi, komboSayaci;
    public GameObject silah1, silah2, yumruk;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public RuntimeAnimatorController oyuncuAnimator;
    public Animator animator;
    public bool silahlarKilitli, hasarObjesiAktif, yumruk1, yumruk2, solTikTiklandi, sagTikTiklandi, tempuraYedi, sashimiYedi;
    public float bonusHasarlarYakin, bonusHasarlarMenzilli, sonHasarYakin, sonHasarMenzilli, sonSaldiriMenzili, beklemeSuresi, silah1DayanikliligiAzalmaMiktari, silah2DayanikliligiAzalmaMiktari, komboGecerlilikSuresi, animasyonSuresi, kritikIhtimali, kritikHasari = 1.5f, silah1DayanikliligiBonus = 1f, silah2DayanikliligiBonus = 1f;
    public Collider2D[] dusmanlar;

    public silahOzellikleriniGetir silah1Script, silah2Script, yumrukScript;
    public silahUltileri silahUltileri;

    public Image silah1Image, silah2Image, silah1DayanikliligiImage, silah2DayanikliligiImage;
    public Sprite yumrukSprite;
    public yetenekKontrol yetenekKontrol;
    public AudioSource saldiriSes, silahKirildi;

    bool sandikMi;

    private void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silahUltileri = FindObjectOfType<silahUltileri>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
        yumruk = GameObject.Find("yumruk");
        

    }
    private void Update()
    {
        silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

        if (yumruk1)
        {
            silah1DayanikliligiImage.fillAmount = 0;
            silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;
            yumruk.SetActive(true);
            if (silah1Script.silahAdi != "YUMRUK")
                yumruk1 = false;
        }
        if (yumruk2)
        {
            silah2DayanikliligiImage.fillAmount = 0;
            silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
            yumruk.SetActive(true);
            if (silah2Script.silahAdi != "YUMRUK")
                yumruk2 = false;
        }
        else
        {
            yumruk.SetActive(false);
            silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
            silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;
        }

        if (!silahlarKilitli)
        {
            if (solTikTiklandi || sagTikTiklandi)
                animator.SetBool("kosu", false);

            if (!oyuncuHareket.havada && !oyuncuHareket.cakiliyor)
            {
                if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && silah1Script != null)
                {
                    if (!yumruk1 && !solTikTiklandi && !sagTikTiklandi)
                    {
                        sonSaldiriMenzili = silah1Script.silahSaldiriMenzili;
                        animator.runtimeAnimatorController = silah1Script.karakterAnimator;
                        yakinSaldiri();
                    }
                }
                if ((Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu"))) && silah2Script != null)
                {
                    if (!yumruk2 && !sagTikTiklandi && !solTikTiklandi)
                    {
                        silah2DayanikliligiAzalmaMiktari = silah2Script.silahDayanikliligiAzalmaMiktari;
                        silah2Script.silahDayanikliligi -= silah2DayanikliligiAzalmaMiktari / silah2DayanikliligiBonus;

                        sonSaldiriMenzili = silah2Script.silahSaldiriMenzili;
                        animator.runtimeAnimatorController = silah2Script.karakterAnimator;
                        menziliSaldiri();
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
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, silah1Script.silahSaldiriMenzili);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, silah2Script.silahSaldiriMenzili);
    }
    void yakinSaldiri()
    {
        oyuncuHareket.enabled = false;
        solTikTiklandi = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(saldiriZaman());
    }
    void menziliSaldiri()
    {
        animator.SetBool("saldiriyor", true);
        animator.SetTrigger("saldiri");
        oyuncuHareket.enabled = false;
        sagTikTiklandi = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(okZaman());
    }
    IEnumerator saldiriZaman()
    {
        if (!tempuraYedi)
        {
            kritikHasari = 1f;
            kritikIhtimali = 0f;
        }
        if (!sashimiYedi)
        {
            bonusHasarlarYakin = 1f;
        }

        sonHasarYakin = silah1Script.silahSaldiriHasari * bonusHasarlarYakin;

        if (hasarObjesiAktif)
            sonHasarYakin *= 2;

        float randomSayi = Random.Range(0, 100);
        if (kritikIhtimali > randomSayi)
        {
            Debug.Log("kritik vurdu");
            sonHasarYakin *= kritikHasari;
        }

        komboSayaci++;
        if (komboSayaci == 1)
        {
            saldiriSes.clip = silah1Script.saldiriSesi[0];
            saldiriSes.Play();
            komboGecerlilikSuresi = 3f;
            animator.SetBool("saldiri1", true);
            beklemeSuresi = silah1Script.animasyonClipleri[0].length;
        }
        else if (komboSayaci == 2)
        {
            saldiriSes.clip = silah1Script.saldiriSesi[1];
            saldiriSes.Play();
            sonHasarYakin = sonHasarYakin * 1.25f;
            komboGecerlilikSuresi = 3f;
            animator.SetBool("saldiri2", true);
            beklemeSuresi = silah1Script.animasyonClipleri[1].length;
        }
        else if (komboSayaci == 3)
        {
            saldiriSes.clip = silah1Script.saldiriSesi[2];
            saldiriSes.Play();
            sonHasarYakin = sonHasarYakin * 1.5f;
            komboGecerlilikSuresi = 0f;
            komboSayaci = 0;
            kameraSarsinti.Shake();
            animator.SetBool("saldiri3", true);
            beklemeSuresi = silah1Script.animasyonClipleri[2].length;
        }

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].name != "zeminkontrol")
                enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasarYakin, "silah1");
        }

        yield return new WaitForSeconds(beklemeSuresi);
        solTikTiklandi = false;
        animator.SetBool("saldiri1", false);
        animator.SetBool("saldiri2", false);
        animator.SetBool("saldiri3", false);
        oyuncuHareket.enabled = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
        oyuncuHareket.rb.freezeRotation = true;

        if (silah1Script.silahDayanikliligi <= 0)
        {
            silahKirildi.Play();
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
    }
    IEnumerator okZaman()
    {
        if (!tempuraYedi)
        {
            kritikHasari = 1f;
            kritikIhtimali = 0f;
        }
        if (!sashimiYedi)
        {
            bonusHasarlarMenzilli = 1f;
        }

        sonHasarMenzilli = silah2Script.silahSaldiriHasari * bonusHasarlarMenzilli;

        if (hasarObjesiAktif)
            sonHasarMenzilli *= 2;

        float randomSayi = Random.Range(0, 100);
        if (kritikIhtimali >= randomSayi)
        {
            Debug.Log("kritik vurdu");
            sonHasarMenzilli *= kritikHasari;
        }

        animator.SetBool("hazirlanma", true);
        beklemeSuresi = silah2Script.animasyonClipleri[0].length;
        yield return new WaitForSeconds(beklemeSuresi);
        saldiriSes.clip = silah2Script.saldiriSesi[0];
        saldiriSes.Play();

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
        if (silah2Script.silahDayanikliligi <= 0)
        {
            silahKirildi.Play();
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
    public void alanHasariVer()
    {
        sandikMi = false;
        dusmanlar = Physics2D.OverlapCircleAll(transform.position, 2f, dusmanLayer);

        HashSet<Collider2D> benzersizDusmanlar = new HashSet<Collider2D>();

        foreach (Collider2D dusman in dusmanlar)
        {
            if (dusman.name == "kutu")
            {
                dusman.GetComponent<toplanabilirSecmeScripti>().sandikAcildi();
                sandikMi = true;
            }
            if (!sandikMi)
            {
                if (dusman.name != "zeminkontrol")
                {
                    benzersizDusmanlar.Add(dusman);
                }
            }
        }
        if (!sandikMi)
        {
            foreach (Collider2D dusman in benzersizDusmanlar)
            {
                dusman.GetComponent<dusmanHasar>().hasarAl(25, "alanHasari");
            }
        }
    }
}
