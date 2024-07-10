using UnityEngine;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{
    public GameObject kan;

    public float can, stamina, canArtmaMiktari, staminaArtmaMiktari, ilkCan, ulasilmasiGerekenCanMiktari;

    public Image canBari, staminaBari;

    public bool staminaAzaldi, canArtiyor, canBelirlendi;

    void Start()
    {
        can = 100f;
        stamina = 100f;
    }

    void Update()
    {
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR
        if (Input.GetKeyDown(KeyCode.Keypad1))
            canAzalmasi(20);
        // BU BUTONLAR SADECE TEST ÝÇÝN VARLAR

        if (canArtiyor && can <= 100)
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
            can -= canAzalma;
            canBari.fillAmount = can / 100f;
            Instantiate(kan, transform.position, Quaternion.identity);
            if(can<=0)
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
