using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oyunKontrol : MonoBehaviour
{
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public kaydetKontrol kaydetKontrol;
    public yetenekKontrol yetenekKontrol;
    public GameObject cikisTextObje;
    public asamaKontrol asamaKontrol;

    private void Awake()
    {
        kaydetKontrol = FindObjectOfType<kaydetKontrol>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
        ozelEtkilerKontrol = FindObjectOfType<ozelEtkilerKontrol>();
    }

    void Start()
    {
        yetenekKontrol.yetenekleriUygula();
        ozelEtkilerKontrol.yemekEtkileriniUygula();
        kaydetKontrol.kaydetKontrolSes.jsonSesYukle();
        kaydetKontrol.kaydetKontrolYetenek.jsonYetenekYukle();
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterYukle();
        kaydetKontrol.kaydetKontrolOzelEtkiler.jsonOzelEtkilerYukle();
    }

    void Update()
    {
        asamaKontrol = FindObjectOfType<asamaKontrol>();
        if (asamaKontrol != null && asamaKontrol.oyuncuGeldi)
        {
            cikisTextObje.SetActive(true);

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
                StartCoroutine(yeniSahneGecis());
        }
        else
            cikisTextObje.SetActive(false);
    }
    IEnumerator yeniSahneGecis()
    {
        kaydetKontrol.kaydetKontrolEnvanter.jsonEnvanterKaydet();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }
}
