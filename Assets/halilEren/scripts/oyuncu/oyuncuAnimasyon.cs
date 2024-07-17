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
        if (oyuncuHareket.movementX.x == 0 && !oyuncuHareket.havada)
        {
            animator.SetBool("kosu", false);
        }
        else if (oyuncuHareket.movementX.x != 0 && !oyuncuHareket.havada && !oyuncuSaldiriTest.solTikTiklandi && !oyuncuSaldiriTest.sagTikTiklandi)
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
}
