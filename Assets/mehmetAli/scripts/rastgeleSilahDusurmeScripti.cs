using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rastgeleSilahDusurmeScripti : MonoBehaviour
{
    public silahOzellikleri dusmaninElindekiSilah;
    public rastgeleDusenSilah rastgeleDusenSilah;
    public GameObject dusecekOlanSilah;
    public float randomSayi;

    public void silahiDusur(float silahDusurmeIhtimali, float minSilahDusurmeIhtimali, float maxSilahDusurmeIhtimali)
    {
        randomSayi = Random.Range(minSilahDusurmeIhtimali, maxSilahDusurmeIhtimali);

        if (randomSayi > silahDusurmeIhtimali)
        {
            rastgeleDusenSilah = dusecekOlanSilah.GetComponent<rastgeleDusenSilah>();
            rastgeleDusenSilah.dusenSilah = dusmaninElindekiSilah;
            rastgeleDusenSilah.silahIconu.sprite = dusmaninElindekiSilah.silahIcon;
            Instantiate(dusecekOlanSilah, transform.position, transform.rotation);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num2Tusu")))
            silahiDusur(60, 50, 100);
    }
}
