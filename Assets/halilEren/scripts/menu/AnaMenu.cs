using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu : MonoBehaviour
{
    public Animator darkAnim;
    public GameObject anaMenu, hakkindaObj, ayarlarObj,yukleniyor,sahneObjeler;
    // Start is called before the first frame update
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
