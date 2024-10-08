﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class dusmanHasar : MonoBehaviour
{
    Gamepad gamepad;
    bool oyuncuVurdu;
    float vurduTimer;

    public Image hpBar;
    public dusmanUi dusmanUi;
    public GameObject[] etmenler;
    public GameObject patlayanOk, digerFici, barutFicisi, killEfekt, sesler, okVurulmaSesi, aniPuaniObje, ejderParasi, kanPartikül, kanPartikülDuvar, hasarRapor, hasarRaporObje, kesilmeSesi, saplanmaSesi;
    public bool yakin, menzilli, zehirleniyor, kaniyor, yaniyor, sersemliyor, havaiFisekPatlamasi, donuyor, antika3;
    public bool arkasiDuvar;
    public float can, aniPuaniIhtimali, canCalmaIhtimali, ilkKritik, arbaletSayac, arbaletTimer;
    public float buzTimer, buzSayac, buzSure, zehirTimer, zehirSayac, zehirSure, kaniyorTimer, kaniyorSayac, kaniyorSure, yaniyorTimer, yaniyorSayac, yaniyorSure, sersemliyorTimer, sersemliyorSayac, sersemliyorSure;

    dusman dusman;
    Rigidbody2D rb;
    GameObject oyuncu;
    flashHasar flashHasar;
    canKontrol canKontrol;
    killSayaci killSayaci;
    hasarRaporu hasarRaporu;
    GameObject oyuncuAnimator;
    BoxCollider2D boxCollider;
    silahUltileri silahUltileri;
    public dusmanSaldiri dusmanSaldiri;
    oyuncuHareket oyuncuHareket;
    envanterKontrol envanterKontrol;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    ozelEtkilerKontrol ozelEtkilerKontrol;
    antikaYadigarKontrol antikaYadigarKontrol;
    rastgeleSilahDusurmeScripti rastgeleSilahDusurmeScripti;
    rastgeleYadigarDusurmeScripti rastgeleYadigarDusurmeScripti;

    void Start()
    {
        gamepad = Gamepad.current;

        dusman = GetComponent<dusman>();
        rb = GetComponent<Rigidbody2D>();
        flashHasar = GetComponent<flashHasar>();
        canKontrol = FindObjectOfType<canKontrol>();
        killSayaci = FindObjectOfType<killSayaci>();
        boxCollider = GetComponent<BoxCollider2D>();
        dusmanSaldiri = GetComponent<dusmanSaldiri>();
        silahUltileri = FindObjectOfType<silahUltileri>();
        oyuncuAnimator = GameObject.Find("oyuncuAnimator");
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        rastgeleSilahDusurmeScripti = GetComponent<rastgeleSilahDusurmeScripti>();
        rastgeleYadigarDusurmeScripti = GetComponent<rastgeleYadigarDusurmeScripti>();

        buzSure = 3f;
        zehirSure = 3.5f;
        kaniyorSure = 2.5f;
        yaniyorSure = 4f;
        sersemliyorSure = 2.5f;
        /* if (antikaYadigarKontrol.hangiAntikaAktif[1])
             antika3 = true;*/

        ilkKritik = oyuncuSaldiriTest.kritikIhtimali;
    }
    private void FixedUpdate()
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
        if (oyuncuVurdu)
        {
            gamepad.SetMotorSpeeds(0.3f, 0.3f);
            StartCoroutine(stopVib());
            vurduTimer += Time.deltaTime;
            if (vurduTimer > 0.5f)
            {
                if (dusmanSaldiri != null)
                {
                    dusmanSaldiri.enabled = true;

                    Animator oAnimator = oyuncuAnimator.GetComponent<Animator>();
                    oAnimator.speed = 1;
                    vurduTimer = 0;
                    oyuncuVurdu = false;
                }
            }
            if (arbaletSayac > 0)
            {
                arbaletTimer += Time.deltaTime;
                if (arbaletTimer > 0.5f)
                {
                    arbaletTimer = 0f;
                    arbaletSayac = 0;
                }
            }
        }
    }
    void Olum()
    {
        if (can <= 0)
        {
            Destroy(dusmanSaldiri);
            Destroy(dusman);

            if (antikaYadigarKontrol.hangiYadigarAktif[1])
                Instantiate(barutFicisi, transform.position, Quaternion.identity);
            if (dusman.dusmanSaldiri.patlayan)
                Instantiate(digerFici, transform.position, Quaternion.identity);
            if (killSayaci != null)
            {
                killSayaci.oldurmeSayisi++;
                killSayaci.yazdir();
            }

            Instantiate(killEfekt, transform.position, Quaternion.identity);
            if (hpBar != null)
                Destroy(hpBar);
            if (!havaiFisekPatlamasi)
            {
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            aniPuaniIhtimali = Random.Range(0, 100);
            if (aniPuaniIhtimali < 75)
            {
                Instantiate(aniPuaniObje, transform.position, Quaternion.identity);
                envanterKontrol.aniArttir(1);
            }
            Instantiate(ejderParasi, transform.position, Quaternion.identity);
            if (!dusmanSaldiri.patlayan)
                rastgeleSilahDusurmeScripti.silahiDusur(75f); // dusme ihtimali
            rastgeleYadigarDusurmeScripti.yadigarDusurme();
            boxCollider.enabled = false;
            dusman.animator.SetBool("yurume", false);
            dusman.animator.SetBool("olum", true);
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
            dusman.animator.enabled = false;
            dusmanSaldiri.enabled = false;
            dusman.enabled = false;
            dusman.rb.constraints = RigidbodyConstraints2D.FreezeAll;

            buzSayac += Time.deltaTime;
            if (buzSayac > buzSure)
            {
                etmenler[0].SetActive(false);
                donuyor = false;
                buzSayac = 0;
                dusman.animator.enabled = true;
                dusmanSaldiri.enabled = true;
                dusman.enabled = true;
                dusman.rb.constraints = RigidbodyConstraints2D.None;
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
            dusman.animator.enabled = false;
            dusmanSaldiri.enabled = false;
            dusman.enabled = false;
            dusman.rb.constraints = RigidbodyConstraints2D.FreezeAll;

            sersemliyorSayac += Time.deltaTime;
            if (sersemliyorSayac > sersemliyorSure)
            {
                oyuncuSaldiriTest.kritikIhtimali = ilkKritik;
                etmenler[4].SetActive(false);
                sersemliyor = false;
                sersemliyorSayac = 0;
                dusman.animator.enabled = true;
                dusmanSaldiri.enabled = true;
                dusman.enabled = true;
                dusman.rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }
    public void hasarAl(float saldiri, string hangiObje)
    {
        if (hangiObje == "silah1")
        {
            /*oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari = oyuncuSaldiriTest.silah1Script.silahDayanikliligiAzalmaMiktari;
            oyuncuSaldiriTest.silah1Script.silahDayanikliligi -= oyuncuSaldiriTest.silah1DayanikliligiAzalmaMiktari + oyuncuSaldiriTest.silah1DayanikliligiBonus;*/

            if (!silahUltileri.silah1UltiAcik)
                silahUltileri.silah1Ulti += 5;

            Instantiate(kesilmeSesi, transform.position, Quaternion.identity);

            flashHasar.Flash();

            Animator oAnimator = oyuncuAnimator.GetComponent<Animator>();
            //oAnimator.speed = 0;

            if (oyuncu.transform.position.x < transform.position.x)
                rb.velocity = Vector2.right * 4;
            else
                rb.velocity = Vector2.left * 4;

            oyuncuVurdu = true;
        }
        else if (hangiObje == "silah2")
        {
            /* if (antika3)
                 donuyor = true;
             if (antikaYadigarKontrol.hangiAntikaAktif[1])
             {
                 Debug.Log("yildirimYayi patlamasini olusturdu");
                 GameObject yeniPatlayanOk = Instantiate(patlayanOk, transform.position, transform.rotation);
                 yeniPatlayanOk.transform.parent = transform;
             }*/

            if (ozelEtkilerKontrol.yemekEtkileri[12])
                zehirleniyor = true;

            Instantiate(okVurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(saplanmaSesi, transform.position, Quaternion.identity);

            if (!silahUltileri.silah2UltiAcik)
                silahUltileri.silah2Ulti += 5;

            flashHasar.Flash();

            Animator oAnimator = oyuncuAnimator.GetComponent<Animator>();
            //oAnimator.speed = 0;
            if (dusmanSaldiri != null)
                dusmanSaldiri.enabled = false;
            if (oyuncu.transform.position.x < transform.position.x)
                rb.velocity = Vector2.right * 4;
            else
                rb.velocity = Vector2.left * 4;

            oyuncuVurdu = true;
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
        }

        dusmanUi.gorunur();

        if (arkasiDuvar)
            Instantiate(kanPartikülDuvar, transform.position, Quaternion.identity);

        hasarRaporObje = Instantiate(hasarRapor, transform.position, Quaternion.identity, transform.transform);
        hasarRaporu = hasarRaporObje.GetComponent<hasarRaporu>();
        hasarRaporu.alinanHasar = saldiri;

        Instantiate(kanPartikül, transform.position, Quaternion.identity);

        // CAN CALMA KODLARI BURADA IHTIMALI SU AN %25  // ------------------------- CAN CALMA KODLARI // CAN CALMA KODLARI -------------------------
        int randomNumara = Random.Range(0, 100);
        if (randomNumara < canCalmaIhtimali)
            canKontrol.can += 15;
        // CAN CALMA KODLARI BURADA IHTIMALI SU AN %25  // ------------------------- CAN CALMA KODLARI // CAN CALMA KODLARI ------------------------- 

        can -= saldiri;
        if (ozelEtkilerKontrol.yemekEtkileri[11])
            canKontrol.can += saldiri / 10;
        hpBar.fillAmount -= saldiri / 100;
        Olum();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ruh"))
        {
            if (oyuncu.transform.position.x < transform.position.x)
            {
                rb.velocity = Vector2.right * 6;
            }
            if (oyuncu.transform.position.x > transform.position.x)
            {
                rb.velocity = Vector2.left * 6;
            }

        }
        if (collision.gameObject.CompareTag("kanDuvari"))
        {
            arkasiDuvar = true;
        }

        if (collision.gameObject.CompareTag("oyuncu"))
        {
            Debug.Log("oyuncu denk geldi");
            oyuncuHareket = FindObjectOfType<oyuncuHareket>();
            oyuncuHareket.sonHareketHizi = 2f;
            /*if (antikaYadigarKontrol.hangiAntikaAktif[3])
                kaniyor = true;*/
        }
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
            arbaletTimer = 0f;
            arbaletSayac++;
            if (arbaletSayac == 3)
            {
                arbaletSayac = 0;
                sersemliyor = true;
            }
            if (ozelEtkilerKontrol.yemekEtkileri[12])
                zehirleniyor = true;

            Instantiate(saplanmaSesi, transform.position, Quaternion.identity);
            hasarAl(oyuncuSaldiriTest.sonHasarMenzilli, "silah2");

            // EGER OYUNCU YEMEK YEDIYSE OK BIRINCI DUSMANA CARPINCA DEGIL SONRAKI DUSMANDA YOK OLUR NORMALDE ISE ILK DUSMANDA DIREKT YOK OLUR // ----------------------
            if (ozelEtkilerKontrol.yemekEtkileri[10])
            {
                Debug.Log("Yemek etkisi aktif"); // Bu satırın çalışıp çalışmadığını kontrol edin

                projectile proj = collision.gameObject.GetComponent<projectile>();

                if (proj != null) // Eğer projectile bileşeni varsa
                {
                    Debug.Log("Projectile bileşeni bulundu: " + collision.gameObject.name);

                    if (proj.kacDusman == 0)
                    {
                        proj.kacDusman++;
                        Debug.Log("Projectile birinci düşmana çarptı");
                    }
                    else
                    {
                        Debug.Log("Projectile ikinci düşmana çarptı, yok ediliyor");
                        Destroy(collision.gameObject);
                    }
                }
                else
                    Debug.LogError("Projectile bileşeni bulunamadı");
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
        if (collision.gameObject.CompareTag("kanDuvari"))
        {
            arkasiDuvar = false;
        }
        if (collision.gameObject.CompareTag("oyuncu"))
        {

        }
        if (collision.gameObject.CompareTag("buz"))
        {
            if (can > 0)
            {
                buzSayac = buzSure;
                dusmanSaldiri.enabled = true;
                dusman.animator.enabled = true;
                dusman.enabled = true;
                donuyor = false;
            }
        }
        if (collision.gameObject.CompareTag("zehir"))
        {
            if (can > 0)
            {
                StartCoroutine(zehirdenCikti());
                zehirSayac = zehirSure;
            }
        }
    }
    IEnumerator ResumeAnimationAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Kısa bir süre bekle
        dusmanSaldiri.enabled = true;
        dusman.animator.speed = 1f; // Animasyonu devam ettir
    }
    IEnumerator stopVib()
    {
        yield return new WaitForSeconds(0.35f);
        gamepad.SetMotorSpeeds(0, 0);
    }
}

