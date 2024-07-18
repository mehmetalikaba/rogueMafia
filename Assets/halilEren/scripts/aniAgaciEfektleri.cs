using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniAgaciEfektleri : MonoBehaviour
{
    public aniAgaciEfektleri[] aniAgaci;
    public bool yanipSonme, yanma, sonme,acti;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (yanipSonme)
        {
            aniPuaniYeterli();
        }
    }
    public void aniPuaniYeterli()
    {
        for (int i = 0; i < aniAgaci.Length; i++)
        {

            if (aniAgaci[i].acti)
            {
                animator.SetBool("sonme", false);
                animator.SetBool("yanipSonme", true);
            }
        }
    }
    public void aniPuaniYeterliDegil()
    {
        animator.SetBool("yanipSonme", false);
        animator.SetBool("sonme", true);
    }
    public void gelistirilmis()
    {
        animator.SetBool("yanipSonme", false);
        animator.SetBool("yanma", true);
        acti = true;
    }
}
