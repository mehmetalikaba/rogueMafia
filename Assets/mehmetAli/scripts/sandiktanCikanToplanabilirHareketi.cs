using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandiktanCikanToplanabilirHareketi : MonoBehaviour
{
    public float xGucu = 2f;
    public float yGucu = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 launchDirection = new Vector2(Random.Range(-xGucu, xGucu), yGucu);
        rb.velocity = launchDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("zemin"))
            Destroy(rb);
    }
}
