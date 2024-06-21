using System.Collections;
using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    oyuncuEfektYoneticisi oyuncuEfektYoneticisi;

    public Rigidbody2D rb;
    public float hareketHizi;
    float input;

    public float atilmaHizi;
    public float atilmaMesafesi;
    bool atildi;

    bool sagaBakiyor = true;

    public bool egilme;
    bool ipde;
    bool zeminde;
    int ziplamaSayaci;
    public int ziplamaSayisi;
    public float ziplamaGucu;

    public Animator animator;

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
        if(egilme)
        {
            hareketHizi = 3;
        }
        else
        {
            hareketHizi = 6;

        }
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
        if(Input.GetKey(KeyCode.S))
        {
            egilme = true;
        }
        else
        {
            if(!ipde)
            {
                egilme = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)&& ziplamaSayaci > 0)
        {
            rb.velocity = Vector2.up * ziplamaGucu;
            oyuncuEfektYoneticisi.ZiplamaToz();
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.zeminde = false;
            zeminde = false;
            ziplamaSayaci--;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl)&&!zeminde)
        {
            rb.velocity = Vector2.down * ziplamaGucu*1.5f;
            animator.SetBool("cakilma", true);
            oyuncuEfektYoneticisi.ZiplamaSesi();
            oyuncuEfektYoneticisi.ZiplamaToz();

        }
        if(Input.GetMouseButtonDown(0))
        {
            if (transform.localScale.x == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + atilmaMesafesi, transform.position.y), atilmaHizi);

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - atilmaMesafesi, transform.position.y), atilmaHizi);

            }
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
        if(collision.gameObject.CompareTag("zemin"))
        {
            Debug.Log("zeminde");

            ziplamaSayaci = ziplamaSayisi;

            animator.SetBool("cakilma", false);
            animator.SetBool("dusus", false);

            oyuncuEfektYoneticisi.zeminde = true;
            zeminde = true;
            oyuncuEfektYoneticisi.DusmeToz();
            oyuncuEfektYoneticisi.DusmeSesi();
        }
        if (collision.gameObject.CompareTag("ip"))
        {
            animator.SetBool("cakilma", false);

            ipde=true;
            egilme = true;
            ziplamaSayaci = ziplamaSayisi;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ip"))
        {
            ipde = false;
            egilme = false;
        }
    }


}
