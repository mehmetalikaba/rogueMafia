using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class dusmanHasar : MonoBehaviour
{
    public GameObject buz,zehir;

    public bool agresif, yumi;
    public bool arkasiDuvar;
    public float can;
    public Animator uiAnimator;
    BoxCollider2D boxCollider;
    Animator animator;
    Rigidbody2D rb;
    GameObject oyuncu;

    dusmanAgresif dusmanAgresif;
    dusmanYumi dusmanYumi;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    kameraSarsinti kameraSarsinti;
    silahUltileri silahUltileri;

    public GameObject okVurulmaSesi;
    public GameObject elmas, ejderPuani, kanPartikül, kanPartikülDuvar, hasarRapor;

    public TextMeshProUGUI canText;

    float zehirTimer;
    bool zehirleniyor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        if(agresif)
        {
            dusmanAgresif = GetComponent<dusmanAgresif>();
        }
        if(yumi)
        {
            dusmanYumi= GetComponent<dusmanYumi>();
        }
        kameraSarsinti = FindObjectOfType<kameraSarsinti>();
        silahUltileri = FindObjectOfType<silahUltileri>();
    }
    private void Update()
    {
        Zehir();
    }
    void Olum()
    {
        if (can <= 0)
        {
            canText.text = "0";
            boxCollider.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;


            Instantiate(ejderPuani, transform.position, Quaternion.identity);
            Instantiate(elmas, transform.position, Quaternion.identity);
            animator.SetBool("yurume", false);
            animator.SetBool("olum", true);
            if(agresif)
            {
                dusmanAgresif.enabled = false;

            }
            if (yumi)
            {
                dusmanYumi.enabled = false;
            }
            this.enabled = false;
        }
    }
    void Zehir()
    {
        if(zehirleniyor)
        {
            zehirTimer += Time.deltaTime;
            if(zehirTimer>=1.5f)
            {
                hasarAl(10);
                zehirTimer = 0;
            }
        }
    }
    public void hasarAl(float sadiri)
    {
        if (!silahUltileri.silah1UltiAcik)
        {
            silahUltileri.silah1Ulti += 10;
        }

        uiAnimator.SetTrigger("hasar");
        kameraSarsinti.Shake();

        Instantiate(kanPartikül, transform.position, Quaternion.identity);

        if (arkasiDuvar)
        {
            Instantiate(kanPartikülDuvar, transform.position, Quaternion.identity);
        }
        Instantiate(hasarRapor, transform.position, Quaternion.identity);
        can -= oyuncuSaldiriTest.sonHasar;
        canText.text = can.ToString();
        Olum();


        /*if (oyuncu.transform.position.x <= transform.position.x)
        {
            rb.velocity = Vector2.right * 3f;
        }
        if (oyuncu.transform.position.x > transform.position.x)
        {
            rb.velocity = Vector2.right * -3f;
        }*/

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("shuriken"))
        {
            Instantiate(okVurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(kanPartikül, transform.position, Quaternion.identity);
            uiAnimator.SetTrigger("hasar");
            kameraSarsinti.Shake();

            can -= 20;
            Olum();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("ok"))
        {
            Instantiate(okVurulmaSesi, transform.position, Quaternion.identity);
            hasarAl(oyuncuSaldiriTest.sonHasar);
            Olum();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("havaiFisek"))
        {
            Instantiate(kanPartikül, transform.position, Quaternion.identity);

            if (oyuncu.transform.position.x <= transform.position.x)
            {
                rb.AddForce(transform.right * 30, ForceMode2D.Impulse);
            }
            if (oyuncu.transform.position.x > transform.position.x)
            {
                rb.AddForce(transform.right * -30, ForceMode2D.Impulse);
            }
            can -= 500;
            canText.text = can.ToString();
            Olum();
            Destroy(collision.gameObject,0.01f);
        }

        if(collision.gameObject.CompareTag("buz"))
        {
            if(agresif)
            {
                buz.SetActive(true);
                dusmanAgresif.enabled = false;
                animator.enabled = false;
            }
            if(yumi)
            {
                buz.SetActive(true);
                dusmanYumi.enabled = false;
                animator.enabled = false;
            }
        }
        if (collision.gameObject.CompareTag("zehir"))
        {
            zehir.SetActive(true);
            zehirleniyor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("buz"))
        {
            if (agresif)
            {
                buz.SetActive(false);

                if (can > 0)
                {
                    dusmanAgresif.enabled = true;

                }
                animator.enabled = true;
            }
            if (yumi)
            {
                buz.SetActive(false);
                if (can > 0)
                {
                    dusmanYumi.enabled = true;

                }
                animator.enabled = true;
            }
        }


        if (collision.gameObject.CompareTag("zehir"))
        {
            zehir.SetActive(false);
            zehirleniyor = false;
            hasarAl(10);
            zehirTimer = 0;
        }
    }
}

