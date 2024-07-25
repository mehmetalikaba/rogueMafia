using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanZeminKontrol : MonoBehaviour
{
    public bool cikti;
    public dusmanHareket dusmanHareket;
    private void FixedUpdate()
    {
        if(cikti)
        {
            if (dusmanHareket.saga)
            {
                dusmanHareket.saga = false;
                dusmanHareket.bekleSag = true;


            }
            if (dusmanHareket.sola)
            {
                dusmanHareket.sola = false;
                dusmanHareket.bekleSol = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            StartCoroutine(beklemeSuresi());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            cikti = true;
        }
    }
    IEnumerator beklemeSuresi()
    {
        yield return new WaitForSeconds(1.5f);
        cikti = false;
    }
}
