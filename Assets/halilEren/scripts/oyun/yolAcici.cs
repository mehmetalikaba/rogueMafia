using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yolAcici : MonoBehaviour
{
    public GameObject uiPanel, efekt;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu"))
        {
            if (killSayaci.oldurmeSayisi >= yeterliKillSayisi)
            {
                efekt.SetActive(true);
                engel.transform.position = new Vector2(engel.transform.position.x + mesafe, transform.position.y);
                kamera.maxX = kamera.maxX + mesafe;
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

        }
    }
}
