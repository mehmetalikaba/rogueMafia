using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public bool yakin, serbest;
    public Animator animator;
    public GameObject karakter;
    oyuncuHareket oyuncuHareket;
    // Start is called before the first frame update
    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!serbest)
        {
            float dist = Vector2.Distance(transform.position, oyuncuHareket.transform.position);
            if (dist <= 1)
            {
                animator.SetBool("acilma", true);
                yakin = true;
            }
            else
            {
                animator.SetBool("acilma", false);
                yakin = false;
            }

            if (oyuncuHareket.transform.position.x > transform.position.x)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void diyalogAc()
    {
        animator.SetBool("acilma", true);

        if (oyuncuHareket.transform.position.x > transform.position.x)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void diyalogKapat()
    {
        animator.SetBool("acilma", false);
    }
}
