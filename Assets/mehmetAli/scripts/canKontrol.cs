using UnityEngine;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    kameraSarsinti kameraSarsinti;

    public Animator kanUiAnimator;

    public GameObject kan;

    public float can, stamina, canArtmaMiktari, staminaArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari;

    public Image canBari, staminaBari;

    public bool staminaAzaldi, canArtiyor, canBelirlendi, dayaniklilikObjesiAktif;

    void Start()
    {
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        can = 100f;
        stamina = 100f;
    }

    void Update()
    {
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num1Tusu")))
            canAzalmasi(20);
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


        if (stamina <= 100)
        {
            stamina += staminaArtmaMiktari * Time.deltaTime;
            staminaBari.fillAmount = stamina / 100f;
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

    public void staminaAzalmasi(float staminaAzalma)
    {
        if (stamina > 1)
        {
            stamina -= staminaAzalma;
            staminaBari.fillAmount = stamina / 100f;
        }
    }
}
