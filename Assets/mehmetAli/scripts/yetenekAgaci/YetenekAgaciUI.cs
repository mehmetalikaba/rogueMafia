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

    public void yakin1Butonu()
    {
        if (yetenekAgaci.yakinYetenekler[0].gerekliAniPuani < oyuncuAniPuani)
        {
            Debug.Log("yakin1 seviyesi acildi");
        }
    }
    public void yakin2Butonu()
    {
        if ((yetenekAgaci.yakinYetenekler[0].gerekliYetenekler[0] != null) && (yetenekAgaci.yakinYetenekler[1].gerekliAniPuani < oyuncuAniPuani))
        {
            Debug.Log("yakin1 seviyesi acildi");
        }
    }
    public void yakin3Butonu()
    {
        if ((yetenekAgaci.yakinYetenekler[0].gerekliYetenekler[0] != null) && (yetenekAgaci.yakinYetenekler[1].gerekliAniPuani < oyuncuAniPuani))
        {
            Debug.Log("yakin1 seviyesi acildi");
        }
    }
    public void yakin4Butonu()
    {

    }
    public void menzilli1Butonu()
    {

    }
    public void menzilli2Butonu()
    {

    }
    public void menzilli3Butonu()
    {

    }
    public void menzilli4Butonu()
    {

    }
    public void ozelGuc1Butonu()
    {

    }
    public void ozelGuc2Butonu()
    {

    }
    public void ozelGuc3Butonu()
    {

    }
    public void ozelGuc4Butonu()
    {

    }
}
