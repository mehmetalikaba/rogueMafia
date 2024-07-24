using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanZeminKontrol : MonoBehaviour
{
    public bool cikti;
    public dusmanHareket dusmanHareket;
    public dusmanAgresif dusmanAgresif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            cikti = true;
            if(dusmanHareket.saga)
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
}
