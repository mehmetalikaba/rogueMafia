using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    public bool firlatilanIleVurulma;
    public AudioSource firlatilanIleVurulmaSesi, kesiciIleVurulmaSesi, olumSesi;
    public kameraSarsinti kameraSarsinti;
    public Animator kanUiAnimator;
    public GameObject kan, canIksiriBariObjesi, olmemeIsigi, deadScreen, oyunPanel, canAzEfekt;
    public float baslangicCani, can, canArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari, canIksiriKatkisi, canAzalmaAzalisi, iskaSansi, artanCan, canYuzde;
    public Image canBari, canIksiriBari;
    public bool oyuncuDead, canArtiyor, canBelirlendi, dayaniklilikObjesiAktif, toplanabilirCanObjesiAktif, hasarObjesiAktif, hareketHiziObjesiAktif, olmemeSansiVar;
    public TextMeshProUGUI canText;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public envanterKontrol envanterKontrol;
    public sesKontrol sesKontrol;
    public oyunKontrol oyunKontrol;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;


    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuAnimasyon = FindObjectOfType<oyuncuAnimasyon>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        oyuncuEfektYoneticisi = FindObjectOfType<oyuncuEfektYoneticisi>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        sesKontrol = FindObjectOfType<sesKontrol>();
        oyunKontrol = FindObjectOfType<oyunKontrol>();
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        can = baslangicCani;
    }

    void Update()
    {


        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(10);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
            can = 100f;
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        canText.text = can.ToString("F0") + "/" + baslangicCani.ToString("F0");
        canBari.fillAmount = can / baslangicCani;
        canYuzde = (can / baslangicCani * 100);

        if (can > baslangicCani)
            can = baslangicCani;

        if (canArtiyor && can < baslangicCani)
        {
            if (!canBelirlendi)
            {
                canBelirlendi = true;
                artanCan = 0f;

                if (can > 50)
                    ulasilmasiGerekenCanMiktari = can / 2;
                else
                    ulasilmasiGerekenCanMiktari = 50f;
            }
            if (artanCan < ulasilmasiGerekenCanMiktari)
            {
                artanCan += Time.deltaTime * 100;
                can += Time.deltaTime * 100;
            }
            else
            {
                //can = 
                canArtiyor = false;
                canBelirlendi = false;
            }
        }

        if (oyuncuDead)
        {
            deadScreen.SetActive(true);
            oyunPanel.SetActive(false);
        }

        /*if ((can / baslangicCani * 100) < 15)
            canAzEfekt.SetActive(true);
        else
            canAzEfekt.SetActive(false);*/


        if (!toplanabilirCanObjesiAktif && !dayaniklilikObjesiAktif && !hasarObjesiAktif && !hareketHiziObjesiAktif)
        {
            if ((can / baslangicCani * 100) > 50)
                canBari.color = Color.red;
            else
                StartCoroutine(nabizEfekti());
        }
        else
            toplanabilirKullanmaScripti.iksirler();
    }



    IEnumerator nabizEfekti()
    {
        while (can < canYuzde)
        {
            float transitionDuration = Mathf.Lerp(0.01f, 1f, can / baslangicCani);
            float t = Mathf.PingPong(Time.time * (1f / transitionDuration), 1f);
            canBari.color = Color.Lerp(Color.red, Color.white, t);
            yield return null;
        }
    }

    public void canAzalmasi(float canAzalma)
    {
        float randomSayi = Random.Range(0, 100);
        if (iskaSansi > randomSayi)
        {
            Debug.Log("ISKA SANSI <==> " + iskaSansi + "RANDOM SAYI <==> " + randomSayi);
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
                    kesiciIleVurulmaSesi.Play();

                if (can > 1)
                {
                    if (toplanabilirCanObjesiAktif)
                    {
                        if (dayaniklilikObjesiAktif)
                            canIksiriKatkisi -= (canAzalma / 2) - canAzalmaAzalisi;
                        else
                            canIksiriKatkisi -= canAzalma -= canAzalmaAzalisi;
                        canIksiriBari.fillAmount = canIksiriKatkisi / baslangicCani;
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
                        if (olmemeSansiVar)
                        {
                            olmemeSansiVar = false;
                            can = baslangicCani;
                            olmemeIsigi.SetActive(true);
                        }
                        else
                        {
                            olumSesi.Play();
                            oyuncuDead = true;
                            canText.text = "0/100";
                            deadScreen.SetActive(true);
                            oyunKontrol.kaydetKontrol.kaydetKontrolEnvanter.olunceEnvanterKaydet();
                            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
                            foreach (AudioSource audioSource in allAudioSources)
                            {
                                if (audioSource.gameObject.name != "oyunMuzigi")
                                    audioSource.volume = 0f;
                            }
                            Destroy(oyuncuHareket.rb);
                            oyuncuHareket.enabled = false;
                            oyuncuAnimasyon.enabled = false;
                            StartCoroutine(yuklemeSuresi());
                            oyuncuSaldiriTest.enabled = false;
                            oyuncuEfektYoneticisi.enabled = false;
                            Destroy(oyuncuEfektYoneticisi.tasYurumeSes);
                            oyuncuHareket.animator.SetBool("olum", true);
                            oyuncuHareket.animator.SetBool("kosu", false);
                            oyuncuHareket.animator.SetBool("dusus", false);
                            oyuncuHareket.animator.SetBool("zipla", false);
                            oyuncuHareket.animator.SetBool("egilme", false);
                            oyuncuHareket.animator.SetBool("firlatma", false);
                            oyuncuHareket.animator.SetBool("hazirlanma", false);
                        }
                    }
                }
            }
        }
    }

    IEnumerator yuklemeSuresi()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("oyunlastirma");
    }
}
