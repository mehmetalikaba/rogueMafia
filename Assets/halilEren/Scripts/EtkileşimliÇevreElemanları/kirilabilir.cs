using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kirilabilir : MonoBehaviour
{
    public oyuncuSaldiriTest oyuncuSaldiriTest;

    public GameObject kirilmisObj;
    public bool oyuncuYakin;
    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }
    private void FixedUpdate()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && oyuncuYakin && !oyuncuSaldiriTest.yumruk1)
            kirildi();
    }
    public void kirildi()
    {
        Instantiate(kirilmisObj,transform.position,kirilmisObj.transform.rotation);
        Destroy(gameObject);
    }
}
