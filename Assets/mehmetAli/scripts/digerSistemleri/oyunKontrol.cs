using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oyunKontrol : MonoBehaviour
{
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public kaydetKontrol kaydetKontrol;
    public yetenekKontrol yetenekKontrol;
    public GameObject cikisTextObje, cikisKontrolcu;
    public asamaKontrol asamaKontrol;
    public oyuncuSaldiriTest oyuncuSaldiriTest;

    private void Awake()
    {
        kaydetKontrol = FindObjectOfType<kaydetKontrol>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    void Start()
    {
        yetenekKontrol.yetenekleriUygula();
        kaydetKontrol.kaydetKontrolSes.jsonSesYukle();
        kaydetKontrol.kaydetKontrolYetenek.jsonYetenekYukle();
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterYukle();
        kaydetKontrol.kaydetKontrolOzelEtkiler.jsonOzelEtkilerYukle();
        ozelEtkilerKontrol.yemekEtkileriniUygula();
    }

    void Update()
    {
        if (oyuncuSaldiriTest.silahlarKilitli)
            oyuncuSaldiriTest.silahlarKilitli = false;
        cikisKontrolcu = GameObject.Find("cikisKontrol");
        if (cikisKontrolcu != null)
        {
            asamaKontrol = cikisKontrolcu.GetComponent<asamaKontrol>();
            if (asamaKontrol != null && asamaKontrol.oyuncuGeldi)
            {
                cikisTextObje.SetActive(true);

                if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
                    StartCoroutine(yeniSahneGecis());
            }
            else
                cikisTextObje.SetActive(false);
        }
    }
    IEnumerator yeniSahneGecis()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterKaydet();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }
}
