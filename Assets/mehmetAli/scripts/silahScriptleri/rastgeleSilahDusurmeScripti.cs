using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rastgeleSilahDusurmeScripti : MonoBehaviour
{
    public silahOzellikleri dusmaninElindekiSilah;
    public rastgeleDusenSilah rastgeleDusenSilah;
    public GameObject dusecekOlanSilah;
    public SpriteRenderer dusecekOlanSilahinSpriteRenderer;
    public float randomSayi;

    public void silahiDusur(float silahDusurmeIhtimali, float minSilahDusurmeIhtimali, float maxSilahDusurmeIhtimali)
    {
        randomSayi = Random.Range(minSilahDusurmeIhtimali, maxSilahDusurmeIhtimali);

        if (randomSayi > silahDusurmeIhtimali)
        {
            rastgeleDusenSilah = dusecekOlanSilah.GetComponent<rastgeleDusenSilah>();
            dusecekOlanSilahinSpriteRenderer = rastgeleDusenSilah.GetComponent<SpriteRenderer>();
            rastgeleDusenSilah.dusenSilah = dusmaninElindekiSilah;
            dusecekOlanSilahinSpriteRenderer.sprite = dusmaninElindekiSilah.silahIcon;
            rastgeleDusenSilah.dayaniklilik = dusmaninElindekiSilah.silahDayanikliligi;
            Instantiate(dusecekOlanSilah, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), transform.rotation);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
            silahiDusur(60, 100, 100);
    }
}
