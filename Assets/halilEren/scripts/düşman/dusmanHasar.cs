using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dusmanHasar : MonoBehaviour
{
    killSayaci killSayaci;
    public GameObject killEfekt;

    public GameObject sesler;
    public Image hpBar;
    public dusmanUi dusmanUi;

    public GameObject buz, zehir, okVurulmaSesi, aniPuaniObje, ejderParasi, kanPartikül, kanPartikülDuvar, hasarRapor, hasarRaporObje, kesilmeSesi, saplanmaSesi;
    public bool agresif, yumi, zehirleniyor, havaiFisekPatlamasi, donuyor;
    public bool arkasiDuvar;
    public float can, zehirTimer, aniPuaniIhtimali;
    BoxCollider2D boxCollider;
    Animator animator;
    Rigidbody2D rb;
    GameObject oyuncu;
    dusmanAgresif dusmanAgresif;
    dusmanYumi dusmanYumi;
    dusmanHareket dusmanHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    silahUltileri silahUltileri;
    envanterKontrol envanterKontrol;
    rastgeleSilahDusurmeScripti rastgeleSilahDusurmeScripti;
    hasarRaporu hasarRaporu;

    void Start()
    {
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
    }
    private void Update()
    {
        Zehir();
    }
    void Olum()
    {
        if (can <= 0)
        {
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
            if (aniPuaniIhtimali > 75)
            {
                Instantiate(aniPuaniObje, transform.position, Quaternion.identity);
                envanterKontrol.aniArttir(1);
            }

            boxCollider.enabled = false;
            Instantiate(ejderParasi, transform.position, Quaternion.identity);
            rastgeleSilahDusurmeScripti.silahiDusur(75, 0, 100); // dusme ihtimali, min ihtimal, max ihtimal

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
    void Zehir()
    {
        if (zehirleniyor)
        {
            zehirTimer += Time.deltaTime;
            if (zehirTimer >= 0.5f)
            {
                hasarAl(15, "zehir");
                zehirTimer = 0;
            }
        }
    }

    public void hasarAl(float saldiri, string hangiObje)
    {
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
                rb.AddForce(transform.right * 15, ForceMode2D.Impulse);
                rb.AddForce(transform.up * 20, ForceMode2D.Impulse);
            }
            if (oyuncu.transform.position.x > transform.position.x)
            {
                rb.AddForce(transform.right * -15, ForceMode2D.Impulse);
                rb.AddForce(transform.up * 20, ForceMode2D.Impulse);
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
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("havaiFisek"))
        {
            hasarAl(500, "havaiFisek");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("buz"))
        {
            donuyor = true;
            buz.SetActive(true);
            animator.enabled = false;

            if (agresif)
                dusmanAgresif.enabled = false;
            if (yumi)
                dusmanYumi.enabled = false;

            dusmanHareket.enabled = false;
        }
        if (collision.gameObject.CompareTag("zehir"))
        {
            zehir.SetActive(true);
            zehirleniyor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("buz"))
        {
            if (can > 0)
            {
                if (agresif)
                    dusmanAgresif.enabled = true;
                if (yumi)
                    dusmanYumi.enabled = true;
            }
            donuyor = false;
            buz.SetActive(false);
            animator.enabled = true;

            if (can > 0)
                dusmanHareket.enabled = true;
        }
        if (collision.gameObject.CompareTag("zehir"))
        {
            if (this == enabled)
                StartCoroutine(zehirdenCikti());
        }
    }
    IEnumerator zehirdenCikti()
    {
        yield return new WaitForSeconds(1);
        zehir.SetActive(false);
        zehirleniyor = false;
        zehirTimer = 0;
    }
}

