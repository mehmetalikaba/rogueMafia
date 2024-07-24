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
        if ((oyuncuHareket.movementX.x == 0 && !oyuncuHareket.havada) || oyuncuHareket.atiliyor || oyuncuHareket.hareketKilitli || oyuncuHareket.cakiliyor || oyuncuHareket.tirmanma.tirmaniyor)
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
        if (oyuncuHareket.movementY.y > 0 && oyuncuHareket.havada)
        {
            animator.SetBool("zipla", true);
            animator.SetBool("dusus", false);
        }
        if (oyuncuHareket.movementY.y < 0)
        {
            animator.SetBool("dusus", true);
            animator.SetBool("zipla", false);
        }
        if (oyuncuHareket.cakiliyor)
        {
            animator.SetBool("zipla", false);
            animator.SetBool("dusus", false);
            animator.SetBool("egilme", true);
        }
        if (oyuncuHareket.atiliyor)
        {
            animator.SetBool("zipla", false);
            animator.SetBool("dusus", false);
        }
        if (oyuncuHareket.tirmanma.tirmaniyor)
        {
            animator.SetBool("zipla", false);
            animator.SetBool("dusus", false);
        }
        if (oyuncuHareket.zeminde)
        {
            animator.SetBool("zipla", false);
            animator.SetBool("dusus", false);
        }
    }
}
