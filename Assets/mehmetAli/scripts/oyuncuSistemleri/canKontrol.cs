using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    public AudioSource firlatilanIleVurulmaSesi, kesiciIleVurulmaSesi, olumSesi;
    public kameraSarsinti kameraSarsinti;
    public Animator kanUiAnimator;
    public GameObject kan, canIksiriBariObjesi, olmemeIsigi, deadScreen, oyunPanel, canAzEfekt;
    public float baslangicCani = 100f, can, canArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari, canIksiriKatkisi, canAzalmaAzalisi, iskaSansi, artanCan, canYuzde;
    public Image canBari, canIksiriBari;
    public bool oyuncuDead, canArtiyor, canBelirlendi, dayaniklilikObjesiAktif, toplanabilirCanObjesiAktif, hasarObjesiAktif, hareketHiziObjesiAktif, ziplamaIksiriAktif, bagisiklikIksiriAktif, olmemeSansiVar;
    public TextMeshProUGUI canText;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public envanterKontrol envanterKontrol;
    public sesKontrol sesKontrol;
    public oyunKontrol oyunKontrol;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public bool[] etmenler;
    public GameObject[] kafaEtmen;
    public float[] etmenTimer, etmenKalanSure, etmenSure;


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

        etmenSure[0] = 3f;
        etmenSure[1] = 3.5f;
        etmenSure[2] = 2.5f;
        etmenSure[3] = 4f;
        etmenSure[4] = 1f;

        etmenKalanSure[0] = 3f;
        etmenKalanSure[1] = 3.5f;
        etmenKalanSure[2] = 2.5f;
        etmenKalanSure[3] = 4f;
        etmenKalanSure[4] = 1f;
    }

    void Update()
    {
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(10, "kesici");

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
            can = 100f;
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        if (!oyuncuDead)
        {
            canKontrolleri();
            ozelGucCan();
            if (!bagisiklikIksiriAktif)
            {
                donuyor();
                zehirleniyor();
                kaniyor();
                yaniyor();
                sersemliyor();
            }
            else if (bagisiklikIksiriAktif)
            {
                for (int i = 0; i < kafaEtmen.Length; i++)
                {
                    kafaEtmen[i].SetActive(false);
                    etmenKalanSure[i] = etmenSure[i];
                    etmenler[i] = false;
                    etmenTimer[i] = 0f;
                }
            }
        }
    }

    public void canKontrolleri()
    {
        if (!toplanabilirCanObjesiAktif)
        {
            canText.text = can.ToString("F0") + "/" + baslangicCani.ToString("F0");
            canYuzde = (can / baslangicCani * 100);
            canBari.fillAmount = can / baslangicCani;
        }

        if (can > baslangicCani)
            can = baslangicCani;

        if (oyuncuDead)
        {
            deadScreen.SetActive(true);
            oyunPanel.SetActive(false);
        }

        /*if ((can / baslangicCani * 100) < 15)
            canAzEfekt.SetActive(true);
        else
            canAzEfekt.SetActive(false);*/

        if (!toplanabilirCanObjesiAktif && !dayaniklilikObjesiAktif && !hasarObjesiAktif && !hareketHiziObjesiAktif && !ziplamaIksiriAktif && !bagisiklikIksiriAktif)
        {
            if (canYuzde > 50)
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

    public void ozelGucCan()
    {
        if (canArtiyor && can < baslangicCani)
        {
            if (!canBelirlendi)
            {
                canBelirlendi = true;
                artanCan = 0f;

                if (canYuzde > 50)
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
    }

    public void donuyor()
    {
        if (etmenler[0])
        {
            kafaEtmen[0].SetActive(true);
            etmenKalanSure[0] -= Time.deltaTime;
            if (etmenKalanSure[0] < 0)
            {
                etmenler[0] = false;
                kafaEtmen[0].SetActive(false);
                etmenKalanSure[0] = etmenSure[0];
            }
        }
    }
    public void zehirleniyor()
    {
        if (etmenler[1])
        {
            kafaEtmen[1].SetActive(true);
            etmenTimer[1] += Time.deltaTime;
            if (etmenTimer[1] > 1.5f)
            {
                canAzalmasi(5f, "zehir");
                etmenTimer[1] = 0f;
            }
            etmenKalanSure[1] -= Time.deltaTime;
            if (etmenKalanSure[1] < 1)
            {
                etmenler[1] = false;
                kafaEtmen[1].SetActive(false);
                etmenKalanSure[1] = etmenSure[1];
            }
        }
    }
    public void kaniyor()
    {
        if (etmenler[2])
        {
            kafaEtmen[2].SetActive(true);
            etmenTimer[2] += Time.deltaTime;
            if (etmenTimer[2] > 1.5f)
            {
                canAzalmasi(5f, "kanama");
                etmenTimer[2] = 0f;
            }
            etmenKalanSure[2] -= Time.deltaTime;
            if (etmenKalanSure[2] < 2)
            {
                etmenler[2] = false;
                kafaEtmen[2].SetActive(false);
                etmenKalanSure[2] = etmenSure[2];
            }
        }
    }
    public void yaniyor()
    {
        if (etmenler[3])
        {
            kafaEtmen[3].SetActive(true);
            etmenTimer[3] += Time.deltaTime;
            if (etmenTimer[3] > 1.5f)
            {
                canAzalmasi(5f, "yanma");
                etmenTimer[3] = 0f;
            }
            etmenKalanSure[3] -= Time.deltaTime;
            if (etmenKalanSure[3] < 3)
            {
                etmenler[3] = false;
                kafaEtmen[3].SetActive(false);
                etmenKalanSure[3] = etmenSure[3];
            }
        }
    }
    public void sersemliyor()
    {
        if (etmenler[4])
        {
            kafaEtmen[4].SetActive(true);
            etmenKalanSure[4] -= Time.deltaTime;
            if (etmenKalanSure[4] < 4)
            {
                etmenler[4] = false;
                kafaEtmen[4].SetActive(false);
                etmenKalanSure[4] = etmenSure[4];
            }
        }
    }
    public void canAzalmasi(float canAzalma, string saldiriTuru)
    {
        float a = Random.Range(0, 100);
        if (a < 10)
        {
            int b = Random.Range(0, etmenler.Length);
            etmenler[b] = true;
        }

        float randomSayi = Random.Range(0, 100);
        if (iskaSansi > randomSayi)
            Debug.Log("ISKA SANSI <==> " + iskaSansi + "RANDOM SAYI <==> " + randomSayi);
        else
        {
            if (saldiriTuru == "firlatilan")
            {
                firlatilanIleVurulmaSesi.Play();
            }
            if (saldiriTuru == "kesici")
            {
                kesiciIleVurulmaSesi.Play();
            }
            if (saldiriTuru == "zehir")
            {

            }
            if (saldiriTuru == "kanama")
            {

            }
            if (saldiriTuru == "yanma")
            {

            }

            sonHasarAl(canAzalma, saldiriTuru);
        }
    }

    public void sonHasarAl(float canAzalma, string saldiriTuru)
    {
        if (!oyuncuHareket.atiliyor)
        {
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
                    olum();
            }
        }
    }

    public void olum()
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

    IEnumerator yuklemeSuresi()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("oyunlastirma");
    }
}
