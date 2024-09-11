using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birimGorunurlugu : MonoBehaviour
{
    public GameObject birim;
    public float yakinlik,mesafe;
    public oyuncuHareket oyuncuHareket;
    // Start is called before the first frame update
    void Awake()
    {
        oyuncuHareket=FindObjectOfType<oyuncuHareket>();
        birim.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mesafe = Vector2.Distance(transform.position, oyuncuHareket.transform.position);
        if(mesafe<=yakinlik)
        {
            birim.gameObject.SetActive(true);
        }
        if (mesafe > yakinlik)
        {
            birim.gameObject.SetActive(false);
        }
    }
}
