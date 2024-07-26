using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    public GameObject deadScreen;

    public bool firlatilanIleVurulma;
    public AudioSource firlatilanIleVurulmaSesi, kesiciIleVurulmaSesi, olumSesi;

    public kameraSarsinti kameraSarsinti;
    public Animator kanUiAnimator;
    public GameObject kan, canIksiriBariObjesi, olmemeIsigi;
    public float baslangicCani, can, canArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari, maxCan, canIksiriKatkisi, canAzalmaAzalisi, iskaSansi;
    public Image canBari, canIksiriBari;
    public bool oyuncuDead, canArtiyor, canBelirlendi, dayaniklilikObjesiAktif, toplanabilirCanObjesiAktif, hasarObjesiAktif, hareketHiziObjesiAktif, pozisyonBelirlendi, olmemeSansi;
    public TextMeshProUGUI canText;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public envanterKontrol envanterKontrol;
    public kaydedilecekler kaydedilecekler;
    public sesKontrol sesKontrol;
    public scriptKontrol scriptKontrol;

    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuAnimasyon = FindObjectOfType<oyuncuAnimasyon>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        oyuncuEfektYoneticisi = FindObjectOfType<oyuncuEfektYoneticisi>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        sesKontrol = FindObjectOfType<sesKontrol>();
        scriptKontrol = FindObjectOfType<scriptKontrol>();
        baslangicCani = 100f;
        can = baslangicCani;
        maxCan = baslangicCani;


    }

    void Update()
    {
        // --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- 
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
        {
            can = 100f;
        }
        // --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- HÝLE --- 


        canText.text = can.ToString("F0") + "/" + maxCan.ToString("F0");

        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(10);
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        if (canArtiyor && can < 100)
        {
            if (!canBelirlendi)
            {
                canBelirlendi = true;
                ilkCan = can;
                ulasilmasiGerekenCanMiktari = ilkCan + canArtmaMiktari;

            }
            if (can >= ulasilmasiGerekenCanMiktari)
            {
                canArtiyor = false;
                canBelirlendi = false;
            }

            can += canArtmaMiktari * Time.deltaTime;
            canBari.fillAmount = can / 100f;
        }

        if (oyuncuDead)
        {
            canText.text = "0/100";
            deadScreen.SetActive(true);

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
                SceneManager.LoadScene("oyunlastirma");
        }

        StartCoroutine(nabizEfekti());

    }

    IEnumerator nabizEfekti()
    {
        while (can < 50 || (toplanabilirCanObjesiAktif || dayaniklilikObjesiAktif || hasarObjesiAktif || hareketHiziObjesiAktif))
        {
            if (!toplanabilirCanObjesiAktif && !dayaniklilikObjesiAktif && !hasarObjesiAktif && !hareketHiziObjesiAktif)
            {
                float transitionDuration = Mathf.Lerp(0.01f, 1f, can / 100f);
                float t = Mathf.PingPong(Time.time * (1f / transitionDuration), 1f);
                canBari.color = Color.Lerp(Color.red, Color.white, t);
            }
            else
            {
                if (toplanabilirCanObjesiAktif)
                {
                    canText.text = (can + canIksiriKatkisi).ToString() + "/" + maxCan;
                    if (!pozisyonBelirlendi)
                    {
                        float kalanCan = (100 - can) * 1.28f;
                        Vector3 yeniPozisyon = canIksiriBariObjesi.transform.localPosition;
                        yeniPozisyon.x -= kalanCan;
                        canIksiriBariObjesi.transform.localPosition = yeniPozisyon;
                        pozisyonBelirlendi = true;
                        maxCan = (can + canIksiriKatkisi);
                    }
                    if (canIksiriKatkisi + can > 100)
                    {
                        canIksiriBari.fillAmount = (100 - can) / 100;
                    }
                    else
                        canIksiriBari.fillAmount = canIksiriKatkisi / 100;
                }
                else if (dayaniklilikObjesiAktif)
                    canBari.color = Color.gray;
                else if (hasarObjesiAktif)
                    canBari.color = Color.magenta;
                else if (hareketHiziObjesiAktif)
                    canBari.color = Color.blue;
            }
            yield return null;
        }
    }

    public void canAzalmasi(float canAzalma)
    {
        float randomSayi = Random.Range(0, 100);
        if (iskaSansi >= randomSayi)
        {
            Debug.Log("iskaladi");
        }
        else
        {
            if (!oyuncuHareket.atiliyor)
            {
                if (firlatilanIleVurulma)
                {
                    firlatilanIleVurulmaSesi.Play();
                    firlatilanIleVurulma = false;
                }
                else
                {
                    kesiciIleVurulmaSesi.Play();

                }
                if (can > 1)
                {
                    if (toplanabilirCanObjesiAktif)
                    {
                        if (dayaniklilikObjesiAktif)
                            canIksiriKatkisi -= (canAzalma / 2) - canAzalmaAzalisi;
                        else
                            canIksiriKatkisi -= canAzalma -= canAzalmaAzalisi;
                        canIksiriBari.fillAmount = canIksiriKatkisi / 100;
                    }
                    else
                    {
                        if (dayaniklilikObjesiAktif)
                            can -= (canAzalma / 2) - canAzalmaAzalisi;
                        else
                            can -= canAzalma -= canAzalmaAzalisi;

                        canBari.fillAmount = can / 100f;
                    }

                    Instantiate(kan, transform.position, Quaternion.identity);
                    kanUiAnimator.SetTrigger("kanUi");
                    kameraSarsinti.Shake();


                    if (can < 1)
                    {
                        if (olmemeSansi)
                        {
                            olmemeSansi = false;
                            can = 100f;
                            //olmemeIsigi.SetActive(true);
                        }
                        else
                        {
                            canText.text = "0/100";
                            kaydedilecekler.aniPuani = envanterKontrol.aniPuani / envanterKontrol.olunceAniMiktariAzalmaYuzdesi;
                            kaydedilecekler.ejderParasi = envanterKontrol.ejderParasi;
                            olumSesi.Play();

                            for (int i = 0; i < sesKontrol.sesSeviyeleri.Length; i++)
                            {
                                sesKontrol.sesSeviyeleri[i] = 0f;
                            }

                            oyuncuDead = true;
                            oyuncuAnimasyon.enabled = false;
                            Destroy(oyuncuHareket.rb);
                            oyuncuHareket.animator.SetBool("dusus", false);
                            oyuncuHareket.animator.SetBool("zipla", false);
                            oyuncuHareket.animator.SetBool("firlatma", false);
                            oyuncuHareket.animator.SetBool("hazirlanma", false);
                            oyuncuHareket.animator.SetBool("egilme", false);
                            oyuncuHareket.animator.SetBool("kosu", false);
                            oyuncuHareket.animator.SetBool("olum", true);
                            oyuncuHareket.enabled = false;
                            oyuncuSaldiriTest.enabled = false;
                            oyuncuEfektYoneticisi.enabled = false;
                            scriptKontrol.ozelEtkilerKontrol.yemekEtkileriniGeriAl();
                            scriptKontrol.kaydetKontrol.envanterKayitTemizle();
                            Destroy(oyuncuEfektYoneticisi.tasYurumeSes);
                        }
                    }
                }
            }
        }
    }

    public void canArtmasi(float canArtma)
    {
        if (can < 100)
        {
            can += canArtma;
            canBari.fillAmount = can / 100f;
        }
    }
}
