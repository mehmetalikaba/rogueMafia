using UnityEngine;

public class dusmanHasar : MonoBehaviour
{
    oyuncuSaldiriTest oyuncuSaldiriTest;
    Rigidbody2D rb;
    public GameObject kanPartikül, hasarRapor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oyuncuSaldiriTest = FindAnyObjectByType<oyuncuSaldiriTest>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(oyuncuSaldiriTest.saldirdi)
        {
            float mesafe = Vector2.Distance(transform.position, oyuncuSaldiriTest.transform.position);
            if (mesafe <= oyuncuSaldiriTest.saldiriMenzil)
            {
                Debug.Log(mesafe);
                rb.velocity = Vector2.right * 2.5f;
                Instantiate(kanPartikül, transform.position, Quaternion.identity);
                Instantiate(hasarRapor, transform.position, Quaternion.identity);
                oyuncuSaldiriTest.saldirdi = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*rb.velocity = Vector2.right * 2.5f;
        Instantiate(kanPartikül, transform.position, Quaternion.identity);
        Instantiate(hasarRapor, transform.position, Quaternion.identity);*/
    }
}
