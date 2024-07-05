using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenu : MonoBehaviour
{
    public Animator darkAnim;
    public GameObject anaMenu, hakkindaObj, ayarlarObj, yukleniyor, sahneObjeler;
    public GameObject tusAtamaAyar, grafikAyar, sesAyar, oyunAyar, ayarlarAna;




    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    public void oyna()
    {
        darkAnim.SetTrigger("dark");
        anaMenu.SetActive(false);
        sahneObjeler.SetActive(false);
        yukleniyor.SetActive(true);
        StartCoroutine(gameStartTime());
    }

    public void hakinda()
    {
        darkAnim.SetTrigger("dark");
        hakkindaObj.SetActive(true);
        anaMenu.SetActive(false);
    }

    public void ayarlar()
    {
        darkAnim.SetTrigger("dark");
        ayarlarObj.SetActive(true);
        anaMenu.SetActive(false);
    }

    public void geriDon()
    {
        darkAnim.SetTrigger("dark");
        anaMenu.SetActive(true);
        ayarlarObj.SetActive(false);
        hakkindaObj.SetActive(false);
    }

    public void tusAtamaAyarlari()
    {
        darkAnim.SetTrigger("dark");
        ayarlarAna.SetActive(false);
        tusAtamaAyar.SetActive(true);
    }

    public void grafikAyarlari()
    {
        darkAnim.SetTrigger("dark");
        grafikAyar.SetActive(true);
        ayarlarAna.SetActive(false);
    }

    public void oyunAyarlari()
    {
        darkAnim.SetTrigger("dark");
        oyunAyar.SetActive(true);
        ayarlarAna.SetActive(false);
    }

    public void sesAyarlari()
    {
        darkAnim.SetTrigger("dark");
        sesAyar.SetActive(true);
        ayarlarAna.SetActive(false);
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
        Application.Quit();
    }

    IEnumerator gameStartTime()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("test");
    }
}