using UnityEngine;

public class dusmanHasar : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject kanPartikül, hasarRapor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncuSaldiri"))
        {
            rb.velocity = Vector2.right * 20;
            rb.velocity = Vector2.up * 1;
            Instantiate(kanPartikül,transform.position, Quaternion.identity);
            Instantiate(hasarRapor,transform.position, Quaternion.identity);
        }
    }
}
