using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asamaKontrol : MonoBehaviour
{
    public bool oyuncuGeldi;

    void Update()
    {
        oyuncuGeldi = Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Oyuncu"));
    }
}
