using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yolAcici : MonoBehaviour
{
    public bool birinci, ikinci,oyunBitimi,fBas;
    public GameObject uiPanel, efekt,fBasObj;

    Kamera kamera;
    killSayaci killSayaci;

    public float mesafe;

    public int yeterliKillSayisi;

    public GameObject engel;

    // Start is called before the first frame update
    void Start()
    {
        kamera=FindObjectOfType<Kamera>();
        killSayaci=FindObjectOfType<killSayaci>();

        engel = GameObject.Find("SağEngel");
    }
    private void Update()
    {
        if ((Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu"))||Input.GetKeyDown(KeyCode.JoystickButton2))&& fBas)
        {
            SceneManager.LoadScene("gistAraBase");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu"))
        {
            if (killSayaci.oldurmeSayisi >= yeterliKillSayisi)
            {
                efekt.SetActive(true);
                if(birinci)
                {

                    //engel.transform.position = new Vector2(185, transform.position.y);
                    kamera.maxX = 175;
                }
                if(ikinci)
                {
                    //engel.transform.position = new Vector2(310, transform.position.y);
                    kamera.maxX = 300;
                }
                if(oyunBitimi)
                {
                    fBas = true;
                    fBasObj.SetActive(true);
                }
            }
            else
            {
                uiPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu"))
        {
            uiPanel.SetActive(false);
            if (oyunBitimi)
            {
                fBas = false;
                fBasObj.SetActive(false);
            }
        }
    }
}
