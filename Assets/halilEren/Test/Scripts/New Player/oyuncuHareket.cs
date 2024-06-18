using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    oyuncuEfektYoneticisi oyuncuEfektYoneticisi;

    Rigidbody2D rb;
    public float hareketHizi;
    float input;

    bool sagaBakiyor = true;

    int ziplamaSayaci;
    public int ziplamaSayisi;
    public float ziplamaGucu;

    Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncuEfektYoneticisi = GetComponent<oyuncuEfektYoneticisi>();
        rb = GetComponent<Rigidbody2D>();
        ziplamaSayaci = ziplamaSayisi;

    }

    private void FixedUpdate()
    {
        
        input = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * hareketHizi, rb.velocity.y);

        if (!sagaBakiyor && input > 0)
        {
            Flip();
        }
        else if (sagaBakiyor && input < 0)
        {
            Flip();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& ziplamaSayaci > 0)
        {
            rb.velocity = Vector2.up * ziplamaGucu;
            oyuncuEfektYoneticisi.ZiplamaToz();
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.zeminde = false;
            ziplamaSayaci--;
        }
    }
    void Flip()
    {
        sagaBakiyor = !sagaBakiyor;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            ziplamaSayaci = ziplamaSayisi;
            oyuncuEfektYoneticisi.zeminde = true;
            oyuncuEfektYoneticisi.DusmeToz();
            oyuncuEfektYoneticisi.DusmeSesi();
        }
    }


}
