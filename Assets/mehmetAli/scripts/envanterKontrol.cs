using TMPro;
using UnityEngine;

public class envanterKontrol : MonoBehaviour
{
    public int ejderhaPuani, anilar;

    public TextMeshProUGUI ejderhaPuaniMiktar, aniPuani;

    void Start()
    {
    }

    void Update()
    {
    }

    public void ejderhaPuaniArttir(int gelenEjderhaPuani)
    {
        ejderhaPuani += gelenEjderhaPuani;
        ejderhaPuaniMiktar.text = ejderhaPuani.ToString("F0");
    }

    public void elmasArttir(int gelenAnilar)
    {
        anilar += gelenAnilar;
        aniPuani.text = anilar.ToString("F0");
    }
}
