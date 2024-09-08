using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oyuncuSaldiriTest : MonoBehaviour
{
    public float beklemeSuresi, komboGecerlilikSuresi, animasyonSuresi, hannyaEtkisi, tutsuCanagiTimer, vurduTimer;
    public float bonusHasarlarYakin, bonusHasarlarMenzilli, sonHasarYakin, sonHasarMenzilli, sonSaldiriMenzili, kritikIhtimali, kritikHasari = 1.5f;
    public float silah1DayanikliligiAzalmaMiktari, silah2DayanikliligiAzalmaMiktari, silah1DayanikliligiBonus = 1f, silah2DayanikliligiBonus = 1f;
    oyuncuHareket oyuncuHareket;
    kameraSarsinti kameraSarsinti;
    public int okSayisi, komboSayaci;
    public GameObject silah1, silah2, yumruk, tutsuCanagi;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public RuntimeAnimatorController oyuncuAnimator;
    public Animator animator;
    public bool silahlarKilitli, hasarObjesiAktif, yumruk1, yumruk2, solTikTiklandi, sagTikTiklandi, vurdu, tempuraYedi, sashimiYedi;
    public Collider2D[] dusmanlar;
    public silahOzellikleriniGetir silah1Script, silah2Script, yumrukScript;
    public silahUltileri silahUltileri;
    public Image silah1Image, silah2Image, silah1DayanikliligiImage, silah2DayanikliligiImage;
    public Sprite yumrukSprite;
    public yetenekKontrol yetenekKontrol;
    public canKontrol canKontrol;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public AudioSource saldiriSes, silahKirildi;
    public silahOzellikleri yumrukSilah;
    bool sandikMi;

    private void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silahUltileri = FindObjectOfType<silahUltileri>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
    }
    private void Update()
    {
        tutsuCanagiVur();

        if (!canKontrol.oyuncuDead)
        {
            silah1Script = silah1.GetComponent<silahOzellikleriniGetir>();
            silah2Script = silah2.GetComponent<silahOzellikleriniGetir>();

            if (yumruk1)
            {
                silah1DayanikliligiImage.fillAmount = 0;
                silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;
                if (silah1Script.aciklamaKeyi != "yumruk_aciklama")
                    yumruk1 = false;
            }
            if (yumruk2)
            {
                silah2DayanikliligiImage.fillAmount = 0;
                silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
                if (silah2Script.aciklamaKeyi != "yumruk_aciklama")
                    yumruk2 = false;
            }
            else
            {
                silah1DayanikliligiImage.fillAmount = silah1Script.silahDayanikliligi / 100;
                silah2DayanikliligiImage.fillAmount = silah2Script.silahDayanikliligi / 100;
            }

            if (!silahlarKilitli)
            {
                if (solTikTiklandi || sagTikTiklandi)
                    animator.SetBool("kosu", false);

                if (!oyuncuHareket.havada && !oyuncuHareket.cakiliyor)
                {
                    if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && silah1Script != null && silah1Script.aciklamaKeyi != "yumruk_aciklama")
                    {
                        SolKlikSaldiri();
                        
                    }
                    if ((Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("sagTikTusu"))) && silah2Script != null && silah2Script.aciklamaKeyi != "yumruk_aciklama")
                    {
                        SagKlikSaldiri();
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
            if (vurdu)
            {
                if (oyuncuHareket.sagaBakiyor)
                    transform.Translate(Vector2.right * (1f * Time.deltaTime));
                else
                    transform.Translate(Vector2.left * (1f * Time.deltaTime));
                vurduTimer += Time.deltaTime;
                if (vurduTimer > 0.15)
                {
                    vurduTimer = 0f;
                    vurdu = false;
                }
            }
        }
    }
    // ------------------------------- YAKIN SALDIRI ------------------------------- YAKIN SALDIRI ------------------------------- YAKIN SALDIRI -------------------------------
    void yakinSaldiri()
    {
        oyuncuHareket.enabled = false;
        solTikTiklandi = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(saldiriZaman());
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

        sonHasarYakin = silah1Script.silahSaldiriHasari * bonusHasarlarYakin * hannyaEtkisi;

        if (hasarObjesiAktif)
            sonHasarYakin *= 2;

        float randomSayi = Random.Range(0, 100);
        if (kritikIhtimali > randomSayi)
        {
            Debug.Log("kritik vurdu");
            sonHasarYakin *= kritikHasari;
        }
        vurdu = true;
        komboSayaci++;
        if (komboSayaci == 1)
        {
            saldiriSes.clip = silah1Script.saldiriSesi[0];
            if (silah1Script.silahAdi != "Tetsubo")
                saldiriSes.Play();
            komboGecerlilikSuresi = 3f;
            animator.SetBool("saldiri1", true);
            if (silah1Script.silahAdi == "Tetsubo")
                beklemeSuresi = silah1Script.animasyonClipleri[0].length;
            else
                beklemeSuresi = silah1Script.animasyonClipleri[0].length;
        }
        else if (komboSayaci == 2)
        {
            saldiriSes.clip = silah1Script.saldiriSesi[1];
            if (silah1Script.silahAdi != "Tetsubo")
                saldiriSes.Play();
            sonHasarYakin = sonHasarYakin * 1.25f;
            komboGecerlilikSuresi = 3f;
            animator.SetBool("saldiri2", true);
            if (silah1Script.silahAdi == "Tetsubo")
                beklemeSuresi = silah1Script.animasyonClipleri[1].length * 1.5f;
            else
                beklemeSuresi = silah1Script.animasyonClipleri[1].length;
        }
        else if (komboSayaci == 3)
        {
            saldiriSes.clip = silah1Script.saldiriSesi[2];
            if (silah1Script.silahAdi != "Tetsubo")
                saldiriSes.Play();
            sonHasarYakin = sonHasarYakin * 1.5f;
            komboGecerlilikSuresi = 0f;
            komboSayaci = 0;
            animator.SetBool("saldiri3", true);
            if (silah1Script.silahAdi == "Tetsubo")
                beklemeSuresi = silah1Script.animasyonClipleri[2].length;
            else
                beklemeSuresi = silah1Script.animasyonClipleri[2].length;
        }
        if (silah1Script.silahAdi == "Tetsubo")
        {
            yield return new WaitForSeconds(beklemeSuresi / 1.85f);
            saldiriSes.Play();
        }
        if (komboSayaci == 3) kameraSarsinti.Shake();

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].GetComponent<dusmanHasar>() != null)
                enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasarYakin, "silah1");
        }

        if (silah1Script.silahAdi == "Tetsubo")
            yield return new WaitForSeconds(beklemeSuresi / 1.25f);
        if (silah1Script.silahAdi != "Tetsubo")
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
            silah1Script.elindekiSilah = yumrukSilah;
            silah1Script.seciliSilahinBilgileriniGetir();
            silah1Image.sprite = yumrukSprite;
            oyuncuHareket.enabled = true;
            oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
            oyuncuHareket.rb.freezeRotation = true;
        }
    }
    // ------------------------------- YAKIN SALDIRI ------------------------------- YAKIN SALDIRI ------------------------------- YAKIN SALDIRI -------------------------------


    // ------------------------------- MENZİLLİ SALDIRI ------------------------------- MENZİLLİ SALDIRI ------------------------------- MENZİLLİ SALDIRI -------------------------------
    void menziliSaldiri()
    {
        oyuncuHareket.enabled = false;
        sagTikTiklandi = true;
        oyuncuHareket.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(okZaman());
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

        sonHasarMenzilli = silah2Script.silahSaldiriHasari * bonusHasarlarMenzilli * hannyaEtkisi;

        if (hasarObjesiAktif)
            sonHasarMenzilli *= 2;

        float randomSayi = Random.Range(0, 100);
        if (kritikIhtimali >= randomSayi)
        {
            Debug.Log("kritik vurdu");
            sonHasarMenzilli *= kritikHasari;
        }

        animator.SetBool("hazirlanma", true);
        yield return new WaitForSeconds(silah2Script.animasyonClipleri[0].length);
        saldiriSes.clip = silah2Script.saldiriSesi[0];
        saldiriSes.Play();
        animator.SetBool("hazirlanma", false);
        animator.SetBool("firlatma", true);
        if (silah2Script.aciklamaKeyi == "arbalet_aciklama")
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.15f);
                if (transform.localScale.x == 1)
                    Instantiate(silah2Script.sagMenzilli, transform.position, silah2Script.sagMenzilli.transform.rotation);
                if (transform.localScale.x == -1)
                    Instantiate(silah2Script.solMenzilli, transform.position, silah2Script.solMenzilli.transform.rotation);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(silah2Script.animasyonClipleri[1].length - 0.5f);
        }
        else
        {
            if (transform.localScale.x == 1)
                Instantiate(silah2Script.sagMenzilli, transform.position, silah2Script.sagMenzilli.transform.rotation);
            if (transform.localScale.x == -1)
                Instantiate(silah2Script.solMenzilli, transform.position, silah2Script.solMenzilli.transform.rotation);
            yield return new WaitForSeconds(silah2Script.animasyonClipleri[1].length);
        }

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
            silah2Script.elindekiSilah = yumrukSilah;
            silah2Script.seciliSilahinBilgileriniGetir();
            silah2Image.sprite = yumrukSprite;
            oyuncuHareket.enabled = true;
            oyuncuHareket.rb.constraints = RigidbodyConstraints2D.None;
            oyuncuHareket.rb.freezeRotation = true;
        }
    }
    // ------------------------------- MENZİLLİ SALDIRI ------------------------------- MENZİLLİ SALDIRI ------------------------------- MENZİLLİ SALDIRI -------------------------------


    // ------------------------------- ALAN HASARI ------------------------------- ALAN HASARI ------------------------------- ALAN HASARI -------------------------------
    public void alanHasariVer()
    {
        sandikMi = false;
        dusmanlar = Physics2D.OverlapCircleAll(transform.position, 2f, dusmanLayer);

        HashSet<Collider2D> benzersizDusmanlar = new HashSet<Collider2D>();

        foreach (Collider2D dusman in dusmanlar)
        {
            if (dusman.GetComponent<iksirCikarmaScripti>() != null)
            {
                dusman.GetComponent<iksirCikarmaScripti>().sandikAcma();
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
    // ------------------------------- ALAN HASARI ------------------------------- ALAN HASARI ------------------------------- ALAN HASARI

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, silah1Script.silahSaldiriMenzili);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, silah2Script.silahSaldiriMenzili);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    public void tutsuCanagiVur()
    {
        if (antikaYadigarKontrol.hangiAntikaAktif[4])
        {
            tutsuCanagi.SetActive(true);
            tutsuCanagiTimer += Time.deltaTime;
            if (tutsuCanagiTimer > 3)
            {
                Debug.Log("tutsuCanagi Vurdu");
                tutsuCanagiTimer = 0;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, 3f, dusmanLayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].name != "zeminkontrol")
                        enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(5, "tutsuCanagi");
                }
            }
        }
        else
            if (tutsuCanagi != null) tutsuCanagi.SetActive(false);
    }
    public void SolKlikSaldiri()
    {
        if (!yumruk1 && !solTikTiklandi && !sagTikTiklandi)
        {
            sonSaldiriMenzili = silah1Script.silahSaldiriMenzili;
            animator.runtimeAnimatorController = silah1Script.karakterAnimator;
            yakinSaldiri();
        }
    }
    public void SagKlikSaldiri()
    {

    }
}
