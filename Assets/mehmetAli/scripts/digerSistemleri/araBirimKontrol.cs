using UnityEngine;

public class araBirimKontrol : MonoBehaviour
{
    public bool heykel, kapuson, sandik;
    public bool[] aldiMi = new bool[5];
    public asamaKontrol[] kontrol;
    public GameObject maviFx, yesilFx;
    public canKontrol canKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;

    void Start()
    {

    }

    void Update()
    {
        if (sandik)
            sandikBirim();
        else if (kapuson)
            kapusonBirim();
        else if (heykel)
            heykelBirim();

    }

    public void sandikBirim()
    {

    }
    public void kapusonBirim()
    {
        if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
        {
            if (!aldiMi[0])
            {
                aldiMi[0] = true;
                if (kontrol[0].oyuncuGeldi)
                {
                    Instantiate(maviFx, kontrol[0].transform.position, Quaternion.identity);
                }
                if (kontrol[1].oyuncuGeldi)
                {
                    Instantiate(yesilFx, kontrol[1].transform.position, Quaternion.identity);
                }
            }
        }
    }
    public void heykelBirim()
    {
        if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
        {
            if (!aldiMi[0])
            {
                aldiMi[0] = true;
                if (kontrol[0].oyuncuGeldi)
                {
                    oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
                    oyuncuSaldiriTest.silah1Script.silahDayanikliligi = 100f;
                    oyuncuSaldiriTest.silah2Script.silahDayanikliligi = 100f;
                    oyuncuSaldiriTest.bonusHasarlarMenzilli += 1.5f;
                    oyuncuSaldiriTest.bonusHasarlarYakin += 1.5f;
                }
                else if (kontrol[1].oyuncuGeldi)
                {
                    canKontrol = FindObjectOfType<canKontrol>();
                    canKontrol.baslangicCani += 25f;
                    canKontrol.can = canKontrol.baslangicCani;
                }
            }
        }
    }
}
