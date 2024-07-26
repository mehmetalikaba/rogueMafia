using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenu : MonoBehaviour
{
    public AudioSource onaySes, geriSes;

    public Animator darkAnim;
    public GameObject anaMenu, hakkindaObj, ayarlarObj, yukleniyor, sahneObjeler;
    public GameObject tusAtamaAyar, grafikAyar, sesAyar, oyunAyar, ayarlarAna;

    public GameObject oyunAyariSecili, grafikAyariSecili, sesAyariSecili, tusAtariSecili;

    public tusDizilimleri tusDizilimleri;

    public kaydedilecekler kaydedilecekler;

    public localizedText yeniOyun;


    private void Awake()
    {
        if (kaydedilecekler.oyunaBasladi)
            yeniOyun.key = "devam_et";
        else
        {
            yeniOyun.key = "oyna";

        }
    }

    void Start()
    {
        tusDizilimleri = FindObjectOfType<tusDizilimleri>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    public void oyna()
    {
        kaydedilecekler.oyunaBasladi = true;
        onaySes.Play();

        darkAnim.SetTrigger("dark");
        anaMenu.SetActive(false);
        //sahneObjeler.SetActive(false);
        yukleniyor.SetActive(true);
        StartCoroutine(gameStartTime());
    }

    public void hakinda()
    {
        onaySes.Play();

        darkAnim.SetTrigger("dark");
        hakkindaObj.SetActive(true);
        anaMenu.SetActive(false);
    }

    public void ayarlar()
    {
        onaySes.Play();

        darkAnim.SetTrigger("dark");
        ayarlarObj.SetActive(true);
        anaMenu.SetActive(false);
    }

    public void geriDon()
    {
        geriSes.Play();

        darkAnim.SetTrigger("dark");
        anaMenu.SetActive(true);
        ayarlarObj.SetActive(false);
        hakkindaObj.SetActive(false);
    }

    public void tusAtamaAyarlari()
    {
        onaySes.Play();

        //darkAnim.SetTrigger("dark");
        //ayarlarAna.SetActive(false);
        tusDizilimleri.tusMetinleriGetir();
        tusAtariSecili.SetActive(true);

        oyunAyariSecili.SetActive(false);
        oyunAyar.SetActive(false);
        grafikAyariSecili.SetActive(false);
        grafikAyar.SetActive(false);
        sesAyariSecili.SetActive(false);
        sesAyar.SetActive(false);

        tusAtamaAyar.SetActive(true);
    }

    public void grafikAyarlari()
    {
        onaySes.Play();

        //darkAnim.SetTrigger("dark");
        grafikAyariSecili.SetActive(true);

        oyunAyariSecili.SetActive(false);
        oyunAyar.SetActive(false);
        tusAtariSecili.SetActive(false);
        tusAtamaAyar.SetActive(false);
        sesAyariSecili.SetActive(false);
        sesAyar.SetActive(false);

        grafikAyar.SetActive(true);
        //ayarlarAna.SetActive(false);
    }

    public void oyunAyarlari()
    {
        onaySes.Play();

        //darkAnim.SetTrigger("dark");
        oyunAyariSecili.SetActive(true);

        tusAtariSecili.SetActive(false);
        tusAtamaAyar.SetActive(false);
        sesAyariSecili.SetActive(false);
        sesAyar.SetActive(false);
        grafikAyariSecili.SetActive(false);
        grafikAyar.SetActive(false);

        oyunAyar.SetActive(true);
        //ayarlarAna.SetActive(false);
    }

    public void sesAyarlari()
    {
        onaySes.Play();

        //darkAnim.SetTrigger("dark");
        sesAyariSecili.SetActive(true);

        oyunAyariSecili.SetActive(false);
        oyunAyar.SetActive(false);
        grafikAyariSecili.SetActive(false);
        grafikAyar.SetActive(false);
        tusAtariSecili.SetActive(false);
        tusAtamaAyar.SetActive(false);

        sesAyar.SetActive(true);
        //ayarlarAna.SetActive(false);
    }

    public void seceneklereDon()
    {
        darkAnim.SetTrigger("dark");
        ayarlarAna.SetActive(true);
        tusAtamaAyar.SetActive(false);
        oyunAyar.SetActive(false);
        sesAyar.SetActive(false);
        grafikAyar.SetActive(false);
    }

    public void oyunuKapat()
    {
        geriSes.Play();
        Application.Quit();
    }

    IEnumerator gameStartTime()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(1);
    }
}