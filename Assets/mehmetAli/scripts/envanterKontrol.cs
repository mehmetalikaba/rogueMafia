using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderhaPuani, elmas;

    public TextMeshProUGUI ejderhaPuaniMiktar, elmasMiktar, toplanabilirMiktarText;

    public bool hpPotuVar;

    public int hpPotuMiktar;

    public canKontrol canKontrol;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();

        ejderhaPuani = 0f;
        elmas = 0f;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hpPotuVar && hpPotuMiktar > 0)
            {
                hpPotuKullanim();
            }
        }
    }

    public void ejderhaPuaniArttir(float gelenEjderhaPuani)
    {
        ejderhaPuani += gelenEjderhaPuani;
        ejderhaPuaniMiktar.text = Mathf.FloorToInt(ejderhaPuani).ToString("F0");
    }

    public void elmasArttir(float gelenElmas)
    {
        elmas += gelenElmas;
        elmasMiktar.text = Mathf.FloorToInt(elmas).ToString("F0");
    }

    public void puanlariSifirla()
    {
        ejderhaPuani = 0f;
        elmas = 0f;
        ejderhaPuaniArttir(0);
        elmasArttir(0);
    }

    public void hpPotuGeldi(int gelenHpPotuMiktar)
    {
        hpPotuVar = true;
        hpPotuMiktar = gelenHpPotuMiktar;
        toplanabilirMiktarText.text = gelenHpPotuMiktar.ToString();
    }

    public void hpPotuKullanim()
    {
        hpPotuMiktar -= 1;
        toplanabilirMiktarText.text = hpPotuMiktar.ToString();
        canKontrol.canArtmaMiktari = 10;
        canKontrol.canArtiyor = true;

        if (hpPotuMiktar == 0)
            hpPotuVar = false;
    }
}
