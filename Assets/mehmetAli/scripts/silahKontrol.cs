using UnityEngine;
using UnityEngine.UI;

public class silahKontrol : MonoBehaviour
{
    public silahOzellikleriniGetir silah1Ozellikleri;
    public silahOzellikleriniGetir silah2Ozellikleri;
    public GameObject silah1, silah2, birakilacakSilah;
    public silahSecimi.silahlar geciciSilah;
    public SpriteRenderer birakilacakSilahSpriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public float silahAlmaSuresi;
    public bool silahAldi;

    void Start()
    {
        silah1Ozellikleri = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = silah2.GetComponent<silahOzellikleriniGetir>();

        silah1Getir(silah1Ozellikleri.silahSecimi.tumSilahlar);
        silah2Getir(silah2Ozellikleri.silahSecimi.tumSilahlar);

        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();

    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("kTusu")))
        {
            silahlarDegistir();
        }
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("bTusu")))
        {
            eldekiSilahiBirakma();
        }

        if (silahAldi)
        {
            silahAlmaSuresi -= Time.deltaTime;
            oyuncuSaldiriTest.animator.SetBool("egilme", true);
            oyuncuSaldiriTest.animator.SetBool("kosu", false);
            oyuncuSaldiriTest.animator.SetBool("zipla", false);
            oyuncuSaldiriTest.animator.SetBool("dusus", false);
            if (silahAlmaSuresi < 0)
            {
                oyuncuSaldiriTest.animator.SetBool("egilme", false);
                silahAldi = false;
                silahAlmaSuresi = 0.5f;
            }
        }
    }

    public void silah1Getir(silahSecimi.silahlar silahAdi)
    {
        silah1Ozellikleri.silahSecimi.tumSilahlar = silahAdi;
        silah1Ozellikleri.silahOzellikleriniGuncelle();
    }

    public void silah2Getir(silahSecimi.silahlar silahAdi)
    {
        silah2Ozellikleri.silahSecimi.tumSilahlar = silahAdi;
        silah2Ozellikleri.silahOzellikleriniGuncelle();
    }

    public void silahlarDegistir()
    {
        geciciSilah = silah1Ozellikleri.silahSecimi.tumSilahlar;
        silah1Getir(silah2Ozellikleri.silahSecimi.tumSilahlar);
        silah2Getir(geciciSilah);
    }

    public void eldekiSilahiBirakma()
    {
        Instantiate(birakilacakSilah, transform.position, transform.rotation);
        birakilacakSilahSpriteRenderer = birakilacakSilah.GetComponent<SpriteRenderer>();
        birakilacakSilahSpriteRenderer.sprite = silah1Ozellikleri.silahImage.sprite;
    }
}