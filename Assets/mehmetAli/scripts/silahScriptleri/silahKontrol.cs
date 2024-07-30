using UnityEngine;
using UnityEngine.UI;

public class silahKontrol : MonoBehaviour
{
    public silahOzellikleriniGetir silah1Ozellikleri, silah2Ozellikleri;
    public GameObject silah1, silah2, birakilacakSilah;
    public SpriteRenderer birakilacakSilahSpriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public oyuncuHareket oyuncuHareket;
    public rastgeleDusenSilah rastgeleDusenSilah;
    public float ilkOyuncuHareketHizi, silahAlmaSuresi;
    public bool silahAldi;


    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        silah1Ozellikleri = silah1.GetComponent<silahOzellikleriniGetir>();
        silah2Ozellikleri = silah2.GetComponent<silahOzellikleriniGetir>();
    }
    void Update()
    {
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
    public void silah1YereAt()
    {
        rastgeleDusenSilah = birakilacakSilah.GetComponent<rastgeleDusenSilah>();
        birakilacakSilahSpriteRenderer = rastgeleDusenSilah.GetComponent<SpriteRenderer>();
        rastgeleDusenSilah.dusenSilah = silah1Ozellikleri.secilenSilahOzellikleri;
        birakilacakSilahSpriteRenderer.sprite = silah1Ozellikleri.silahImage.sprite;
        rastgeleDusenSilah.dayaniklilik = silah1Ozellikleri.silahDayanikliligi;
        Instantiate(birakilacakSilah, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), transform.rotation);
    }
    public void silah2YereAt()
    {
        rastgeleDusenSilah = birakilacakSilah.GetComponent<rastgeleDusenSilah>();
        birakilacakSilahSpriteRenderer = rastgeleDusenSilah.GetComponent<SpriteRenderer>();
        rastgeleDusenSilah.dusenSilah = silah2Ozellikleri.secilenSilahOzellikleri;
        birakilacakSilahSpriteRenderer.sprite = silah2Ozellikleri.silahImage.sprite;
        rastgeleDusenSilah.dayaniklilik = silah2Ozellikleri.silahDayanikliligi;
        Instantiate(birakilacakSilah, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), transform.rotation);
    }
}