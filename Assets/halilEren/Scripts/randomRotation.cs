using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float f = Random.RandomRange(0, 360);
        float f_b = Random.RandomRange(0.015f, 0.05f);
        transform.rotation=Quaternion.Euler(0,0,f);
        transform.localScale=new Vector2(f_b,f_b);
    }

}
