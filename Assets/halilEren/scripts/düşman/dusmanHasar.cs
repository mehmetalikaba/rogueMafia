using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dusmanHasar : MonoBehaviour
{
    GameObject oyuncuAnimator;

    public Image hpBar;
    public dusmanUi dusmanUi;
    public GameObject[] etmenler;
    public GameObject killEfekt, sesler, okVurulmaSesi, aniPuaniObje, ejderParasi, kanPartikül, kanPartikülDuvar, hasarRapor, hasarRaporObje, kesilmeSesi, saplanmaSesi;
    public bool agresif, yumi, zehirleniyor, kaniyor, yaniyor, sersemliyor, havaiFisekPatlamasi, donuyor, antika3, antika6;
    public bool arkasiDuvar;
    public float can, aniPuaniIhtimali, canCalmaIhtimali, ilkKritik;
    public float buzTimer, buzSayac, buzSure, zehirTimer, zehirSayac, zehirSure, kaniyorTimer, kaniyorSayac, kaniyorSure, yaniyorTimer, yaniyorSayac, yaniyorSure, sersemliyorTimer, sersemliyorSayac, sersemliyorSure;

    Rigidbody2D rb;
    Animator animator;
    GameObject oyuncu;
    dusmanYumi dusmanYumi;
    canKontrol canKontrol;
    killSayaci killSayaci;
    hasarRaporu hasarRaporu;
    BoxCollider2D boxCollider;
    silahUltileri silahUltileri;
    dusmanHareket dusmanHareket;
    dusmanAgresif dusmanAgresif;
    envanterKontrol envanterKontrol;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    ozelEtkilerKontrol ozelEtkilerKontrol;
    antikaYadigarKontrol antikaYadigarKontrol;
    rastgeleSilahDusurmeScripti rastgeleSilahDusurmeScripti;
    rastgeleYadigarDusurmeScripti rastgeleYadigarDusurmeScripti;

    void Start()
    {
        oyuncuAnimator = GameObject.Find("oyuncuAnimator");

        killSayaci = FindObjectOfType<killSayaci>();

        dusmanHareket = GetComponent<dusmanHareket>();

        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

        if (agresif)
            dusmanAgresif = GetComponent<dusmanAgresif>();
        if (yumi)
            dusmanYumi = GetComponent<dusmanYumi>();

        silahUltileri = FindObjectOfType<silahUltileri>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        rastgeleSilahDusurmeScripti = GetComponent<rastgeleSilahDusurmeScripti>();
        rastgeleYadigarDusurmeScripti = GetComponent<rastgeleYadigarDusurmeScripti>();
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
        canKontrol = FindObjectOfType<canKontrol>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();

        buzSure = 3f;
        zehirSure = 3.5f;
        kaniyorSure = 2.5f;
        yaniyorSure = 4f;
        sersemliyorSure = 1f;
        if (antikaYadigarKontrol.hangiAntikaAktif[1])
            antika3 = true;
        if (antikaYadigarKontrol.hangiAntikaAktif[2])
            antika6 = true;

        ilkKritik = oyuncuSaldiriTest.kritikIhtimali;

    }
    private void Update()
    {
        if (can > 0)
        {
            buz();
            zehir();
            kanama();
            yanma();
            sersemleme();
        }
        else
        {
            donuyor = false;
            zehirleniyor = false;
            kaniyor = false;
            yaniyor = false;
            sersemliyor = false;
        }

    }
    void Olum()
    {
        if (can <= 0)
        {
            if (agresif)
                dusmanAgresif.saldiriAlan = 0f;
            if (yumi)
            {
                dusmanYumi.okFirlat = false;
                dusmanYumi.suAndaOkAtiyor = false;
                dusmanYumi.atiyor = true;
                dusmanYumi.sagaOk = null;
                dusmanYumi.solaOk = null;
            }


            killSayaci.oldurmeSayisi++;
            killSayaci.yazdir();
            Instantiate(killEfekt, transform.position, Quaternion.identity);

            Destroy(hpBar.gameObject);

            if (!havaiFisekPatlamasi)
            {
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }

            aniPuaniIhtimali = Random.Range(0, 100);
            if (aniPuaniIhtimali < 25)
            {
                Instantiate(aniPuaniObje, transform.position, Quaternion.identity);
                envanterKontrol.aniArttir(1);
            }
            Instantiate(ejderParasi, transform.position, Quaternion.identity);
            rastgeleSilahDusurmeScripti.silahiDusur(75, 0, 100); // dusme ihtimali, min ihtimal, max ihtimal
            rastgeleYadigarDusurmeScripti.yadigarDusurme();

            boxCollider.enabled = false;
            animator.SetBool("yurume", false);
            animator.SetBool("olum", true);

            if (agresif)
                dusmanAgresif.enabled = false;
            if (yumi)
                dusmanYumi.enabled = false;

            dusmanHareket.enabled = false;
            sesler.SetActive(false);
            this.enabled = false;
            Destroy(gameObject, 1);
        }
    }
    void buz()
    {
        if (donuyor)
        {
            etmenler[0].SetActive(true);
            animator.enabled = false;
            if (agresif)
                dusmanAgresif.enabled = false;
            if (yumi)
                dusmanYumi.enabled = false;
            dusmanHareket.enabled = false;

            buzSayac += Time.deltaTime;
            if (buzSayac > buzSure)
            {
                etmenler[0].SetActive(false);
                donuyor = false;
                buzSayac = 0;
                if (agresif)
                    dusmanAgresif.enabled = true;
                if (yumi)
                    dusmanYumi.enabled = true;
                animator.enabled = true;
                dusmanHareket.enabled = true;
            }
        }
    }
    void zehir()
    {
        if (zehirleniyor)
        {
            etmenler[1].SetActive(true);
            zehirSayac += Time.deltaTime;
            if (zehirSayac > zehirSure)
            {
                StartCoroutine(zehirdenCikti());
                zehirSayac = 0;
            }

            zehirTimer += Time.deltaTime;
            if (zehirTimer >= 1f)
            {
                hasarAl(10, "zehir");
                zehirTimer = 0;
            }
        }
    }
    IEnumerator zehirdenCikti()
    {
        yield return new WaitForSeconds(1);
        etmenler[1].SetActive(false);
        zehirleniyor = false;
        zehirTimer = 0;
    }
    void kanama()
    {
        if (kaniyor)
        {
            etmenler[2].SetActive(true);
            kaniyorSayac += Time.deltaTime;
            if (kaniyorSayac > kaniyorSure)
            {
                etmenler[2].SetActive(false);
                kaniyor = false;
                kaniyorSayac = 0;
            }

            kaniyorTimer += Time.deltaTime;
            if (kaniyorTimer >= 1f)
            {
                hasarAl(10, "kanama");
                kaniyorTimer = 0;
            }
        }
    }
    void yanma()
    {
        if (yaniyor)
        {
            etmenler[3].SetActive(true);
            yaniyorSayac += Time.deltaTime;
            if (yaniyorSayac > yaniyorSure)
            {
                etmenler[3].SetActive(false);
                yaniyor = false;
                yaniyorSayac = 0;
            }

            yaniyorTimer += Time.deltaTime;
            if (yaniyorTimer >= 1f)
            {
                hasarAl(10, "yanma");
                yaniyorTimer = 0;
            }
        }
    }
    void sersemleme()
    {
        if (sersemliyor)
        {
            oyuncuSaldiriTest.kritikIhtimali = 100f;
            etmenler[4].SetActive(true);

            animator.enabled = false;
            if (agresif)
                dusmanAgresif.enabled = false;
            if (yumi)
                dusmanYumi.enabled = false;
            dusmanHareket.enabled = false;

            sersemliyorSayac += Time.deltaTime;
            if (sersemliyorSayac > sersemliyorSure)
            {
                oyuncuSaldiriTest.kritikIhtimali = ilkKritik;
                etmenler[4].SetActive(false);
                sersemliyor = false;
                sersemliyorSayac = 0;

                if (agresif)
                    dusmanAgresif.enabled = true;
                if (yumi)
                    dusmanYumi.enabled = true;
                animator.enabled = true;
                dusmanHareket.enabled = true;
            }
        }
    }


    public void hasarAl(float saldiri, string hangiObje)
    {
        /*Animator oAnimator;
        oAnimator = oyuncuAnimator.GetComponent<Animator>();
        oAnimator.StopPlayback();*/

        if (hangiObje == "silah1")
        {
            //Debug.Log("silah1 vurdu");
            oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari = oyuncuSaldiriTest.silah1Script.silahDayanikliligiAzalmaMiktari;
            oyuncuSaldiriTest.silah1Script.silahDayanikliligi -= oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari + oyuncuSaldiriTest.silah1DayanikliligiBonus;

            if (!silahUltileri.silah1UltiAcik)
                silahUltileri.silah1Ulti += 5;

            Instantiate(kesilmeSesi, transform.position, Quaternion.identity);
        }
        else if (hangiObje == "silah2")
        {
            if (antika3)
                donuyor = true;
            if (antika6)
            {
                // BURADA DUSMANIN BULUNDUGU KONUMDA BIR ALAN HASARI OLUSMALI, DUSMAN SALDIRIYI KURCALAMADIM ORADAKI BIRTAKIM SISTEMLER ILE BU HALLEDILIR
                /*Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, sonSaldiriMenzili, dusmanLayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].name != "zeminkontrol")
                        enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(sonHasarYakin, "silah1");
                }*/
                // BURADA DUSMANIN BULUNDUGU KONUMDA BIR ALAN HASARI OLUSMALI, DUSMAN SALDIRIYI KURCALAMADIM ORADAKI BIRTAKIM SISTEMLER ILE BU HALLEDILIR
            }

            //Debug.Log("silah2 vurdu");
            Instantiate(okVurulmaSesi, transform.position, Quaternion.identity);

            if (!silahUltileri.silah2UltiAcik)
                silahUltileri.silah2Ulti += 5;

            Instantiate(saplanmaSesi, transform.position, Quaternion.identity);
        }
        else if (hangiObje == "alanHasari")
        {
            //Debug.Log("alan hasari vurdu");
        }
        else if (hangiObje == "zehir")
        {
            //Debug.Log("zehir vurdu");
        }
        else if (hangiObje == "kanama")
        {
            //Debug.Log("kanama vurdu");
        }
        else if (hangiObje == "yanma")
        {
            //Debug.Log("yanma vurdu");
        }
        else if (hangiObje == "kunai")
        {
            //Debug.Log("kunai vurdu");
        }
        else if (hangiObje == "shuriken")
        {
            //Debug.Log("shuriken vurdu");
            Instantiate(okVurulmaSesi, transform.position, Quaternion.identity);

        }
        else if (hangiObje == "havaiFisek")
        {
            //Debug.Log("havaiFisek vurdu");
            havaiFisekPatlamasi = true;
            if (oyuncu.transform.position.x <= transform.position.x)
            {
                /*rb.AddForce(transform.right * 15, ForceMode2D.Impulse);
                rb.AddForce(transform.up * 20, ForceMode2D.Impulse);*/
            }
            if (oyuncu.transform.position.x > transform.position.x)
            {
               /* rb.AddForce(transform.right * -15, ForceMode2D.Impulse);
                rb.AddForce(transform.up * 20, ForceMode2D.Impulse);*/
            }
        }

        dusmanUi.gorunur();
        //kameraSarsinti.Shake();

        if (arkasiDuvar)
            Instantiate(kanPartikülDuvar, transform.position, Quaternion.identity);

        hasarRaporObje = Instantiate(hasarRapor, transform.position, Quaternion.identity);
        hasarRaporu = hasarRaporObje.GetComponent<hasarRaporu>();
        hasarRaporu.alinanHasar = saldiri;

        Instantiate(kanPartikül, transform.position, Quaternion.identity);

        // CAN CALMA KODLARI BURADA IHTIMALI SU AN %25  // ------------------------- CAN CALMA KODLARI // CAN CALMA KODLARI -------------------------
        int randomNumara = Random.Range(0, 100);
        if (randomNumara < canCalmaIhtimali)
            canKontrol.can += 15;
        // CAN CALMA KODLARI BURADA IHTIMALI SU AN %25  // ------------------------- CAN CALMA KODLARI // CAN CALMA KODLARI ------------------------- 

        can -= saldiri;
        hpBar.fillAmount -= saldiri / 100;
        Olum();


        /*if (oyuncu.transform.position.x <= transform.position.x)
        {
            rb.velocity = Vector2.right * 3f;
        }
        if (oyuncu.transform.position.x > transform.position.x)
        {
            rb.velocity = Vector2.right * -3f;
        }*/

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("kunai"))
        {

            Instantiate(saplanmaSesi, transform.position, Quaternion.identity);

            hasarAl(500, "kunai");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("shuriken"))
        {
            Instantiate(saplanmaSesi, transform.position, Quaternion.identity);

            hasarAl(20, "shuriken");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("ok"))
        {
            Instantiate(saplanmaSesi, transform.position, Quaternion.identity);

            hasarAl(oyuncuSaldiriTest.sonHasarMenzilli, "silah2");
            // EGER OYUNCU YEMEK YEDIYSE OK BIRINCI DUSMANA CARPINCA DEGIL SONRAKI DUSMANDA YOK OLUR NORMALDE ISE ILK DUSMANDA DIREKT YOK OLUR // ----------------------
            if (ozelEtkilerKontrol.yemekEtkileri[10])
            {
                Debug.Log(collision.gameObject.name);
                if (collision.gameObject.GetComponent<projectile>().kacDusman == 0)
                    collision.gameObject.GetComponent<projectile>().kacDusman++;
                else
                    Destroy(collision.gameObject);
            }
            else
                Destroy(collision.gameObject);
            // EGER OYUNCU YEMEK YEDIYSE OK BIRINCI DUSMANA CARPINCA DEGIL SONRAKI DUSMANDA YOK OLUR NORMALDE ISE ILK DUSMANDA DIREKT YOK OLUR // ----------------------
        }
        if (collision.gameObject.CompareTag("havaiFisek"))
        {
            hasarAl(500, "havaiFisek");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("buz"))
        {
            buzSayac = 0;
            donuyor = true;
        }
        if (collision.gameObject.CompareTag("zehir"))
        {
            zehirSayac = 0;
            zehirleniyor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("buz"))
        {
            buzSayac = buzSure;
            if (agresif)
                dusmanAgresif.enabled = true;
            if (yumi)
                dusmanYumi.enabled = true;
            animator.enabled = true;
            dusmanHareket.enabled = true;
            donuyor = false;
        }
        if (collision.gameObject.CompareTag("zehir"))
        {
            StartCoroutine(zehirdenCikti());
            zehirSayac = zehirSure;
        }
    }
}

