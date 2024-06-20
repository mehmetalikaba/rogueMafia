using UnityEngine;
using UnityEngine.UI;

public class canKontrol : MonoBehaviour
{

    public float can, hasar, iyilestirme;

    public Image canBari;

    void Start()
    {
        can = 100f;
        hasar = 5f;
        iyilestirme = 5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            canHasari();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            canIyilestirme();
        }
    }

    public void canHasari()
    {
        if (can > 1)
        {
            can -= hasar;
            canBari.fillAmount = can / 100f;
        }
    }

    public void canIyilestirme()
    {
        if (can < 100)
        {
            can += iyilestirme;
            canBari.fillAmount = can / 100f;
        }
    }
}
