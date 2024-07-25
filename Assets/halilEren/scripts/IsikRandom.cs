using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsikRandom : MonoBehaviour
{
    public GameObject isiklar;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, 2);
        if(i==1)
        {
            isiklar.SetActive(true);
        }
        else
        {
            isiklar.SetActive(false);
        }
    }

}
