using UnityEngine;

public class silahKontrol : MonoBehaviour
{
    public silahOzellikleriniGetir silah1Ozellikleri, silah2Ozellikleri;
    public GameObject silah1, silah2, birakilacakSilah;
    public SpriteRenderer birakilacakSilahSpriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public rastgeleDusenSilah rastgeleDusenSilah;
    public float yerdenAlmaSuresi;
    public bool yerdenAliyor;

    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silah1Ozellikleri = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = silah2.GetComponent<silahOzellikleriniGetir>();
        yerdenAlmaSuresi = 0.5f;
    }

    void Update()
    {
        if (yerdenAliyor)
        {
            oyuncuHareket.hareketKilitli = true;
            oyuncuHareket.inmeKilitli = true;
            oyuncuHareket.ziplamaKilitli = true;
            oyuncuSaldiriTest.silahlarKilitli = true;

            yerdenAlmaSuresi -= Time.deltaTime;
            oyuncuSaldiriTest.animator.SetBool("egilme", true);
            oyuncuSaldiriTest.animator.SetBool("kosu", false);
            oyuncuSaldiriTest.animator.SetBool("zipla", false);
            oyuncuSaldiriTest.animator.SetBool("dusus", false);

            if (yerdenAlmaSuresi < 0)
            {
                oyuncuSaldiriTest.animator.SetBool("egilme", false);
                yerdenAliyor = false;
                yerdenAlmaSuresi = 0.5f;
                oyuncuHareket.hareketKilitli = false;
                oyuncuHareket.inmeKilitli = false;
                oyuncuHareket.ziplamaKilitli = false;
                oyuncuSaldiriTest.silahlarKilitli = false;
            }
        }
    }

    public void silah1YereAt()
    {
        rastgeleDusenSilah = birakilacakSilah.GetComponent<rastgeleDusenSilah>();
        birakilacakSilahSpriteRenderer = rastgeleDusenSilah.GetComponent<SpriteRenderer>();
        rastgeleDusenSilah.dusenSilah = silah1Ozellikleri.elindekiSilah;
        birakilacakSilahSpriteRenderer.sprite = silah1Ozellikleri.silahImage.sprite;
        rastgeleDusenSilah.dayaniklilik = silah1Ozellikleri.silahDayanikliligi;
        Instantiate(birakilacakSilah, oyuncuSaldiriTest.transform.position, oyuncuSaldiriTest.transform.rotation);
    }

    public void silah2YereAt()
    {
        rastgeleDusenSilah = birakilacakSilah.GetComponent<rastgeleDusenSilah>();
        birakilacakSilahSpriteRenderer = rastgeleDusenSilah.GetComponent<SpriteRenderer>();
        rastgeleDusenSilah.dusenSilah = silah2Ozellikleri.elindekiSilah;
        birakilacakSilahSpriteRenderer.sprite = silah2Ozellikleri.silahImage.sprite;
        rastgeleDusenSilah.dayaniklilik = silah2Ozellikleri.silahDayanikliligi;
        Instantiate(birakilacakSilah, oyuncuSaldiriTest.transform.position, oyuncuSaldiriTest.transform.rotation);
    }
}
