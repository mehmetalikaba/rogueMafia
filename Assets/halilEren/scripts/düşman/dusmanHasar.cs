using UnityEngine;

public class dusmanHasar : MonoBehaviour
{
    public float can;
    Rigidbody2D rb;
    GameObject oyuncu;

    oyuncuSaldiriTest oyuncuSaldiriTest;

    public GameObject kanPartikül, hasarRapor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        oyuncuSaldiriTest = FindAnyObjectByType<oyuncuSaldiriTest>();
    }

    public void hasarAl(float sadiri)
    {
        if (oyuncu.transform.position.x <= transform.position.x)
        {
            rb.velocity = Vector2.right * 2.5f;
        }
        if (oyuncu.transform.position.x > transform.position.x)
        {
            rb.velocity = Vector2.right * -2.5f;
        }
        Instantiate(kanPartikül, transform.position, Quaternion.identity);
        Instantiate(hasarRapor, transform.position, Quaternion.identity);
        can -= oyuncuSaldiriTest.sonHasar;
        if(can<=0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ok"))
        {
            Instantiate(kanPartikül, transform.position, Quaternion.identity);
            Instantiate(hasarRapor, transform.position, Quaternion.identity);
            can -= 20;
            if (can <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}

