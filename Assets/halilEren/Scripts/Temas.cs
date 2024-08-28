using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temas : MonoBehaviour
{
    oyuncuHareket oyuncu;
    // Start is called before the first frame update
    void Start()
    {
        oyuncu=FindObjectOfType<oyuncuHareket>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position=oyuncu.transform.position;   
    }
}
