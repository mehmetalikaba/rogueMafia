using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rastgeleIksirDusurmeScripti : MonoBehaviour
{
    public GameObject dusecekOlanIksir;
    public int rastgeleSayi, dusmeIhtimali;
    public bool iksirDustu;

    void Start()
    {
        dusmeIhtimali = 25;
    }
    public void iksirDusurme()
    {
        if (!iksirDustu)
        {
            iksirDustu = true;
            rastgeleSayi = Random.Range(0, 100);
            if (dusmeIhtimali > rastgeleSayi)
                Instantiate(dusecekOlanIksir, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        }
    }
}