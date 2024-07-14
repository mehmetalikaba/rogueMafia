using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public float ejderhaPuani, elmas;

    public TextMeshProUGUI ejderhaPuaniMiktar, elmasMiktar;

    void Start()
    {
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
