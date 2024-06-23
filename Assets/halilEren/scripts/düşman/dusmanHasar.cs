using UnityEngine;
using TMPro;

public class dusmanHasar : MonoBehaviour
{
    public bool arkasiDuvar;
    public float can;
    public Animator uiAnimator;
    BoxCollider2D boxCollider;
    Animator animator;
    Rigidbody2D rb;
    GameObject oyuncu;

    dusmanHareket dusmanHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;

    public GameObject elmas,ejderPuani,kanPartikül,kanPartikülDuvar, hasarRapor;

    public TextMeshProUGUI canText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        oyuncuSaldiriTest = FindAnyObjectByType<oyuncuSaldiriTest>();
        dusmanHareket=GetComponent<dusmanHareket>();
    }

    public void hasarAl(float sadiri)
    {
        uiAnimator.SetTrigger("hasar");
        animator.SetTrigger("hasar");

        if (oyuncu.transform.position.x <= transform.position.x)
        {
            rb.velocity = Vector2.right * 2.5f;
        }
        if (oyuncu.transform.position.x > transform.position.x)
        {
            rb.velocity = Vector2.right * -2.5f;
        }
        Instantiate(kanPartikül, transform.position, Quaternion.identity);
        if(arkasiDuvar)
        {
            Instantiate(kanPartikülDuvar, transform.position, Quaternion.identity);
        }
        Instantiate(hasarRapor, transform.position, Quaternion.identity);
        can -= oyuncuSaldiriTest.sonHasar;
        canText.text = can.ToString();
        if(can<=0)
        {
            canText.text = "0";
            boxCollider.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;

            Instantiate(ejderPuani, transform.position, Quaternion.identity);
            Instantiate(elmas, transform.position, Quaternion.identity);
            animator.SetBool("yurume", false);
            animator.SetBool("olum", true);
            dusmanHareket.enabled = false;
            this.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ok"))
        {
            uiAnimator.SetTrigger("hasar");
            if (!dusmanHareket.gordu)
            {
                animator.SetTrigger("hasar");
                dusmanHareket.gordu = true;
                Instantiate(dusmanHareket.uyari, transform.position, Quaternion.identity);
            } 

            if (collision.transform.position.x <= transform.position.x)
            {
                rb.velocity = Vector2.right * 2.5f;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                rb.velocity = Vector2.right * -2.5f;
            }
            Instantiate(kanPartikül, transform.position, Quaternion.identity);
            if(arkasiDuvar)
            {
                Instantiate(kanPartikülDuvar, transform.position, Quaternion.identity);
            }
            Instantiate(hasarRapor, transform.position, Quaternion.identity);
            can -= 25;
            canText.text = can.ToString();

            if (can <= 0)
            {
                canText.text = "0";
                boxCollider.enabled = false;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;


                Instantiate(ejderPuani, transform.position, Quaternion.identity);
                Instantiate(elmas,transform.position, Quaternion.identity);
                animator.SetBool("yurume", false);
                animator.SetBool("olum", true);
                dusmanHareket.enabled = false;
                this.enabled = false;
            }
            Destroy(collision.gameObject);
        }
    }
}

