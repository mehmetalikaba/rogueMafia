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
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
        {
            toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
            int rastgeleIndex = Random.Range(0, toplanabilirler.Length);
            toplanabilirKullanmaScripti.toplanabilirObje = toplanabilirler[rastgeleIndex];
            toplanabilirKullanmaScripti.toplanabilirObjeOzellikleriniGetir();
            Instantiate(toplanabilirler[rastgeleIndex], transform.position, transform.rotation);
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
