using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merdiven : MonoBehaviour
{
    public tirmanma tirmanma;
    public Transform oyuncuTransform;
    public GameObject oyuncu;
    public oyuncuHareket oyuncuHareket;

    void Start()
    {
        tirmanma = FindObjectOfType<tirmanma>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        if (oyuncuTransform != null)
        {
            if (tirmanma.tirmaniyor)
            {
                oyuncuHareket.animator.SetBool("kosu", false);
                Vector3 newPosition = new Vector3(transform.position.x, oyuncuTransform.position.y, oyuncuTransform.position.z);
                oyuncuTransform.position = newPosition;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            tirmanma.oyuncuYakin = true;
            oyuncuTransform = oyuncu.transform;
            //if (Input.GetKey(tusDizilimleri.instance.tusIsleviGetir("wTusu")))
                //oyuncuHareket.rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            tirmanma.oyuncuYakin = false;
            tirmanma.tirmaniyor = false;
            oyuncuTransform = null;
        }
    }
}
