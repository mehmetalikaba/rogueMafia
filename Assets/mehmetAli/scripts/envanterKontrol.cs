using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public int ejderhaPuani, anilar;

    public TextMeshProUGUI ejderhaPuaniMiktar, elmasMiktar;

    void Start()
    {
    }

    void Update()
    {
    }

    public void ejderhaPuaniArttir(int gelenEjderhaPuani)
    {
        ejderhaPuani += gelenEjderhaPuani;
        ejderhaPuaniMiktar.text = Mathf.FloorToInt(ejderhaPuani).ToString("F0");
    }

    public void elmasArttir(int gelenAnilar)
    {
        anilar += gelenAnilar;
        elmasMiktar.text = Mathf.FloorToInt(anilar).ToString("F0");
    }
}
