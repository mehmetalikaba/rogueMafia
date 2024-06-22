using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakterUiTakip : MonoBehaviour
{
    public Transform hedef;
    void Update()
    {
        if(transform.rotation.y==-180)
        {
            transform.rotation=Quaternion.Euler(0,180,0);
        }
        if (transform.rotation.y == 0)
        {
            transform.rotation=Quaternion.Euler(0,0,0);
        }
    }
}
