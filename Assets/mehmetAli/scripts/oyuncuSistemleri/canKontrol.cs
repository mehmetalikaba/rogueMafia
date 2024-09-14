using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    Gamepad gamePad;

    public AudioSource firlatilanIleVurulmaSesi, kesiciIleVurulmaSesi, olumSesi;
    public kameraSarsinti kameraSarsinti;
    public Animator kanUiAnimator;
    public GameObject toriKalkan, kan, canIksiriBariObjesi, yenidenDogma, deadScreen, oyunPanel, canAzEfekt, karakterAnimator;
    public float baslangicCani = 100f, can, canArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari, canIksiriKatkisi, canAzalmaAzalisi, iskaSansi, artanCan, canYuzde, toriTimer;
    public Image canBari, canIksiriBari, damar;
    public bool durdur, hasarAlabilir, toriVar, oyuncuDead, canArtiyor, canBelirlendi, dayaniklilikIksiriAktif, canIksiriAktif, hasarIksiriAktif, hareketHiziIksiriAktif, ziplamaIksiriAktif, bagisiklikIksiriAktif, olmemeSansiVar;
    public TextMeshProUGUI canText;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public envanterKontrol envanterKontrol;
    public sesKontrol sesKontrol;
    public oyunKontrol oyunKontrol;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public iksirKullanmaScripti iksirKullanmaScripti;
    public bool[] etmenler;
    public GameObject[] kafaEtmen;
    public float[] etmenTimer, etmenKalanSure, etmenSure;
    public int kacOlmemeSansi;
    SpriteRenderer spriteRenderer;
    ozelEtkilerKontrol ozelEtkilerKontrol;
    float nigriTimer;

    void Start()
    {
        hasarAlabilir = true;
        gamePad = Gamepad.current;

        sesKontrol = FindObjectOfType<sesKontrol>();
        oyunKontrol = FindObjectOfType<oyunKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        oyuncuAnimasyon = FindObjectOfType<oyuncuAnimasyon>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
        iksirKullanmaScripti = FindObjectOfType<iksirKullanmaScripti>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        oyuncuEfektYoneticisi = FindObjectOfType<oyuncuEfektYoneticisi>();
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
            canAzalmasi(20, "kesici");

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
            can = 100f;
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        if (ozelEtkilerKontrol.yemekEtkileri[14] && can < baslangicCani)
        {
            nigriTimer += Time.deltaTime;
            if (nigriTimer > 5f)
            {
                nigriTimer = 0f;
                can += 2.5f;
            }
        }

        if (!oyuncuDead)
        {
            if (antikaYadigarKontrol.hangiAntikaAktif[2] && !toriVar)
                toriTilsimi();

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
        if (!canIksiriAktif)
        {
            canText.text = can.ToString("F0") + "/" + baslangicCani.ToString("F0");
            canBari.fillAmount = can / baslangicCani;
            canYuzde = (can / baslangicCani * 100);
        }

        if (can > baslangicCani)
            can = baslangicCani;

        if (oyuncuDead)
        {
            deadScreen.SetActive(true);
            oyunPanel.SetActive(false);
        }

        if (!durdur)
        {
            if (!canIksiriAktif && !dayaniklilikIksiriAktif && !hasarIksiriAktif && !hareketHiziIksiriAktif && !ziplamaIksiriAktif && !bagisiklikIksiriAktif)
            {
                if (canYuzde >= 50)
                {
                    canBari.color = Color.red;
                    damar.color = Color.red;
                }
                else if (canYuzde < 50)
                {
                    StartCoroutine(nabizEfekti());
                }
            }
            else
                iksirKullanmaScripti.iksirler();
        }

    }
    IEnumerator nabizEfekti()
    {
        while (!canIksiriAktif && !dayaniklilikIksiriAktif && !hasarIksiriAktif && !hareketHiziIksiriAktif && !ziplamaIksiriAktif && !bagisiklikIksiriAktif)
        {
            if (canYuzde < 50)
            {
                // Sıfır bölme hatasını önleyin
                if (baslangicCani > 0f && can > 0f && canYuzde > 0f)
                {
                    float transitionDuration = Mathf.Lerp(0.1f, 1f, Mathf.Clamp01(can / baslangicCani)); // Clamp ile 0-1 arasında sınırladık
                    float t = Mathf.PingPong(Time.time * (1f / Mathf.Max(transitionDuration, 0.1f)), 1f); // Min değeri 0.1f olarak belirledik
                    canBari.color = Color.Lerp(Color.red, Color.white, t);
                    damar.color = Color.Lerp(Color.red, Color.white, t);
                }

                yield return null; // Sonsuz döngüye girmez, her frame'de devam eder
            }
            else
            {
                yield break; // Can %50'yi geçtiğinde döngüden çıkar
            }
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
            oyuncuHareket.hareketKilitli = true;
            oyuncuHareket.ziplamaKilitli = true;
            oyuncuHareket.atilmaKilitli = true;
            etmenKalanSure[0] -= Time.deltaTime;
            if (etmenKalanSure[0] < 0)
            {
                etmenler[0] = false;
                kafaEtmen[0].SetActive(false);
                etmenKalanSure[0] = etmenSure[0];
                etmenTimer[0] = 0f;
                oyuncuHareket.hareketKilitli = false;
                oyuncuHareket.ziplamaKilitli = false;
                oyuncuHareket.atilmaKilitli = false;
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
            if (etmenKalanSure[1] < 0)
            {
                etmenler[1] = false;
                kafaEtmen[1].SetActive(false);
                etmenKalanSure[1] = etmenSure[1];
                etmenTimer[1] = 0f;
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
            if (etmenKalanSure[2] < 0)
            {
                etmenler[2] = false;
                kafaEtmen[2].SetActive(false);
                etmenKalanSure[2] = etmenSure[2];
                etmenTimer[2] = 0f;
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
            if (etmenKalanSure[3] < 0)
            {
                etmenler[3] = false;
                kafaEtmen[3].SetActive(false);
                etmenKalanSure[3] = etmenSure[3];
                etmenTimer[3] = 0f;
            }
        }
    }
    public void sersemliyor()
    {
        if (etmenler[4])
        {
            oyuncuHareket.hareketKilitli = true;
            oyuncuHareket.ziplamaKilitli = true;
            oyuncuHareket.atilmaKilitli = true;
            kafaEtmen[4].SetActive(true);
            etmenKalanSure[4] -= Time.deltaTime;
            if (etmenKalanSure[4] < 0)
            {
                etmenler[4] = false;
                kafaEtmen[4].SetActive(false);
                etmenKalanSure[4] = etmenSure[4];
                etmenTimer[4] = 0f;
                oyuncuHareket.hareketKilitli = false;
                oyuncuHareket.ziplamaKilitli = false;
                oyuncuHareket.atilmaKilitli = false;
            }
        }
    }
    public void toriTilsimi()
    {
        if (toriTimer < 5)
            toriTimer += Time.deltaTime;
        else
        {
            if (toriKalkan != null)
                toriKalkan.SetActive(true);
            toriVar = true;
        }
    }

    public void canAzalmasi(float canAzalma, string saldiriTuru)
    {
        if (hasarAlabilir)
        {
            if (!toriVar)
            {
                toriTimer = 0f;
                if (!oyuncuHareket.atiliyor)
                {
                    /*
                    if (!etmenler[0] && !etmenler[1] && !etmenler[2] && !etmenler[3] && !etmenler[4])
                    {
                        float a = Random.Range(0, 100);
                        if (a < 15)
                        {
                            int b = Random.Range(0, etmenler.Length);
                            etmenler[b] = true;
                        }
                    }*/
                    float randomSayi = Random.Range(0, 100);
                    if (iskaSansi > randomSayi)
                        Debug.Log("ISKA SANSI <==> " + iskaSansi + "RANDOM SAYI <==> " + randomSayi);
                    else
                    {
                        if (saldiriTuru == "firlatilan")
                        {
                            if (antikaYadigarKontrol.hangiYadigarAktif[2])
                            {
                                etmenler[1] = true;
                                etmenKalanSure[1] = etmenSure[1];
                            }
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
            }
            if (toriVar)
            {
                toriKalkan.SetActive(false);
                toriTimer = 0f;
                toriVar = false;
            }
        }
    }

    public void sonHasarAl(float canAzalma, string saldiriTuru)
    {
        if (can >= 0)
        {
            if (etmenler[4])
                canAzalma *= 1.5f;

            if (canIksiriAktif)
            {
                if (dayaniklilikIksiriAktif)
                    canIksiriKatkisi -= (canAzalma / 2) - canAzalmaAzalisi;
                else
                    canIksiriKatkisi -= canAzalma -= canAzalmaAzalisi;
                canIksiriBari.fillAmount = canIksiriKatkisi / baslangicCani;
            }
            else
            {
                if (dayaniklilikIksiriAktif)
                    can -= (canAzalma / 2) - canAzalmaAzalisi;
                else
                    can -= canAzalma -= canAzalmaAzalisi;

                canBari.fillAmount = can / 100f;
            }

            Instantiate(kan, oyuncuHareket.transform.position, Quaternion.identity);
            kanUiAnimator.SetTrigger("kanUi");
            kameraSarsinti.Shake();
            nigriTimer = 0f;

            //kol titreme

            gamePad.SetMotorSpeeds(0.5f, 0.5f);
            StartCoroutine(stopVib());

            if (can < 1)
                olum();
        }
    }

    public void olum()
    {
        if (olmemeSansiVar)
            StartCoroutine(olmemeSansi());
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
    IEnumerator olmemeSansi()
    {
        durdur = true;
        hasarAlabilir = false;
        oyuncuHareket.hareketKilitli = true;
        antikaYadigarKontrol.kontrollerAcik = false;
        oyuncuHareket.animator.SetBool("olum", true);
        oyuncuHareket.animator.SetBool("kosu", false);
        oyuncuHareket.animator.SetBool("dusus", false);
        oyuncuHareket.animator.SetBool("zipla", false);
        oyuncuHareket.animator.SetBool("egilme", false);
        oyuncuHareket.animator.SetBool("firlatma", false);
        oyuncuHareket.animator.SetBool("hazirlanma", false);
        yield return new WaitForSeconds(1.5f);
        karakterAnimator.SetActive(false);
        yenidenDogma.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        if (antikaYadigarKontrol.hangiAntikaAktif[1])
        {
            antikaYadigarKontrol.hangiAntikaAktif[1] = false;
            if (antikaYadigarKontrol.elindekiAntikalar[0] == antikaYadigarKontrol.tumAntikalar[1])
            {
                antikaYadigarKontrol.elindekiAntikalar[0] = null;
                antikaYadigarKontrol.antikaAdi[0] = "";
            }
            if (antikaYadigarKontrol.elindekiAntikalar[1] == antikaYadigarKontrol.tumAntikalar[1])
            {
                antikaYadigarKontrol.elindekiAntikalar[1] = null;
                antikaYadigarKontrol.antikaAdi[1] = "";
            }
        }
        antikaYadigarKontrol.kontrollerAcik = true;
        if (kacOlmemeSansi > 1)
            kacOlmemeSansi--;
        else if (kacOlmemeSansi <= 1)
        {
            kacOlmemeSansi--;
            olmemeSansiVar = false;
        }

        can = baslangicCani;
        karakterAnimator.SetActive(true);
        yenidenDogma.SetActive(false);
        oyuncuHareket.hareketKilitli = false;
        hasarAlabilir = true;
        yield return new WaitForSeconds(0.5f);
        durdur = false;
    }
    IEnumerator yuklemeSuresi()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
    IEnumerator stopVib()
    {
        yield return new WaitForSeconds(0.35f);
        gamePad.SetMotorSpeeds(0, 0);
    }
}
