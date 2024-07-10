using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandikTest : MonoBehaviour
{
    public envanterKontrol envanterKontrol;

    public bool oyuncuYakin;

    void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (oyuncuYakin)
            {
                Debug.Log("Sandigi actin ama bir sey cikmadi cünkü henüz yapmadim");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
            oyuncuYakin = false;
    }
}
