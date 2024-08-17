using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rastgeleYadigarDusurmeScripti : MonoBehaviour
{
    public antikaYadigarOzellikleri[] yadigarlar;
    public rastgeleDusenYadigar rastgeleDusenYadigar;
    public GameObject dusecekOlanYadigar;
    public SpriteRenderer dusecekOlanYadigarSpriteRenderer;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public int rastgeleSayi, dusmeIhtimali, hangiYadigar;
    public bool yadigarDustu;

    void Start()
    {
        dusmeIhtimali = 90;
    }
    public void yadigarDusurme()
    {
        if (!yadigarDustu)
        {
            yadigarDustu = true;
            rastgeleSayi = Random.Range(0, 100);
            if (dusmeIhtimali > rastgeleSayi)
            {
                hangiYadigar = Random.Range(0, yadigarlar.Length);
                rastgeleDusenYadigar = dusecekOlanYadigar.GetComponent<rastgeleDusenYadigar>();
                dusecekOlanYadigarSpriteRenderer = dusecekOlanYadigar.GetComponent<SpriteRenderer>();
                rastgeleDusenYadigar.buYadigarObjesi = yadigarlar[hangiYadigar];
                dusecekOlanYadigarSpriteRenderer.sprite = yadigarlar[hangiYadigar].yadigarIcon;
                Instantiate(dusecekOlanYadigar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }
}
