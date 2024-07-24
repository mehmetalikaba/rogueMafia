using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanSes : MonoBehaviour
{
    public dusmanHareket dusmanHareket;

    Rigidbody2D rb;
    public GameObject cimYurumeSes, tasYurumeSes;
    public bool cimde, tasda;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(cimde)
        {
            if (dusmanHareket.saga||dusmanHareket.sola)
            {
                cimYurumeSes.SetActive(true);
            }
            else
            {
                cimYurumeSes.SetActive(false);
            }
        }
        if (tasda)
        {
            if (dusmanHareket.saga || dusmanHareket.sola)
            {
                tasYurumeSes.SetActive(true);
            }
            else
            {
                tasYurumeSes.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("zemin"))
        {
            tasda = true;
            cimde = false;
        }
        if(collision.gameObject.CompareTag("cimZemin"))
        {
            cimde = true;
            tasda = false;
        }
    }
}
