using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    public GameObject deadScreen, oyunPanel;
    public bool firlatilanIleVurulma;
    public AudioSource firlatilanIleVurulmaSesi, kesiciIleVurulmaSesi, olumSesi;
    public kameraSarsinti kameraSarsinti;
    public Animator kanUiAnimator;
    public GameObject kan, canIksiriBariObjesi, olmemeIsigi;
    public float baslangicCani, can, canArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari, maxCan, canIksiriKatkisi, canAzalmaAzalisi, iskaSansi, artanCan;
    public Image canBari, canIksiriBari;
    public bool oyuncuDead, canArtiyor, canBelirlendi, dayaniklilikObjesiAktif, toplanabilirCanObjesiAktif, hasarObjesiAktif, hareketHiziObjesiAktif, pozisyonBelirlendi, olmemeSansi;
    public TextMeshProUGUI canText;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public envanterKontrol envanterKontrol;
    public sesKontrol sesKontrol;
    public oyunKontrol oyunKontrol;
    public RectTransform canIksiriBariRectTransform;
    public Vector3 baslangicVector3;
    public Vector3 canIksiriBariVector3;


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
        can = baslangicCani;
        maxCan = baslangicCani;

        baslangicVector3 = canIksiriBariRectTransform.position;
    }

    void Update()
    {


        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(10);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
            can = 100f;
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        canText.text = can.ToString("F0") + "/" + maxCan.ToString("F0");
        canBari.fillAmount = can / maxCan;

        if (can > baslangicCani)
            can = baslangicCani;

        if (canArtiyor && can < maxCan)
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
            StartCoroutine(yuklemeSuresi());
        }

        if (!toplanabilirCanObjesiAktif && !dayaniklilikObjesiAktif && !hasarObjesiAktif && !hareketHiziObjesiAktif)
        {
            if (can > 50)
                canBari.color = Color.red;
            else
                StartCoroutine(nabizEfekti());
        }
        else
            iksirler();
    }

    public void iksirler()
    {
        if (toplanabilirCanObjesiAktif)
        {
            float toplamCan = can + canIksiriKatkisi;
            canText.text = toplamCan.ToString("F0") + "/" + maxCan;

            if (!pozisyonBelirlendi)
            {
                pozisyonBelirlendi = true;
                canIksiriBariRectTransform.position = baslangicVector3;
                float canFarki = maxCan - can;
                canIksiriBariVector3.x -= canFarki;
                canIksiriBariRectTransform.position = canIksiriBariVector3;
            }

            if (toplamCan > maxCan)
            {
                maxCan = toplamCan;
                canIksiriBari.fillAmount = (maxCan - can) / maxCan;
            }
            else
                canIksiriBari.fillAmount = canIksiriKatkisi / maxCan;
        }
        else if (dayaniklilikObjesiAktif)
            canBari.color = Color.gray;
        else if (hasarObjesiAktif)
            canBari.color = Color.magenta;
        else if (hareketHiziObjesiAktif)
            canBari.color = Color.blue;
    }

    IEnumerator nabizEfekti()
    {
        while (can < 50)
        {
            float transitionDuration = Mathf.Lerp(0.01f, 1f, can / maxCan);
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
                    kesiciIleVurulmaSesi.Play();

                if (can > 1)
                {
                    if (toplanabilirCanObjesiAktif)
                    {
                        if (dayaniklilikObjesiAktif)
                            canIksiriKatkisi -= (canAzalma / 2) - canAzalmaAzalisi;
                        else
                            canIksiriKatkisi -= canAzalma -= canAzalmaAzalisi;
                        canIksiriBari.fillAmount = canIksiriKatkisi / maxCan;
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
