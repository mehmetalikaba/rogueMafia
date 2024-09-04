using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rastegeleGuc : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float a = Random.RandomRange(-2, 2);
        float b = Random.RandomRange(6, 8);
        rb.AddForce(transform.right * a, ForceMode2D.Impulse);
        rb.AddForce(transform.up * b, ForceMode2D.Impulse);
    }

}
