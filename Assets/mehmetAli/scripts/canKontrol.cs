using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    kameraSarsinti kameraSarsinti;
    public Animator kanUiAnimator;
    public GameObject kan;
    public float baslangicCani, can, canArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari, maxCan;
    public Image canBari;
    public bool oyuncuDead, canArtiyor, canBelirlendi, dayaniklilikObjesiAktif, toplanabilirCanObjesiAktif, hasarObjesiAktif, hareketHiziObjesiAktif;
    public TextMeshProUGUI canText;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public envanterKontrol envanterKontrol;

    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuAnimasyon = FindObjectOfType<oyuncuAnimasyon>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        oyuncuEfektYoneticisi = FindObjectOfType<oyuncuEfektYoneticisi>();
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        baslangicCani = 100f;
        can = baslangicCani;
        maxCan = baslangicCani;


    }

    void Update()
    {
        // --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- 
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num5Tusu")))
        {
            can = 100f;
        }
        // --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- H�LE --- 


        canText.text = can.ToString("F0") + "/" + maxCan.ToString("F0");

        // BU BUTONLAR SADECE TEST ���N VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(10);
        // BU BUTONLAR SADECE TEST ���N VARLAR

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

        if (can <= 0)
            oyuncuDead = true;

        if (oyuncuDead)
        {
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("endTusu")))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (can < 50 || (toplanabilirCanObjesiAktif || dayaniklilikObjesiAktif || hasarObjesiAktif || hareketHiziObjesiAktif))
        {
            StartCoroutine(nabizEfekti());
        }

    }

    IEnumerator nabizEfekti()
    {
        while (true)
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
                    canBari.color = Color.green;
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
        if (!oyuncuHareket.atiliyor)
        {
            if (can > 1)
            {
                if (dayaniklilikObjesiAktif)
                    can -= (canAzalma / 2);
                else
                    can -= canAzalma;

                canBari.fillAmount = can / 100f;
                Instantiate(kan, transform.position, Quaternion.identity);
                kanUiAnimator.SetTrigger("kanUi");
                kameraSarsinti.Shake();

                if (can <= 0)
                {
                    oyuncuDead = true;
                    envanterKontrol.aniKaydet();
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
                    Destroy(oyuncuEfektYoneticisi.yurumeSes);
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
