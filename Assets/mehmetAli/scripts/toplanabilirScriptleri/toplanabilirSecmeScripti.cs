using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class toplanabilirSecmeScripti : MonoBehaviour
{
    public GameObject[] toplanabilirler;
    public bool oyuncuYakin;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public Light2D light2d;
    public Transform toplanabilirOlusmaNoktasi;
    SpriteRenderer objeSpriteRenderer;

    public void Start()
    {
        objeSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
        {
            toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
            int rastgeleIndex = Random.Range(0, toplanabilirler.Length);
            Instantiate(toplanabilirler[rastgeleIndex], toplanabilirOlusmaNoktasi.transform.position, transform.rotation);
            light2d.enabled = false;
            objeSpriteRenderer.color = UnityEngine.Color.red;
            Destroy(this);
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
