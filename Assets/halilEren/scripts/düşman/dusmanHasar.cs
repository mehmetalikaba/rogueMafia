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
        rb.velocity = Vector2.right * 2.5f;
        Instantiate(kanPartikül, transform.position, Quaternion.identity);
        Instantiate(hasarRapor, transform.position, Quaternion.identity);
    }
}
