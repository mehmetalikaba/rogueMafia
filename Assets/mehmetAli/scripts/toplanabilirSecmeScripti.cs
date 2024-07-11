using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class toplanabilirSecmeScripti : MonoBehaviour
{
    public GameObject[] toplanabilirler;
    public bool oyuncuYakin;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public Light2D light2d;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && oyuncuYakin)
        {
            Debug.Log("sectin");

            toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();

            int rastgeleIndex = Random.Range(0, toplanabilirler.Length);
            toplanabilirKullanmaScripti.toplanabilirObje = toplanabilirler[rastgeleIndex];

            Debug.Log(toplanabilirler[rastgeleIndex]);

            toplanabilirKullanmaScripti.toplanabilirObjeOzellikleriniGetir();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            oyuncuYakin = true;
            light2d.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            oyuncuYakin = false;
            light2d.enabled = false;
        }
    }
}
