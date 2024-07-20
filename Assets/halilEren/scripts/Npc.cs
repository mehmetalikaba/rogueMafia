using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public Animator animator;
    public GameObject karakter;
    oyuncuHareket oyuncuHareket;
    // Start is called before the first frame update
    void Start()
    {
        oyuncuHareket=FindObjectOfType<oyuncuHareket>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position,oyuncuHareket.transform.position);
        if(dist<=1)
        {
            animator.SetBool("acilma", true);
        }
        else
        {
            animator.SetBool("acilma", false);

        }

        if(oyuncuHareket.transform.position.x>transform.position.x)
        {
            karakter.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            karakter.transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }

}
