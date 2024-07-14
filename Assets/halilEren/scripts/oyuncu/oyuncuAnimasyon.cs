using UnityEngine;

public class oyuncuAnimasyon : MonoBehaviour
{
    public Animator animator;

    oyuncuHareket oyuncuHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;


    void Start()
    {
        oyuncuHareket = FindAnyObjectByType<oyuncuHareket>();
        oyuncuSaldiriTest = FindAnyObjectByType<oyuncuSaldiriTest>();
    }

    void Update()
    {
        if (oyuncuHareket.egilme == false)
        {
            if (oyuncuHareket.movementX.x == 0 && !oyuncuHareket.havada)
            {
                animator.SetBool("kosu", false);
            }
            else if (oyuncuHareket.movementX.x != 0 && !oyuncuHareket.havada)
            {
                animator.SetBool("kosu", true);
            }
            if (oyuncuHareket.movementY.y == 0)
            {
                animator.SetBool("zipla", false);
                animator.SetBool("dusus", false);
            }
            if (oyuncuHareket.movementY.y > 0)
            {
                animator.SetBool("zipla", true);
                animator.SetBool("dusus", false);
            }
            if (oyuncuHareket.movementY.y < 0)
            {
                animator.SetBool("dusus", true);
                animator.SetBool("zipla", false);
            }
        }
        if (oyuncuHareket.egilme)
        {
            animator.SetBool("egilme", true);
            animator.SetBool("kosu", false);
            animator.SetBool("zipla", false);
            animator.SetBool("dusus", false);

            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("aTusu")) || Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("dTusu")))
                animator.SetBool("kosu", true);
            else
                animator.SetBool("kosu", false);
        }
        else
            animator.SetBool("egilme", false);
    }
}
