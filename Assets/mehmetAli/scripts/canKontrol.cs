using System.Collections;
using TMPro;
using UnityEngine;
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

    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuAnimasyon = FindObjectOfType<oyuncuAnimasyon>();
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        oyuncuEfektYoneticisi = FindObjectOfType<oyuncuEfektYoneticisi>();
        baslangicCani = 100f;
        can = baslangicCani;
        maxCan = baslangicCani;


    }

    void Update()
    {
        canText.text = can.ToString("F0") + "/" + maxCan.ToString("F0");

        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(10);
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        if (canArtiyor && can < 100)
        {
            Debug.Log("can artiyor");
            if (!canBelirlendi)
            {
                canBelirlendi = true;
                ilkCan = can;
                ulasilmasiGerekenCanMiktari = ilkCan + canArtmaMiktari;

            }
            if (can >= ulasilmasiGerekenCanMiktari)
            {
                Debug.Log("can ARTTI");
                canArtiyor = false;
                canBelirlendi = false;
            }

            can += canArtmaMiktari * Time.deltaTime;
            canBari.fillAmount = can / 100f;
        }

        if (can <= 0)
            oyuncuDead = true;

        if (can < 50 || (toplanabilirCanObjesiAktif || dayaniklilikObjesiAktif || hasarObjesiAktif || hareketHiziObjesiAktif))
        {
            StartCoroutine(nabizEfekti());
        }

    }

    IEnumerator nabizEfekti()
    {
        while (true)
        {
            if (!toplanabilirCanObjesiAktif && !dayaniklilikObjesiAktif)
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
