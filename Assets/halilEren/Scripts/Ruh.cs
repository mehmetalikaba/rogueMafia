using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruh : MonoBehaviour
{
    oyuncuHareket oyuncuHareket;
    // Start is called before the first frame update
    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        if(oyuncuHareket.transform.localScale.x==1)
        {
            transform.position=new Vector2(transform.position.x+1,transform.position.y);
        }
        if (oyuncuHareket.transform.localScale.x == -1)
        {
            transform.position=new Vector2(transform.position.x - 1, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

}
