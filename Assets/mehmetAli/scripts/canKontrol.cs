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

    public bool canArtiyor, canBelirlendi, dayaniklilikObjesiAktif, toplanabilirCanObjesiAktif;

    public TextMeshProUGUI canText;

    void Start()
    {
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        baslangicCani = 100f;
        can = baslangicCani;
        maxCan = baslangicCani;

        StartCoroutine(nabizEfekti());
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
                    canBari.color = Color.magenta;
                else if (dayaniklilikObjesiAktif)
                    canBari.color = Color.gray;

            }

            yield return null;
        }
    }

    public void canAzalmasi(float canAzalma)
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
                oyuncuHareket oyuncu = FindObjectOfType<oyuncuHareket>();
                Destroy(oyuncu.gameObject);
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
