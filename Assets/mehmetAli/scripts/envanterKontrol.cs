using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderhaPuani, elmas;

    public TextMeshProUGUI ejderhaPuaniMiktar, elmasMiktar;

    public bool canPotuVar;

    public GameObject[] toplanabilirler;

    public GameObject toplanabilirObje;

    public canKontrol canKontrol;

    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
        ejderhaPuani = 0f;
        elmas = 0f;
    }

    void Update()
    {
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
}
