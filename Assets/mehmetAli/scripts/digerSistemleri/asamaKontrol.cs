using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asamaKontrol : MonoBehaviour
{
    public bool oyuncuGeldi;
    public float mesafe = 0.5f;

    void Update()
    {
        oyuncuGeldi = Physics2D.OverlapCircle(transform.position, mesafe, LayerMask.GetMask("Oyuncu"));
    }
}
