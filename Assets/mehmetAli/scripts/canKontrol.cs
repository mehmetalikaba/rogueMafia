using UnityEngine;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{

    public float can, canAzalma, canArtma, stamina, staminaAzalma, staminaArtma;

    public Image canBari, staminaBari;

    void Start()
    {
        can = 100f;
        canAzalma = 5f;
        canArtma = 5f;

        stamina = 100f;
        staminaAzalma = 5f;
        staminaArtma = 5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            canAzalmasi();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            canArtmasi();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            staminaAzalmasi();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            staminaArtmasi();
        }
    }

    public void canAzalmasi()
    {
        if (can > 1)
        {
            can -= canAzalma;
            canBari.fillAmount = can / 100f;
        }
    }

    public void canArtmasi()
    {
        if (can < 100)
        {
            can += canArtma;
            canBari.fillAmount = can / 100f;
        }
    }

    public void staminaAzalmasi()
    {
        if (stamina > 1)
        {
            stamina -= staminaAzalma;
            staminaBari.fillAmount = stamina / 100f;
        }
    }

    public void staminaArtmasi()
    {
        if (stamina < 100)
        {
            stamina += staminaArtma;
            staminaBari.fillAmount = stamina / 100f;
        }
    }
}
