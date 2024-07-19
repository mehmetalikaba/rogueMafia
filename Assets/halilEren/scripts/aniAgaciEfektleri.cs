using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniAgaciEfektleri : MonoBehaviour
{
    public aniAgaciEfektleri[] aniAgaci;
    public bool baslangicSkill, acilabilir, acti;
    Animator animator;
    public envanterKontrol envanterKontrol;
    public yetenekKontrol yetenekKontrol;
    public float gerekenAniPuani;
    public GameObject aniPanel;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        envanterKontrol = FindAnyObjectByType<envanterKontrol>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (acti)
        {
            animator.SetBool("yanma", true);
            animator.SetBool("yanipSonme", false);
        }
        else
        {
            if (!baslangicSkill)
            {
                if (envanterKontrol.anilar <= gerekenAniPuani)
                {
                    animator.SetBool("yanipSonme", false);
                    animator.SetBool("sonme", true);
                }
                else
                    aniPuaniYeterli();
            }
            else
            {
                if (envanterKontrol.anilar >= gerekenAniPuani && !acti)
                {
                    acilabilir = true;
                    animator.SetBool("sonme", false);
                    animator.SetBool("yanipSonme", true);
                }
            }
        }
    }
    public void aniPuaniYeterli()
    {
        for (int i = 0; i < aniAgaci.Length; i++)
        {
            if (aniAgaci[i].acti)
            {
                if (!acti)
                {
                    acilabilir = true;
                    animator.SetBool("sonme", false);
                    animator.SetBool("yanipSonme", true);
                }
            }
        }
    }
    public void gelistirilmis()
    {
        if (acilabilir)
        {
            acti = true;
            acilabilir = false;
        }
    }
}
