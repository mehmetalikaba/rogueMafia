using UnityEngine;

public class oyuncuAnimasyon : MonoBehaviour
{
    public Animator animator;

    oyuncuHareket oyuncuHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    tirmanma tirmanma;

    void Start()
    {
        oyuncuHareket = FindAnyObjectByType<oyuncuHareket>();
        oyuncuSaldiriTest = FindAnyObjectByType<oyuncuSaldiriTest>();
        tirmanma = FindObjectOfType<tirmanma>();
    }

    void Update()
    {
        if (!tirmanma.tirmaniyor)
        {
            if ((oyuncuHareket.movementX.x == 0 && !oyuncuHareket.havada) || oyuncuHareket.atiliyor)
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
}
