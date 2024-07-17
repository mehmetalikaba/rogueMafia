using UnityEngine;
using UnityEngine.UI;

public class yetenekAgaciUI : MonoBehaviour
{
    public Text[] yetenekAdlari;
    public Text[] yetenekAciklamalari;
    public Button[] yetenekButonlari;
    public yetenekObjesi[] yetenekler;
    public yetenekAgaclari yetenekAgaci;
    public int oyuncuAniPuani;
    public envanterKontrol envanterKontrol;

    private void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();

        oyuncuAniPuani = envanterKontrol.anilar;
    }

    public void yakin1SeviyeYukseltme()
    {
        yetenekler[0].yetenekSeviyesi = 1;
    }
}
